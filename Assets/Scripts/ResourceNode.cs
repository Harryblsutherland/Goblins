﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public static List<GameObject> allNodes = new List<GameObject>();
    public int remaining = 5000;
    public GameObject Depot;
    public List<GameObject> Miners = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        allNodes.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
