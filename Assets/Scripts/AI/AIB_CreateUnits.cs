using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIB_CreateUnits : AIBehaviour
{
    public int unitsPerBase = 7;
    public float unitCost = 50;
    private aiSupport support;
    

    public override void Execute()
    {
        //Debug.Log("Creating a drone.");
        var bases = support.warrens;
        var index = UnityEngine.Random.Range(0, bases.Count);
        var BuildingStructure = bases[index];
        BuildingStructure.GetComponent<CreateUnitAction>().GetClickAction() (); 
     
    }

    public override float GetWeight()
    {
        if (support == null)
        {
            support = aiSupport.GetSupport(gameObject);
        }
        if (support.Player.Credits < unitCost)
        {
            return 0;
        }

        var goblins = support.peasants.Count;
        var warrens = support.warrens.Count;

        if (warrens == 0 || goblins >= warrens * unitsPerBase)
        {
            return 0;
        }

        return 1;
    }
}
