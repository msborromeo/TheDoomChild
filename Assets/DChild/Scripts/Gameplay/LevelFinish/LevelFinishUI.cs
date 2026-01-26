using DChild.Gameplay.FastTravel;
using DChild.Gameplay.Systems;
using DChild.Gameplay.Systems.Serialization;
using DChild.Gameplay.UI;
using DChild.Menu;
using Holysoft.Event;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
namespace DChild.Gameplay.LevelFinish.UI
{
    public class LevelFinishUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_unlockedLocationLabel;
        [SerializeField]
        private TextMeshProUGUI m_headerDetailsText;
        [SerializeField]
        private TextMeshProUGUI m_instructionsText;
        [SerializeField]
        private SetTextToTextBox m_promptSetter;
        //fastest way to teleport player at the moment
        [SerializeField]
        private ConfirmationHandler m_confirmationHandler;
        [SerializeField]
        private FastTravelHandle m_fastTravelHandle;
        [SerializeField]
        private LocationInWorldData m_underworldLocations;

        private LocationData m_destinationData;


        [Button]
        public void NotifyAvailableLocation(AvailableLocations location, InputActionConfiguration input)
        {
            m_unlockedLocationLabel.text = $"{location}";
            switch (location)
            {
                case AvailableLocations.Overworld:
                    m_headerDetailsText.text = "You can now travel to Overworld";
                    m_promptSetter.SetText("Hold BUTTONPROMPT to warp to the Overworld.", input);
                    break;
                case AvailableLocations.Throne_Room:
                    m_headerDetailsText.text = "You can now travel back to your Throne Room";
                    m_promptSetter.SetText("Hold BUTTONPROMPT to warp to your Throne Room.", input);
                    break;
                default:
                    m_headerDetailsText.text = "";
                    m_instructionsText.text = "";
                    break;
            }
        }


        #region Teleport Confirmation Section
        public void SetupTeleportableLocation(LocationData data)
        {
            m_destinationData = data;
            var header = "Teleport";
            if (m_underworldLocations.Locations.Contains(data.location))
            {
                m_confirmationHandler.RequestConfirmation(OnConfirm, header, $"Teleport to the Throne Room?", OnDecline: OnDecline);
            }
            else
            {
                m_confirmationHandler.RequestConfirmation(OnConfirm, header, $"Teleport to the Overworld?", OnDecline: OnDecline);
            }
        }

        private void OnConfirm(object sender, EventActionArgs eventArgs)
        {
            m_fastTravelHandle.TransferPlayerTo(m_destinationData);
            GameplaySystem.ResumeGame();
        }

        private void OnDecline(object sender, EventActionArgs eventArgs)
        {
            GameplaySystem.ResumeGame();
        }

        #endregion
    }
}