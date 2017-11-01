using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIB_DoNothing : AIBehaviour
{
    public float ReturnWeight = 0.5f;
    private aiSupport support;


    public override void Execute()
    {
        support.Player.Credits += 5 + (support.warrens.Count * 2);
    }

    public override float GetWeight()
    {
        if (support == null)
        {
            support = aiSupport.GetSupport(gameObject);
        }
        return ReturnWeight;
    }
}
