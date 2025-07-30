using DChild.Gameplay;
using DChild.Gameplay.Systems.Serialization;
using UnityEngine;

namespace DChild.Gameplay.Environment
{
    public enum TransitionType
    {
        Enter,
        PostEnter,
        Exit,
        PostExit
    }

    public interface ISwitchHandle
    {
        void SetLocationDataReference(LocationData locationData);

        bool isDebugSwitchHandle { get; }

        void DoSceneTransition(Character character, TransitionType type);
        float transitionDelay { get; }

        bool needsButtonInteraction { get; }
        Vector3 promptPosition { get; }

        string prompMessage { get; }
    } 
}
