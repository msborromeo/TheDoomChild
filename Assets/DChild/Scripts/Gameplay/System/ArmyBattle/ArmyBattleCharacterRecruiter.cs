using DChild.Gameplay.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle
{
    public class ArmyBattleCharacterRecruiter : MonoBehaviour, IGameplaySystemModule , IGameplayInitializable
    {
        [SerializeField]
        private List<int> m_recruitedCharacters;

        public ArmyCharactersSaveData SaveData() => new ArmyCharactersSaveData(m_recruitedCharacters.ToArray());

        public void LoadData(ArmyCharactersSaveData data)
        {
            m_recruitedCharacters.Clear();
            if (data == null)
                return;

            for (int i = 0; i < data.recruitedCharacterCount; i++)
            {
                m_recruitedCharacters.Add(data.GetRecruitedCharacterID(i));
            }
        }

        public void SetAsRecruited(ArmyCharacterData characterData, bool isRecruited)
        {
            var id = characterData.ID;
            if (isRecruited)
            {
                if(m_recruitedCharacters.Contains(id) == false)
                {
                    m_recruitedCharacters.Add(id);
                }
            }
            else if(m_recruitedCharacters.Contains(id))
            {
                m_recruitedCharacters.Remove(id);
            }
        }

        public bool HasRecruitedCharacter(ArmyCharacterData characterData)
        {
            return m_recruitedCharacters.Contains(characterData.ID);
        }

        public int ArmySize()
        {
            return m_recruitedCharacters.Count;
        }

        public void Initialize()
        {
            m_recruitedCharacters = new List<int>();
            GameplaySystem.campaignSerializer.PreSerialization += OnPreSerialization;
            GameplaySystem.campaignSerializer.PostDeserialization += OnPostDeserialization;
        }

        private void OnDisable()
        {
            GameplaySystem.campaignSerializer.PreSerialization -= OnPreSerialization;
            GameplaySystem.campaignSerializer.PostDeserialization -= OnPostDeserialization;
        }

        private void OnPostDeserialization(object sender, CampaignSlotUpdateEventArgs eventArgs)
        {
            if (eventArgs.IsPartOfTheUpdate(SerializationScope.Quest))
            {
                LoadData(GameplaySystem.campaignSerializer.slot.armyCharactersSaveData);
            }
        }

        private void OnPreSerialization(object sender, CampaignSlotUpdateEventArgs eventArgs)
        {
            if (eventArgs.IsPartOfTheUpdate(SerializationScope.Quest))
            {
                GameplaySystem.campaignSerializer.slot.UpdateArmyCharacterData(SaveData());
            }
        }

        private void OnDestroy()
        {
            GameplaySystem.campaignSerializer.PreSerialization -= OnPreSerialization;
            GameplaySystem.campaignSerializer.PostDeserialization -= OnPostDeserialization;
        }
    }
}