using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Combat;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ZeeMaxedOutSoloLevelingHeltiSaTomiSkyTown : MonoBehaviour
{
    private BasicHealth m_zeeHealth;
    private PlayerSkills m_skills;
    private PrimarySkill m_listOfSkills = PrimarySkill.All;

    private void Awake()
    {
        var playerSkills = GameObject.Find("Progression");
        m_skills = playerSkills.GetComponent<PlayerSkills>();
        var zeeHealth = GameObject.Find("Zee").GetComponentInChildren<BasicHealth>();
        m_zeeHealth = zeeHealth;
    }
    private void Start()
    {
        m_skills.SetSkillStatus(m_listOfSkills, true);
        StartCoroutine(SetZeeHealthWithDelay());
    }
    private IEnumerator SetZeeHealthWithDelay()
    {
        yield return new WaitForEndOfFrame();
        m_zeeHealth.SetMaxValue(2147483647);
        m_zeeHealth.ResetValueToMax();
    }
}
