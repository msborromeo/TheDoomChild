using Doozy.Runtime.Signals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using I2.Loc;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class ArmyBattlePopupUI : MonoBehaviour
    {
        [SerializeField]
        private string m_signalCategory;
        [SerializeField]
        private string m_signalName;
        [SerializeField]
        private TextMeshProUGUI m_popupLabel;


        private SignalReceiver m_signalReceiver;
        private SignalStream m_signalStream;

        [TermsPopup]
        public string m_victoryText;
        [TermsPopup]
        public string m_defeatText;
        [TermsPopup]
        public string m_battleText;

        private void Awake()
        {
            m_signalStream = SignalStream.Get(m_signalCategory, m_signalName);
            m_signalReceiver = new SignalReceiver().SetOnSignalCallback(OnSignal);
        }

        private void OnEnable()
        {
            m_signalStream.ConnectReceiver(m_signalReceiver);
        }

        private void OnDisable()
        {
            m_signalStream.DisconnectReceiver(m_signalReceiver);
        }

        private void OnSignal(Signal signal)
        {

            if (signal.valueType != typeof(bool))
            {
                //m_popupLabel.text = "BATTLE";
                m_popupLabel.text = LocalizationManager.GetTranslation(m_battleText);
                return;
            }

            bool battleResult = (bool)signal.valueAsObject;

            if (battleResult != true)
            {
                //m_popupLabel.text = "DEFEAT";
                m_popupLabel.text = LocalizationManager.GetTranslation(m_defeatText);
                return;
            }
            //m_popupLabel.text = "VICTORY";
            m_popupLabel.text = LocalizationManager.GetTranslation(m_victoryText);
        }
    }
}