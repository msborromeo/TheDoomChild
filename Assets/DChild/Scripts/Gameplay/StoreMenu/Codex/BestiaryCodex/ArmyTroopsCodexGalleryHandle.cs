using DChild.Codex.Characters;
using DChild.Gameplay.ArmyBattle;
using UnityEngine;

namespace DChild.Menu.Codex.ArmyTroops
{
    public class ArmyTroopsCodexGalleryHandle : CodexGalleryHandle<ArmyGroupTemplateData, CharacterCodexProgressTracker>
    {
        public override void Awake()
        {
            if (m_gallery is CodexArmyTroopsGalleryUI troopsGallery &&
                m_popupPage is ArmyTroopsCodexGalleryPopupInfoUI troopsPopup)
            {
                troopsGallery.OnCodexDataReceived -= troopsPopup.OnCodexDatasReceived;
                m_gallery.OnGalleryEntryReceived -= m_popupPage.ShowInfo;

                troopsGallery.OnCodexDataReceived += troopsPopup.OnCodexDatasReceived;
                m_gallery.OnGalleryEntryReceived += m_popupPage.ShowInfo;
            }
        }

        //private void OnDisable()
        //{
        //    if (m_gallery is CodexArmyTroopsGalleryUI troopsGallery &&
        //        m_popupPage is ArmyTroopsCodexGalleryPopupInfoUI troopsPopup)
        //    {
        //        troopsGallery.OnCodexDataReceived -= troopsPopup.OnCodexDatasReceived;
        //        m_gallery.OnGalleryEntryReceived -= m_popupPage.ShowInfo;
        //    }
        //}
    }
}