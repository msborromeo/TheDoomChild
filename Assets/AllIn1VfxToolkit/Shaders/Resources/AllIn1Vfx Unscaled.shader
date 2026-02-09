Shader "AllIn1Vfx/AllIn1Vfx Unscaled"
{
    Properties
    {
        _RenderingMode("Rendering Mode", float) = 0
        _UnscaledTime("Unscaled Time", Float) = 0
        _UseUnscaledTime("Use Unscaled Time", Float) = 1
        _TimeScale("Time Scale", Float) = 1

        // … keep all other properties exactly as in your original shader
        _SrcMode("SrcMode", float) = 5
        _DstMode("DstMode", float) = 10
        _CullingOption("Culling Option", float) = 0
        _ZWrite("Depth Write", float) = 0.0
        _ZTestMode("Z Test Mode", float) = 4
        _ColorMask("Color Write Mask", float) = 15

        _Alpha("Global Alpha", Range(0, 1)) = 1
        _Color("Global Color", Color) = (1,1,1,1)

        _TimingSeed("Random Seed", Float) = 0.0
        _EditorDrawers("Editor Drawers", Int) = 60

        _MainTex("Shape1 Texture", 2D) = "white" {}
        [HDR] _ShapeColor("Shape1 Color", Color) = (1,1,1,1)
        _ShapeXSpeed("Shape1 X Speed", Float) = 0
        _ShapeYSpeed("Shape1 Y Speed", Float) = 0
        _ShapeContrast("Shape1 Contrast", Range(0, 10)) = 1
        _ShapeBrightness("Shape1 Brightness", Range(-1, 1)) = 0
        _ShapeDistortTex("Distortion Texture", 2D) = "black" {}
        _ShapeDistortAmount("Distortion Amount", Range(0, 10)) = 0.5
        _ShapeDistortXSpeed("Scroll speed X", Float) = 0.1
        _ShapeDistortYSpeed("Scroll speed Y", Float) = 0.1
        _ShapeColorWeight("Shape1 RGB Weight", Range(0, 5)) = 1
        _ShapeAlphaWeight("Shape1 A Weight", Range(0, 5)) = 1

            // … copy all other properties from your original shader
            // No changes needed for properties
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent" "CanUseSpriteAtlas" = "True" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane"
            }
            Blend[_SrcMode][_DstMode]
            Cull[_CullingOption]
            ZWrite[_ZWrite]
            ZTest[_ZTestMode]
            ColorMask[_ColorMask]
            Lighting Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_instancing

                // … keep all #pragma shader_feature as in original

                float _UnscaledTime;
                float _TimeScale;
                float _UseUnscaledTime;

                #include "UnityCG.cginc"
                #include "AllIn1VfxFunctions.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float4 uv : TEXCOORD0;
                    half2 customData1 : TEXCOORD1;
                    half3 normal : NORMAL;
                    half4 color : COLOR;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f
                {
                    float4 uvSeed : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    half4 color : COLOR;
                    half2 offsetCustomData : TEXCOORD1;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                    float time = _UseUnscaledTime > 0 ? _UnscaledTime * _TimeScale : _Time;

                    #if VERTOFFSET_ON
                    half4 offsetUv = half4(TRANSFORM_TEX(v.uv.xy, _VertOffsetTex),0,0);
                    offsetUv.x += (time * _VertOffsetTexXSpeed) % 1;
                    offsetUv.y += (time * _VertOffsetTexYSpeed) % 1;
                    v.vertex.xyz += v.normal * _VertOffsetAmount * pow(tex2Dlod(_VertOffsetTex, offsetUv).r, _VertOffsetPower);
                    #endif

                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uvSeed = v.uv;
                    o.color = v.color;

                    #if OFFSETSTREAM_ON
                    o.offsetCustomData = v.customData1.xy;
                    #endif

                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {
                    float seed = i.uvSeed.z + UNITY_ACCESS_INSTANCED_PROP(Seeds, _TimingSeed);
                    float time = _UseUnscaledTime > 0 ? _UnscaledTime * _TimeScale + seed : _Time + seed;

                    // Example: Texture scroll
                    #if TEXTURESCROLL_ON
                    i.uvSeed.x += (time * _TextureScrollXSpeed) % 1;
                    i.uvSeed.y += (time * _TextureScrollYSpeed) % 1;
                    #endif

                    // Example: Shake UV
                    #if SHAKEUV_ON
                    half xShake = sin(time * _ShakeUvSpeed * 50) * _ShakeUvX;
                    half yShake = cos(time * _ShakeUvSpeed * 50) * _ShakeUvY;
                    i.uvSeed.xy += half2(xShake * 0.012, yShake * 0.01);
                    #endif

                    // Example: Rotate shapes
                    #if SHAPE1ROTATE_ON
                    half2 shape1Uv = RotateUvs(i.uvSeed.xy, _ShapeRotationOffset + ((_ShapeRotationSpeed * time) % 6.28318530718), _MainTex_ST);
                    #endif

                    // … repeat for all other effects that use `_Time`
                    // All `_Time + seed` replaced with `time`

                    half4 col = tex2D(_MainTex, i.uvSeed.xy); // Sample main texture
                    return col;
                }

                ENDCG
            }
        }
}
