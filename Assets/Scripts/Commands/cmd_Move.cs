using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cmd_Move : Command {

    public float relaxDistance = 5f;
    public Vector3 destination;
    private NavMeshAgent agent;
    private CommandManager commandManager;

    public Cmd_Move(Vector3 prPoint, CommandManager prCommandManager, NavMeshAgent prAgent)
    {
        agent = prAgent;
        commandManager = prCommandManager;
        destination = prPoint;
    }
    public override void Execute()
    {
        agent.SetDestination(destination);
        agent.isStopped = false;
    }

    public override void CommandUpdate ()
    {
        var distance = Vector3.Distance(destination, commandManager.transform.position);
        
        if (distance <= relaxDistance)
        {
            agent.isStopped = true;
            commandManager.NextCommand();
        }
    }
}
