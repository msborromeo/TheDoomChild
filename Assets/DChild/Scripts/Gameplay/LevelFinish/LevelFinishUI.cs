using DChild.Gameplay.FastTravel;
using DChild.Gameplay.Systems;
using DChild.Gameplay.Systems.Serialization;
using DChild.Gameplay.UI;
using DChild.Menu;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
namespace DChild.Gameplay.LevelFinish.UI
{
    public class LevelFinishUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_headerText;
        [SerializeField]
        private TextMeshProUGUI m_instructionsText;
        [SerializeField]
        private SetTextToTextBox m_promptSetter;
        //fastest way to teleport player at the moment
        [SerializeField]
        private ConfirmationHandler m_confirmationHandler;
        [SerializeField]
        private FastTravelHandle m_fastTravelHandle;

        private LocationData m_destinationData;


        [Button]
        public void NotifyAvailableLocation(AvailableLocations location, InputActionConfiguration input)
        {
            switch (location)
            {
                case AvailableLocations.Overworld:
                    m_headerText.text = "You can now travel the Overworld";
                    m_promptSetter.SetText("Hold BUTTONPROMPT to access the Overworld.", input);
                    break;
                case AvailableLocations.Throne_Room:
                    m_headerText.text = "You can now travel back to your Throne Room";
                    m_promptSetter.SetText("Hold BUTTONPROMPT to Teleport to your Throne Room.", input);
                    break;
                default:
                    m_headerText.text = "";
                    m_instructionsText.text = "";
                    break;
            }
        }


        #region Teleport Confirmation Section
        public void SetupTeleportableLocation(LocationData data)
        {
            m_destinationData = data;
            var WorldTypeVar = FindObjectOfType<WorldTypeManager>();

            //if (WorldTypeVar.CurrentWorldType != WorldTypeVar.GetLocationWorldType(m_destinationData.location))
            //{
            //    WorldTypeVar.SetCurrentWorldType(m_destinationData.location);
            //}
                //GameplaySystem.campaignSerializer.UpdateData(SerializationScope.Player);


            switch (WorldTypeVar.CurrentWorldType)
            {
                case WorldType.Underworld:
                    m_confirmationHandler.RequestConfirmation(OnConfirm, $"Teleport to the Throne Room?");
                    break;
                case WorldType.Overworld:
                    m_confirmationHandler.RequestConfirmation(OnConfirm, $"Teleport to the Overworld?");
                    break;
            }
        }

        private void OnConfirm(object sender, EventActionArgs eventArgs)
        {
            m_fastTravelHandle.TransferPlayerTo(m_destinationData);
        }
        #endregion
    }
}