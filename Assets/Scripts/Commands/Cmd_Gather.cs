using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cmd_Gather : Command
{
    public GameObject initialMine;
    public GameObject resourceDepot;
    public ResourceNode resourceNode;
    public NavMeshAgent agent;
    public int GatherAmount;
    private bool collected = false;
    private float relaxDistance = 10;
    private float AmountGathered;
    private float originalRadius;
    private Action gatherAction;
    private Action cancelAction;


    public static Cmd_Gather New(GameObject prGameObject, GameObject prMine)
    {
        Cmd_Gather newcommand = prGameObject.AddComponent<Cmd_Gather>();
        newcommand.initialMine = prMine;
        newcommand.resourceDepot = prMine.GetComponent<ResourceNode>().depot;

        return newcommand;
    }
    public override void Awake()
    {
        base.Awake();
        gatherAction = () => {CollectResource();};
        cancelAction = () => {resourceNode.mineable = true;};
        agent = GetComponent<NavMeshAgent>();
        relaxDistance = 10;
        GatherAmount = 5;
        originalRadius = agent.radius;
        agent.radius = 0.1f;
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

    public bool canmine()
    {
        
        return false;
    }
    private ResourceNode FindNode()
    {
        var tmpNode = initialMine.GetComponent<ResourceNode>().depot.GetComponent<GatheringManager>().ChooseNode().GetComponent<ResourceNode>();
        tmpNode.Miners.Add(gameObject);
        return tmpNode;
    }

    public override void CommandUpdate()
    {

        var distanceToNode = Vector3.Distance(resourceNode.transform.position, commandManager.transform.position);
        var distancetoDepot = Vector3.Distance(resourceDepot.transform.position, commandManager.transform.position);

        if (distanceToNode <= relaxDistance && !collected)
        {
            if (resourceNode.mineable)
            {
                resourceNode.mineable = false;
                commandManager.InsertCommand(Cmd_Channel.New(transform.gameObject, "Attack", 1, 0.9f, resourceNode.transform.position, gatherAction, cancelAction));
            }
            return;
        }
        else if (distancetoDepot <= relaxDistance && collected)
        {
            GetComponent<Player>().Info.Credits += AmountGathered;
            AmountGathered = GatherAmount;
            collected = false;
            return;
        }
        else if (distancetoDepot <= relaxDistance)
        {
            commandManager.InsertCommand(Cmd_Move.New(transform.gameObject, resourceNode.transform.position));
            return;
        }
        else if (distanceToNode <= relaxDistance)
        {
            commandManager.InsertCommand(Cmd_Move.New(transform.gameObject, resourceDepot.transform.position));
            return;
        }
    }

    public override void Delete()
    {
        agent.radius = originalRadius;
        resourceNode.Miners.Remove(gameObject);
    }
    public void CollectResource()
    {
        resourceNode.mineable = true;
        resourceNode.remaining -= GatherAmount;
        collected = true;
        AmountGathered = GatherAmount;
    }

}
