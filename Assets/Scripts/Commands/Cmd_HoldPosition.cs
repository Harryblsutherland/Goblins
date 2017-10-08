using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cmd_HoldPosition : Command
{
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;

    public static Cmd_HoldPosition New(GameObject prGameObject)
    {
        Cmd_HoldPosition newcommand = prGameObject.AddComponent<Cmd_HoldPosition>();
        return newcommand;
    }
    public override void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
    }
    public override void CommandUpdate()
    {
    }

    public override void Delete()
    {
        agent.enabled = true;
        obstacle.enabled = false;
    }

    public override void Execute()
    {
        commandManager.animator.Play(GetComponent<UnitAnimation>().Idle.name);
        agent.enabled = false;
        obstacle.enabled = true;
    }
}
