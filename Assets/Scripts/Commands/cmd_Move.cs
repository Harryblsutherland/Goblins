using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cmd_Move : Command {

    public float relaxDistance = 5f;
    public Vector3 destination;
    private NavMeshAgent agent;
    

    public Cmd_Move(Vector3 prPoint, CommandManager prCommandManager, NavMeshAgent prAgent)
    {
        agent = prAgent;
        commandManager = prCommandManager;
        destination = prPoint;
    }

    public override void Delete()
    {
        
    }

    public override void Execute()
    {
        agent.SetDestination(destination);
        agent.isStopped = false;
    }

    public override void Pause()
    {
        agent.isStopped = true;
    }

    public override void Update ()
    {
        var distance = Vector3.Distance(destination, commandManager.transform.position);
        
        if (distance <= relaxDistance)
        {
            agent.isStopped = true;
            commandManager.NextCommand();
        }
    }
}
