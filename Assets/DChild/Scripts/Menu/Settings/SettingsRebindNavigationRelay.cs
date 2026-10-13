using UnityEngine;

namespace DChild.Menu.UI
{
    // Called by the rebind UI's existing start/stop events, without depending on its sample assembly.
    public class SettingsRebindNavigationRelay : MonoBehaviour
    {
        public void BeginRebind() => GetComponentInParent<SettingsNavigationController>()?.BeginRebind();
        public void EndRebind() => GetComponentInParent<SettingsNavigationController>()?.EndRebind();
    }
}
