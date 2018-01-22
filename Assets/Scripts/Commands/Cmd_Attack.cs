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
    private Animation idle;
    private AttackManager attackManager;

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
        attackManager = GetComponent<AttackManager>();
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
        
        base.Execute();
        if (ReturnifDead())
        {
            return;
        }
        commandManager.animator.Play(GetComponent<UnitAnimation>().CombatIdle.name);
        attackManager.Target = TargetUnit.GetComponent<UnitInfo>();
        GetComponent<TargetFinding>().Aggressive = true;
        agent.SetDestination(TargetUnit.transform.position);
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
            attackManager.Target = null;
            commandManager.NextCommand();
            return;
        }

        agent.SetDestination(TargetUnit.transform.position);
        var distance = Vector3.Distance(TargetUnit.transform.position, transform.position);
        if (distance <= (attackManager.GetMinimumWeaponRange() + 1 ))
        {
            agent.isStopped = true;
            attackManager.Attack();
        }
        else
        {
            commandManager.animator.Play(GetComponent<UnitAnimation>().CombatWalk.name);
            agent.isStopped = false;
        }
    }
    private bool ReturnifDead()
    {
        if (TargetUnit.gameObject == null)
        {
            attackManager.Target = null;
            UnitInfo newTarget = Targeting.FindTarget();
            if (newTarget != null)
            {
                commandManager.commandQueue.Insert(1, New(transform.gameObject, newTarget.gameObject, leashDistance,startingPoint));
            }
            else if (leashDistance < 10000)
            {
                commandManager.commandQueue.Insert(1, Cmd_Move.New(transform.gameObject, startingPoint));
            }
            commandManager.NextCommand();
            return true;
        }
        return false;
    }
}
