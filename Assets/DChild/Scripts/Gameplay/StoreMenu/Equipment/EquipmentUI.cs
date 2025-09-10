using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_activeItem;
        [SerializeField] private List<Image> m_partsList;
    }
}
