using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cmd_Gather : Command
{
    public GameObject initialMine;
    public GameObject resourceDepot;
    public GameObject resourceNode;
    public NavMeshAgent agent;
    private bool collected = false;
    private float relaxDistance = 5;
    private float AmountGathered;
    private Action gatherAction;



    public static Cmd_Gather New(GameObject prGameObject, GameObject prMine)
    {
        Cmd_Gather newcommand = prGameObject.AddComponent<Cmd_Gather>();
        newcommand.initialMine = prMine;
        newcommand.resourceDepot = prMine.GetComponent<ResourceNode>().Depot;
    
        return newcommand;
    }
    public override void Awake()
    {
        base.Awake();
        gatherAction = () => { this.CollectResource(); };
        agent = GetComponent<NavMeshAgent>();

    }
    public override void Execute()
    {
        if (Vector3.Distance(resourceDepot.transform.position, transform.position) > relaxDistance && !paused)
        {
            commandManager.InsertCommand(Cmd_Move.New(transform.gameObject, resourceDepot.transform.position));
            return;
        }
        if (resourceNode == null)
        {
            resourceNode = FindNode();
        }

    }

    private GameObject FindNode()
    {
        var tmpNode = initialMine.GetComponent<ResourceNode>().Depot.GetComponent<GatheringManager>().ChooseNode();
        return tmpNode;
    }

    public override void CommandUpdate()
    {
        
        var distanceToNode = Vector3.Distance(resourceNode.transform.position, commandManager.transform.position);
        var distancetoDepot = Vector3.Distance(resourceDepot.transform.position, commandManager.transform.position);
        if (distanceToNode <= relaxDistance && !collected)
        {
            agent.isStopped = true;
            commandManager.InsertCommand(Cmd_Channel.New(transform.gameObject, "Attack", 2, 1.9f, gatherAction));
            return;
        }

        if (distancetoDepot <= relaxDistance && collected)
        {
            GetComponent<Player>().Info.Credits += AmountGathered;
            AmountGathered = 0;
            collected = false;
            return;
        }

        if (distancetoDepot <= relaxDistance)
        {
            agent.SetDestination(resourceNode.transform.position);
            agent.isStopped = false;
        }
        else if (distanceToNode <= relaxDistance)
        {
            agent.SetDestination(resourceDepot.transform.position);
            agent.isStopped = false;
        }
    }

    public override void Delete()
    {
    }
    public void CollectResource()
    {
        collected = true;
        AmountGathered = 5;
    }
}
