using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIB_AttackWave : AIBehaviour  {

    public int unitsRequired;
    public float timeDelay = 10;
    public float attackWaveSize = 0.5f;
    public int increasePerWave = 10;

    public override void Execute()
    {
        var ai = aiSupport.GetSupport(this.gameObject);
       // Debug.Log(ai.Player.Name + "is moving to attack");
        int wave = (int)(ai.goblins.Count * attackWaveSize);
        unitsRequired += increasePerWave;
    
        foreach (var Player in RtsManager.Current.Players)
        {
            if (Player.IsAi)
                continue;
            for (int i = 0; i < wave; i++)
            {
                var Unit = ai.goblins[i];
                var nav = Unit.GetComponent<RightClickNavigation>();
                nav.SendToTarget(Player.Location.position);
            }
            return;
        }
    }

    public override float GetWeight()
    {
        if (TimePassed < timeDelay)
        {
            return 0;
        }
        var ai = aiSupport.GetSupport(this.gameObject);

        TimePassed = 0;
        if (ai.goblins.Count < unitsRequired)
        {
            return 0;
        }
        return 1;

    }


}
