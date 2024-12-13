using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Animators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class ArmyCommandNavigationAnimationSignal : MonoBehaviour
    {
        [SerializeField]
        private UISelectableUIAnimator m_animator;
        [SerializeField]
        private KeyCode m_keyCode;

        private void Animate()
        {
            if(Input.GetKeyDown(m_keyCode))
            {
                m_animator.Play(UISelectionState.Pressed);
            }
        }
    }

}
