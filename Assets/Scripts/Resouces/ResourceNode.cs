using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public static List<GameObject> allNodes = new List<GameObject>();
    public int remaining = 5000;
    public GameObject depot;
    public bool mineable;
    public List<GameObject> Miners = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        mineable = true;
        allNodes.Add(gameObject);
    }

    public bool canmine()
    { 
        if (depot == null)
        {
            return false;
        }
        float maxPerNode = depot.GetComponent<GatheringManager>().maximumUnitsPerNode;

        foreach (var node in depot.GetComponent<GatheringManager>().nodes)
        {
            if (node.Miners.Count < maxPerNode)
            {
                return true;
            }
        }
        return false;
    }
}
