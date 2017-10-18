using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cmd_Move : Command
{
    public float relaxDistance = 0;
    public Vector3 destination;
    private NavMeshAgent agent;
    public bool aggression;
    public string animationclip;
    /// <summary>
    /// This command will move the unit to a given place then progress to the next. this command disables targeting and just moves the unit.
    /// </summary>


    public static Cmd_Move New(GameObject prGameObject, Vector3 prPoint)
    {
        Cmd_Move newcommand = prGameObject.AddComponent<Cmd_Move>();
        newcommand.destination = prPoint;

        return newcommand;
    }
    public static Cmd_Move New(GameObject prGameObject, Vector3 prPoint,float prRelaxDistance)
    {
        Cmd_Move newcommand = prGameObject.AddComponent<Cmd_Move>();
        newcommand.destination = prPoint;
        newcommand.relaxDistance = prRelaxDistance;

        return newcommand;
    }
    public static Cmd_Move New(GameObject prGameObject, Vector3 prPoint,bool Agressive)
    {
        Cmd_Move newcommand = prGameObject.AddComponent<Cmd_Move>();
        newcommand.destination = prPoint;
        newcommand.aggression = Agressive;
        return newcommand;
    }
    public override void Awake()
    {
        base.Awake();
        relaxDistance = 5f;
        agent = GetComponent<NavMeshAgent>();
        aggression = false;
        
    }
    public override void Execute()
    {
        commandManager.animator.Play(GetComponent<UnitAnimation>().Run.name);
        agent.SetDestination(destination);
        agent.isStopped = false;
        Targeting.Target = null;
        Targeting.Aggressive = aggression;
    }

    public override void Pause()
    {
        agent.isStopped = true;
    }

    public override void CommandUpdate()
    {
        var distance = Vector3.Distance(destination, transform.position);

        if (distance <= relaxDistance)
        {
            agent.isStopped = true;

            commandManager.NextCommand();
            Targeting.Aggressive = true;
            aggression = false;

            Destroy(this);
        }
    }
    public override void Delete()
    {
        agent.isStopped = true;
    }
}
