using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIB_CreateBase : AIBehaviour {

    public float cost = 199;

    public int unitsPerBase = 6;

    public int buildingRange = 30;

    public int attemptsPerDrone = 5;

    public GameObject buildingPrefab;

    private aiSupport support = null;

 

    public override void Execute()
    {
       // Debug.Log("creating Base");

        var go = Instantiate(buildingPrefab);
        go.AddComponent<Player>().Info = support.Player;

        foreach (var goblin in support.goblins)
        {
                for (int i = 0;i < attemptsPerDrone; i++)
            {
                var pos = goblin.transform.position;
                pos += UnityEngine.Random.insideUnitSphere * buildingRange;
                pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.transform.position.y;
                go.transform.position = pos;

                if (RtsManager.Current.IsGameObjectSafeToPlace(go) && support.Player.Credits > cost)
                {
                    support.Player.Credits -= cost;
                    return;
                }
            }
        }
        Destroy(go);
    }

    public override float GetWeight()
    {
        if (support == null)
        {
            support = aiSupport.GetSupport(gameObject);
        }
        if (support.goblins.Count == 0 || support.Player.Credits < cost)
        {
            return 0;
        }
        if (support.warrens.Count * unitsPerBase <= support.goblins.Count)
        {
            return 1;
        }
        return 0;
        

    }

   
}
