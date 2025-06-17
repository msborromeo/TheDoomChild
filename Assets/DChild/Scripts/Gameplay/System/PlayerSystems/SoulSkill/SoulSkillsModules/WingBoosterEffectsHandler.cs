using DChild.Gameplay;
using DChild.Gameplay.Characters.Players;
using Holysoft.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingBoosterEffectsHandler : MonoBehaviour
{

    [SerializeField]
    private ParticleFX m_effects;

    private void Update()
    {
        bool isLevitating=false;
        isLevitating = GameplaySystem.playerManager.player.state.isLevitating;   

        if (isLevitating == true)
        {
            m_effects.Play();
        }
        else
        {
            m_effects.Stop();
        }
    }

}
