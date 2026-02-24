Shader "Sprites/Soft Sprite (Parallax Atmosphere Final)"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        // Edge softness (TMP-style)
        _Softness ("Edge Softness", Range(0,1)) = 0.3
        _Threshold ("Edge Center", Range(0,1)) = 0.5

        // Blur
        _BlurSize ("Blur Size", Range(0,0.01)) = 0.002
        _DistanceBlur ("Distance Blur", Range(0,1)) = 1

        // Temperature
        _Temperature ("Local Temperature", Range(-1,1)) = 0
        _GlobalTemp ("Global Temperature", Range(-1,1)) = 0
        _DayNight ("Day/Night", Range(0,1)) = 0

        // Heat & Wind
        _HeatStrength ("Heat Strength", Range(0,0.02)) = 0
        _HeatSpeed ("Heat Speed", Range(0,5)) = 1
        _Wind ("Wind Vector", Vector) = (0,0,0,0)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Softness, _Threshold;
            float _BlurSize, _DistanceBlur;
            float _Temperature, _GlobalTemp, _DayNight;
            float _HeatStrength, _HeatSpeed;
            float4 _Wind;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed3 GaussianBlur(float2 uv, float blur)
            {
                fixed3 col = tex2D(_MainTex, uv).rgb * 0.5;
                float2 o = float2(blur, blur);

                col += tex2D(_MainTex, uv + o).rgb * 0.125;
                col += tex2D(_MainTex, uv - o).rgb * 0.125;
                col += tex2D(_MainTex, uv + float2(o.x, -o.y)).rgb * 0.125;
                col += tex2D(_MainTex, uv + float2(-o.x, o.y)).rgb * 0.125;

                return col;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float time = _Time.y * _HeatSpeed;

                float2 windOffset = _Wind.xy * time;
                float heatOffset = sin((i.uv.y + time) * 10) * _HeatStrength;

                float2 uv = i.uv + windOffset + heatOffset;

                fixed4 baseTex = tex2D(_MainTex, uv);
                float dist = baseTex.a;

                float outer = smoothstep(_Threshold - _Softness, _Threshold, dist);
                float inner = smoothstep(_Threshold, _Threshold + _Softness, dist);
                float alpha = max(outer * (1.0 - inner), dist);

                float edgeFactor = saturate(abs(dist - _Threshold) / max(_Softness, 0.0001));
                float blur = _BlurSize * edgeFactor * _DistanceBlur;

                fixed3 color = GaussianBlur(uv, blur);

                float temp = _Temperature + _GlobalTemp;
                temp = lerp(temp, temp - 0.3, _DayNight);

                fixed3 cold = fixed3(0.85, 0.9, 1.1);
                fixed3 hot  = fixed3(1.1, 0.9, 0.8);
                fixed3 tint = lerp(cold, hot, saturate((temp + 1) * 0.5));

                color *= tint;

                return fixed4(color * i.color.rgb, alpha * i.color.a);
            }
            ENDCG
        }
    }
}
