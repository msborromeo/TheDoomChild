using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Combat;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Gameplay;

public class ElementalSwordController : MonoBehaviour
{ 
    private PlayerWeapon m_playerWeapon;
    [SerializeField]
    private ElementalSwordFX[] m_elementalSwordFX;
    private DamageType currentDamageType;

    private void Start()
    {
        m_playerWeapon = GameplaySystem.playerManager.player.weapon;
        if (m_playerWeapon != null)
        {
            m_playerWeapon.DamageChange += OnDamageChange;
            UpdateElementalFX();
        }
    }

    private void OnDestroy()
    {
        if (m_playerWeapon != null)
        {
            m_playerWeapon.DamageChange -= OnDamageChange;
        }
    }

    private void OnDamageChange(object sender, EventActionArgs args)
    {
        UpdateElementalFX();
    }
    private void UpdateElementalFX()
    {
        currentDamageType = m_playerWeapon.damage.type;
        foreach(var fx in m_elementalSwordFX)fx.Stop();
        switch (currentDamageType)
        {
            case DamageType.Physical:
                foreach (var fx in m_elementalSwordFX) fx.SetElementTo(ElementalSwordFX.Element.Physical);
                break;
            case DamageType.Fire:
                foreach (var fx in m_elementalSwordFX) fx.SetElementTo(ElementalSwordFX.Element.Fire);
                break;
            case DamageType.Ice:
                foreach (var fx in m_elementalSwordFX) fx.SetElementTo(ElementalSwordFX.Element.Ice);
                break;
            case DamageType.Lightning:
                foreach (var fx in m_elementalSwordFX) fx.SetElementTo(ElementalSwordFX.Element.Lightning);
                break;
        }
    }
}
