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
        support.Player.Credits += support.warrens.Count * 10;
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
