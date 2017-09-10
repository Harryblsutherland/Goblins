using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cmd_Attack : Command {

    public GameObject TargetUnit;
    public NavMeshAgent agent;
    public AttackInRange Targeting;
    private float relaxDistance;

    public override void Awake()
    {
        base.Awake();
        relaxDistance = 5f;
        agent = GetComponent<NavMeshAgent>();
        Targeting = GetComponent<AttackInRange>();
    }

    public override void Delete()
    {

    }

    public override void Pause()
    {
        base.Pause();
        agent.isStopped = true;
    }
    public override void Execute()
    {
        Targeting.target = TargetUnit.GetComponent<UnitInfo>();
        GetComponent<AttackInRange>().Aggressive = true;
        agent.SetDestination(TargetUnit.transform.position);
        agent.isStopped = false;
    }

	public override void CommandUpdate()
    {
        agent.SetDestination(TargetUnit.transform.position);
        var distance = Vector3.Distance(TargetUnit.transform.position, transform.position);

        if (distance <= Targeting.GetMinimumWeaponRange())
        {
            agent.isStopped = true;
            Targeting.Attack();
            Debug.Log("FIRE ze Missile");

        }
        else
        {
            agent.isStopped = false;
        }
    }
}
