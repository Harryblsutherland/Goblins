using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cmd_Follow : Command
{

    public GameObject followedUnit;
    public NavMeshAgent agent;
    private float relaxDistance;

    /// <summary>
    /// This Command has a unit follow another target Unit. it is an indefinite commandand will never move to the next command unless the command queue is flushed.
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        relaxDistance = 5f;
        agent = GetComponent<NavMeshAgent>();
    }

    public override void Execute()
    {
        GetComponent<AttackInRange>().Aggressive = false;
        agent.SetDestination(followedUnit.transform.position);
        agent.isStopped = false;
    }

    public override void CommandUpdate()
    {
        agent.SetDestination(followedUnit.transform.position);
        var distance = Vector3.Distance(followedUnit.transform.position, transform.position);

        if (distance <= relaxDistance)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }

    public override void Delete()
    {

    }






}
