using DChild;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderControllerBBW : MonoBehaviour
{
    [SerializeField]
    private List<MaterialParameterCall> m_materialParameterCall;
    [SerializeField]
    private float m_setLerpSpeed; 

    public IEnumerator AppearRoutine()
    {
        yield return null;
    }
    [Button]
    private void DissapearEffect()
    {
        Debug.LogWarning("Dissapear Effect");
        for (int i = 0; i < m_materialParameterCall.Count; i++)
        {

            var individualMaterialParamCall = m_materialParameterCall[i];
            individualMaterialParamCall.SetLerpSpeed(m_setLerpSpeed);
            individualMaterialParamCall.SetValue(true);
            individualMaterialParamCall.LerpValue(0f);
            //m_materialParameterCall.SetLerpSpeed(m_setLerpSpeed);
            //m_materialParameterCall.SetValue(true);
            //m_materialParameterCall.LerpValue(m_lerpValue);
        }
    }
    [Button]
    private void AppearEffect()
    {
        Debug.LogWarning("Appear Effect");
        for (int i = 0; i < m_materialParameterCall.Count; i++)
        {
            var individualMaterialParamCall = m_materialParameterCall[i];
            individualMaterialParamCall.SetLerpSpeed(m_setLerpSpeed);
            individualMaterialParamCall.SetValue(false);
            individualMaterialParamCall.LerpValue(1f);
        }
        //    m_materialParameterCall.SetLerpSpeed(m_setLerpSpeed);
        //m_materialParameterCall.SetValue(false);
        //m_materialParameterCall.LerpValue(1f);
    }
}
