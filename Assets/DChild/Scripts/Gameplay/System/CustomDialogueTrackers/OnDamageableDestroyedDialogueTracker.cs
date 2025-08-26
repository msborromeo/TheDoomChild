using DChild.Gameplay.Combat;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.Dialogues.Triggers
{
    public class OnDamageableDestroyedDialogueTracker : MonoBehaviour
    {
        [SerializeField,ValueDropdown("GetAllDamageables",IsUniqueList = true)]
        private Damageable[] m_entitiesToTracks;
        [SerializeField]
        private DialogueSystemTrigger[] m_triggers;

        private IEnumerable GetAllDamageables()
        {
            Func<Transform, string> getPath = null;
            getPath = x => (x ? getPath(x.transform.parent) + "/" + x.gameObject.name : "");
            return FindObjectsOfType<Damageable>().Select(x => new ValueDropdownItem(getPath(x.transform),x));
        }

        private void OnEntityDeath(object sender, EventActionArgs eventArgs)
        {
            for (int i = 0; i < m_triggers.Length; i++)
            {
                m_triggers[i].OnUse();
            }

            var entity = (Damageable)sender;
            entity.Destroyed -= OnEntityDeath;
        }

        private void OnEnable()
        {
            for (int i = 0; i < m_entitiesToTracks.Length; i++)
            {
                if (m_entitiesToTracks[i] == null)
                    continue;

                m_entitiesToTracks[i].Destroyed += OnEntityDeath;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < m_entitiesToTracks.Length; i++)
            {
                if (m_entitiesToTracks[i] == null)
                    continue;

                m_entitiesToTracks[i].Destroyed -= OnEntityDeath;
            }
        }

    }
}