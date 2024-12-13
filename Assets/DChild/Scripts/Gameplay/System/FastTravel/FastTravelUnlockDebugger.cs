using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.FastTravel.Debug
{
    public class FastTravelUnlockDebugger : MonoBehaviour
    {
        [SerializeField, AssetList]
        private FastTravelPageData[] m_pageDatas;

        public void SetAllFastTravelState(bool unlock)
        {
            for (int i = 0; i < m_pageDatas.Length; i++)
            {
                //Create Variable names
                var data = m_pageDatas[i];
                for (int j = 0; j < data.count; j++)
                {
                    var variable = FastTravelUtility.GenerateActivationVariableName(data.GetUnderworldTravelData(j));
                    DialogueLua.SetVariable(variable, unlock);
                }
            }
        }
    }
}
