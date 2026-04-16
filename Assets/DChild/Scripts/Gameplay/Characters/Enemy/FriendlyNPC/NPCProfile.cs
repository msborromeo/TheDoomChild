using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.NPC
{

    [CreateAssetMenu(fileName = "NPCProfile", menuName = "DChild/Database/NPC Profile")]
    public class NPCProfile : ScriptableObject
    {
        [SerializeField]
        private string m_name;
        [SerializeField]
        private string m_title;
        [SerializeField, PreviewField]
        private Sprite m_baseIcon;
        
        
        [SerializeField, BoxGroup("Merchant Shop Assets")]
        private Sprite m_shopBackground;
        [SerializeField, BoxGroup("Merchant Shop Assets")]
        private Sprite m_frontPanel;
        [SerializeField, BoxGroup("Merchant Shop Assets")]
        private Sprite m_categoryBar;
        [SerializeField, BoxGroup("Merchant Shop Assets")]
        private Sprite m_tradeActionsPanel;
        [SerializeField, BoxGroup("Merchant Shop Assets")]
        private Sprite m_bottomPanel;

        public string characterName => m_name;
        public string title => m_title;
        public Sprite baseIcon => m_baseIcon;


        public Sprite shopBackground => m_shopBackground;
        public Sprite frontPanel => m_frontPanel;
        public Sprite categoryBar => m_categoryBar;
        public Sprite tradeActionsPanel => m_tradeActionsPanel;
        public Sprite bottomPanel => m_bottomPanel;

    }
}