using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cmd_Attack : Command
{

    public float leashDistance = 10000;
    public GameObject TargetUnit;
    public NavMeshAgent agent;
    private float relaxDistance;
    private Vector3 startingPoint;

    public static Cmd_Attack New(GameObject prGameObject, GameObject prTargetUnit)
    {
        Cmd_Attack newCommand = prGameObject.AddComponent<Cmd_Attack>();
        newCommand.TargetUnit = prTargetUnit;

        return newCommand;
    }

    public static Cmd_Attack New(GameObject prGameObject, GameObject prTargetUnit, float prLeashDistance)
    {
        Cmd_Attack newCommand = prGameObject.AddComponent<Cmd_Attack>();
        newCommand.TargetUnit = prTargetUnit;
        newCommand.leashDistance = prLeashDistance;

        return newCommand;
    }
    public static Cmd_Attack New(GameObject prGameObject, GameObject prTargetUnit, float prLeashDistance, Vector3 prStartingLocation)
    {
        Cmd_Attack newCommand = prGameObject.AddComponent<Cmd_Attack>();
        newCommand.TargetUnit = prTargetUnit;
        newCommand.leashDistance = prLeashDistance;
        newCommand.startingPoint = prStartingLocation;
        return newCommand;
    }

    public override void Awake()
    {
        base.Awake();
        relaxDistance = 5f;
        startingPoint = transform.position;
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
        if (ReturnifDead())
        {
            return;
        }

        Targeting.Target = TargetUnit.GetComponent<UnitInfo>();
        GetComponent<AttackInRange>().Aggressive = true;
        agent.SetDestination(TargetUnit.transform.position);
        agent.isStopped = false;
    }

    public override void CommandUpdate()
    {
        if (ReturnifDead())
        {
            return;
        }
        if (Vector3.Distance(transform.position, startingPoint) > leashDistance)
        {
            commandManager.commandQueue.Insert(1, Cmd_Move.New(transform.gameObject, startingPoint));
            Targeting.Target = null;
            commandManager.NextCommand();
            return;
        }

        agent.SetDestination(TargetUnit.transform.position);
        var distance = Vector3.Distance(TargetUnit.transform.position, transform.position);
        if (distance <= (Targeting.GetMinimumWeaponRange() - 0.1))
        {
            agent.isStopped = true;
            Targeting.Attack();
        }
        else
        {
            agent.isStopped = false;
        }
    }
    private bool ReturnifDead()
    {
        if (TargetUnit.gameObject == null)
        {
            var newTarget = Targeting.FindNearestEnemyInRange();
            if (newTarget != null)
            {
                commandManager.commandQueue.Insert(1, Cmd_Attack.New(transform.gameObject,newTarget.gameObject,leashDistance,startingPoint));
            }
            else if (leashDistance < 10000)
            {
                commandManager.commandQueue.Insert(1, Cmd_Move.New(transform.gameObject, startingPoint));
            }
            Targeting.Target = null;
            commandManager.NextCommand();
            return true;
        }
        return false;
    }
}
