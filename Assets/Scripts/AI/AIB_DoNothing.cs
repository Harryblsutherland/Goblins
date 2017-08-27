using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIB_DoNothing : AIBehaviour
{
    public float ReturnWeight = 0.5f;

    public override void Execute()
    {
      //  Debug.Log("DoingNothing");
    }

    public override float GetWeight()
    {
      return ReturnWeight;
    }
}
