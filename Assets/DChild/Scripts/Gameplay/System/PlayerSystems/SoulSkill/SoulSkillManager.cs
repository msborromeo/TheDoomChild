using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.SoulSkills.UI;
using DChild.Gameplay.Systems;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DChild.Gameplay.SoulSkills
{

    public class SoulSkillManager : MonoBehaviour, IGameplaySystemModule, ISoulSkillManager, IGameplayInitializable
    {
        [SerializeField]
        private SoulSkillList m_completeSoulSkillList;
        [SerializeField]
        private PlayerSoulSkillHandle m_playerHandle;

        /*Things I Need:
        -Something to Show Available SoulSkills
        -Something to Activate/Deactivate SoulSkills
        */
        [SerializeField, BoxGroup("UI")]
        private SoulSkillSelection m_skillSelection;
        [SerializeField, BoxGroup("UI")]
        private AvailableSoulCapacityUI m_availableSoulCapacity;
        [SerializeField, BoxGroup("UI")]
        private SoulSkillListUI m_availableListUI;
        [SerializeField, BoxGroup("UI")]
        private SoulSkillInfoUI m_infoUI;
        [SerializeField]
        private bool m_forceSoulSkillActivation;

        private bool m_canActivateSoulSkill;
        private bool m_hasSubscribed;

        private bool canActivateSoulSkill => m_canActivateSoulSkill || m_forceSoulSkillActivation;

        public void AllowSoulSkillActivation(bool canActivateSoulSkill)
        {
            m_canActivateSoulSkill = canActivateSoulSkill;
        }

        public void ForceAllowSoulSkillActivation(bool forceCanActivateSoulSkill)
        {
            m_forceSoulSkillActivation = forceCanActivateSoulSkill;
        }

        public void ActivateSoulSkill(int soulSkillID)
        {
            if (canActivateSoulSkill == false)
                return;

            ActivateSoulSkill(m_completeSoulSkillList.GetInfo(soulSkillID));
        }

        public void DeactivateSoulSkill(int soulSkillID)
        {
            if (canActivateSoulSkill == false)
                return;

            DeactivateSoulSkill(m_completeSoulSkillList.GetInfo(soulSkillID));
        }

        public void ActivateSoulSkill(SoulSkill soulSkill)
        {
            if (m_playerHandle.CanBeActivated(soulSkill))
            {
                m_playerHandle.AddAsActivated(soulSkill);
                ////m_activatedListUI.ActivateSoulSkill(soulSkill);
                m_availableListUI.SetActivatedUIState(soulSkill.id, true);

                m_availableSoulCapacity.DisplayCapacity(m_playerHandle.currentSoulCapacity);
            }
            else
            {
                //Do Something to warn player why it cant be activated
            }
        }

        public void DeactivateSoulSkill(SoulSkill soulSkill)
        {
            m_playerHandle.RemoveAsActivated(soulSkill);
            m_availableListUI.SetActivatedUIState(soulSkill.id, false);
        }

        public void SetAsActivatedSoulSkills(IReadOnlyCollection<int> list)
        {
            m_availableListUI.InitializeListActivatedState(list);
            List<SoulSkill> activatedSoulSkills = new List<SoulSkill>();
            for (int i = 0; i < list.Count; i++)
            {
                activatedSoulSkills.Add(m_completeSoulSkillList.GetInfo(list.ElementAt(i)));
            }
            //m_activatedListUI.SetAsActivedSoulSkills(activatedSoulSkills);

            m_availableSoulCapacity.DisplayCapacity(m_playerHandle.currentSoulCapacity);
        }

        public void Initialize()
        {
            if (m_hasSubscribed == false)
            {
                m_playerHandle.SaveDataLoaded += OnSoulSkillSaveDataLoaded;
                m_playerHandle.AvailableSoulSkillChanged += OnAvailableSkillsChanged;
                m_playerHandle.MaxCapacityChanged += OnMaxCapacityChanged;
                m_skillSelection.OnSelected += OnSoulSkillSelected;
                m_skillSelection.OnActionRequired += OnSoulSkillActionRequired;
            }
            m_hasSubscribed = true;
            m_availableListUI.SetAvailableSkills(m_playerHandle.acquiredSkills);
            m_availableListUI.InitializeList(m_completeSoulSkillList);
            //m_activatedListUI.Reset();
            SyncWithSaveData();
        }

        public void SetAvailableSoulSkills(IReadOnlyCollection<int> list)
        {
            m_availableListUI.SetAvailableSkills(list);
        }

        private void OnSoulSkillSelected(object sender, SoulSkillUIEventArgs eventArgs)
        {
            var soulSkill = m_completeSoulSkillList.GetInfo(eventArgs.soulskillUI.soulSkillID);
            m_infoUI.DisplayInfo(soulSkill);
        }
        private void OnSoulSkillActionRequired(object sender, SoulSkillUIEventArgs eventArgs)
        {
            var soulSkillUI = eventArgs.soulskillUI;
            if (soulSkillUI.isActivated)
            {
                DeactivateSoulSkill(soulSkillUI.soulSkillID);
                soulSkillUI.SetAcivatedStatus(false);
                return;
            }

            ActivateSoulSkill(soulSkillUI.soulSkillID);
            soulSkillUI.SetAcivatedStatus(true);
        }

        private void OnSoulSkillSaveDataLoaded(object sender, EventActionArgs eventArgs)
        {
            SyncWithSaveData();
        }

        private void SyncWithSaveData()
        {
            //m_activatedListUI.DisplayCapacity(m_playerHandle.currentSoulCapacity);
            var skillIDs = m_completeSoulSkillList.GetIDs();

            //m_availableListUI.UpdateAvailableSkillsList(m_playerHandle.acquiredSkills);
            var activatedSkillIDs = m_playerHandle.activatedSkills;
            m_availableListUI.InitializeListActivatedState(activatedSkillIDs);

            var activatedSoulSkillList = new List<SoulSkill>();
            foreach (var skill in activatedSkillIDs)
            {
                var soulSkill = m_completeSoulSkillList.GetInfo(skill);
                if (soulSkill == null)
                {
                    Debug.LogError(skill + " - This is null because this soul skill is activated through Equipment and data is not yet added to Soul Skill List");
                    continue;
                }

                activatedSoulSkillList.Add(soulSkill);
                m_playerHandle.AddAsActivated(soulSkill);
            }
            //m_activatedListUI.SetAsActivedSoulSkills(activatedSoulSkillList);
            m_availableSoulCapacity.DisplayCapacity(m_playerHandle.currentSoulCapacity);
        }

        private void OnMaxCapacityChanged(object sender, EventActionArgs eventArgs)
        {
            m_availableSoulCapacity.DisplayCapacity(m_playerHandle.currentSoulCapacity);
        }

        private void OnAvailableSkillsChanged(object sender, EventActionArgs eventArgs)
        {
            m_availableListUI.SetAvailableSkills(m_playerHandle.acquiredSkills);
        }

    }
}
