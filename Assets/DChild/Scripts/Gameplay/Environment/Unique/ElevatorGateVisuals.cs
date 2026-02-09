using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Environment.VisualConfigurators
{
    public class ElevatorGateVisuals : MonoBehaviour
    {
        [SerializeField]
        private MovingPlatform m_elevator;
        [SerializeField]
        private SkeletonAnimation m_elevatoranimation;
        [SerializeField, HorizontalGroup("Up"), ShowIf("m_elevatoranimation"), Spine.Unity.SpineAnimation(dataField = "m_elevatoranimation.skeletonDataAsset")]
        private string m_gateupAnimation;
        [SerializeField, HorizontalGroup("Down"), ShowIf("m_elevatoranimation"), Spine.Unity.SpineAnimation(dataField = "m_elevatoranimation.skeletonDataAsset")]
        private string m_gatedownAnimation;
       


        [Button]
        public void gateUp()
        {
            m_elevatoranimation.state.SetAnimation(2, m_gateupAnimation, false);
        }
        [Button]
        public void gateDown()
        {
            m_elevatoranimation.state.SetAnimation(2, m_gatedownAnimation, false);

        }

        private void Start()
        {
            m_elevator.DestinationReached += OnDestinationReached;
            m_elevator.DestinationChanged += OnDestinationChanged;
        }

        private void OnDisable()
        {
            m_elevator.DestinationReached -= OnDestinationReached;
            m_elevator.DestinationChanged -= OnDestinationChanged;
        }

        private void OnDestinationChanged(object sender, MovingPlatform.UpdateEventArgs eventArgs)
        {
            gateUp();

        }

        private void OnDestinationReached(object sender, MovingPlatform.UpdateEventArgs eventArgs)
        {
            gateDown();
        }
    }
}
