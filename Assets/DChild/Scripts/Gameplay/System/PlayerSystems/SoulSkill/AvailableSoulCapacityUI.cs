using DChild.Gameplay.Characters.Players.SoulSkills;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.SoulSkills.UI
{
    public class AvailableSoulCapacityUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_capacityText;

        public void DisplayCapacity(int capacity)
        {
            m_capacityText.text = capacity.ToString();
        }

    }
}
