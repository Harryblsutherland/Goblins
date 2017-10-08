using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HoldPositionAction : ActionBehaviour {

    public Action onClickAction;

    public void Awake()
    {
        var obstacle = gameObject.AddComponent<NavMeshObstacle>();
        obstacle.enabled = false;
        onClickAction = () => HoldPositionAllUnits();
    }
    public override Action GetClickAction()
    {
        return delegate ()
        {
            onClickAction();
        };
    }



    private void HoldPositionAllUnits()
    {
        foreach (var Unit in MouseManager.Current.Selections)
        {
            Unit.GetComponent<CommandManager>().AddCommand(Cmd_HoldPosition.New(Unit.gameObject));
        }
    }

}
