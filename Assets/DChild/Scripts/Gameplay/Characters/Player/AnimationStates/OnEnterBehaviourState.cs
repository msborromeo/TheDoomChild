using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players
{
    public class OnEnterBehaviourState : StateMachineBehaviour
    {
        private enum Command
        {
            SlashStateOne,
            SlashStateTwo,
            SlashStateThree,
            WhipComboStateOne,
            WhipComboStateTwo,
            WhipComboStateThree,
        }

        [SerializeField]
        private Command m_toExecute;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            var player = animator.GetComponent<PlayerFunctions>();
            Debug.Log("Search for player function");
            if (player != null)
            {
                switch (m_toExecute)
                {
                    //nothing as of now as this is only being used for Phantom for now - 7/7/25
                }
            }
            else
            {
                var shadow = animator.GetComponent<PhantomFunctions>();

                switch (m_toExecute)
                {
                    case Command.SlashStateOne:
                        shadow.SlashComboOn(0);
                        break;
                    case Command.SlashStateTwo:
                        shadow.SlashComboOn(1);
                        break;
                    case Command.SlashStateThree:
                        shadow.SlashComboOn(2);
                        break;
                    case Command.WhipComboStateOne:
                        shadow.WhipComboOn(0);
                        break;
                    case Command.WhipComboStateTwo:
                        shadow.WhipComboOn(1);
                        break;
                    case Command.WhipComboStateThree:
                        shadow.WhipComboOn(2);
                        break;

                }
            }
        }
    }

}
