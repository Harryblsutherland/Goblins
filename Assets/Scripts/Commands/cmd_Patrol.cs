using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Cmd_Patrol : Command
{

    public float relaxDistance = 2f;
    public Vector3 pointA;
    private Vector3 pointB;
    private NavMeshAgent agent;
    /// <summary>
    /// this command will move between the two given points indefinitely. it enables targeting.
    /// </summary>

    public static Cmd_Patrol New(GameObject prGameObject, Vector3 prPoint)
    {
        Cmd_Patrol newcommand = prGameObject.AddComponent<Cmd_Patrol>();
        newcommand.pointA = prPoint;

        return newcommand;
    }

    public override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        pointB = commandManager.transform.position;
    }

    public override void Execute()
    {
        agent.SetDestination(pointA);
        agent.isStopped = false;
        Targeting.Aggressive = true;
    }

    public override void Pause()
    {
        agent.isStopped = true;
    }

    public override void CommandUpdate()
    {
        var distancetoA = Vector3.Distance(pointA, commandManager.transform.position);
        var distancetoB = Vector3.Distance(pointB, commandManager.transform.position);
        if (distancetoA <= relaxDistance)
        {
            agent.SetDestination(pointB);
            agent.isStopped = false;
        }
        else if (distancetoB <= relaxDistance)
        {
            agent.SetDestination(pointA);
            agent.isStopped = false;
        }
    }
    public override void Delete()
    {
    }
}
