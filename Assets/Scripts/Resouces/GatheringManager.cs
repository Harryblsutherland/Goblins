using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringManager : MonoBehaviour
{

    public List<ResourceNode> nodes = new List<ResourceNode>();
    public float maximumUnitsPerNode = 3;


    // Use this for initialization
    void Start()
    {
        foreach (var Node in ResourceNode.allNodes)
        {
            if (Vector3.Distance(transform.position, Node.transform.position) < 100)
            {
                AddNode(Node);
            }
        }
    }

    private void AddNode(GameObject prNode)
    {
        var node = prNode.GetComponent<ResourceNode>();
        node.depot = gameObject;
        nodes.Add(node);
    }
    public GameObject ChooseNode()
    {
        ResourceNode bestNode = null;
        foreach (var node in nodes)
        {
            if (bestNode == null)
            {
                if (node.Miners.Count < maximumUnitsPerNode)
                {
                    bestNode = node;
                }
            }
            else
            {
                if (node.Miners.Count < bestNode.Miners.Count)
                {
                    bestNode = node;

                }
            }
        }
        if (bestNode == null)
        {
            return null;
        }
        return bestNode.transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
