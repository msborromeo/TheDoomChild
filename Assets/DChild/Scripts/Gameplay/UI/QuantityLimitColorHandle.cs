using DChild.Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public class QuantityLimitColorHandle : MonoBehaviour
    {
        [SerializeField]
        protected Color overLimitColor = Color.red;
        [SerializeField]
        protected TextMeshProUGUI quantityText;
        [SerializeField]
        protected Color underLimitColor => Color.white;

        public virtual IStoredItem currentItem {  get; set; }

        public void UpdateQuantityTextColor()
        {
            if(currentItem.count >= currentItem.data.quantityLimit)
            {
                quantityText.color = overLimitColor;
            }
            else
            {
                quantityText.color = underLimitColor;
            }
        }
    }
}

