using DChild.Gameplay.Characters.AI;
using DChild.Gameplay.Characters.Enemies;
using Holysoft.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGrabAttack : MonoBehaviour, IEyeBossAttacks
{
    [SerializeField]
    public TentacleGrab tentacleGrab;

    public event EventAction<EventActionArgs> AttackStart;
    public event EventAction<EventActionArgs> AttackDone;

    private void Awake()
    {
        tentacleGrab.AttackDone += OnGrabAttackDone;
    }

    private void OnDisable()
    {
        tentacleGrab.AttackDone -= OnGrabAttackDone;
    }

    private void OnGrabAttackDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Grab Attack Done from TentacleGrabAttack");
        AttackDone?.Invoke(this, EventActionArgs.Empty);
    }

    public IEnumerator ExecuteAttack()
    {
        //Make it so different types of grab attacks can happen later
        yield return tentacleGrab.GroundSlamRoutine();
    }

    public IEnumerator ExecuteAttack(Vector2 PlayerPosition)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator ExecuteAttack(AITargetInfo Target)
    {
        throw new System.NotImplementedException();
    }
}
