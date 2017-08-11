using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Cmd_Patrol : Command {


    public float relaxDistance = 2f;
    private Vector3 pointA;
    private Vector3 pointB;
    private NavMeshAgent agent;
    private CommandManager commandManager;

    public Cmd_Patrol(Vector3 prPoint, CommandManager prCommandManager, NavMeshAgent prAgent)
    {
        agent = prAgent;
        commandManager = prCommandManager;
        pointA = prPoint;
    }
    public override void Execute()
    {
        pointB = commandManager.transform.position;
        agent.SetDestination(pointA);
        agent.isStopped = false;
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
}
