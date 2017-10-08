using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAction : ActionBehaviour {

    public Action onClickAction;
    public override Action GetClickAction()
    {
        return delegate ()
        {
            onClickAction();
        };
    }

    void Awake()
    {
        onClickAction = () => StopAllUnits();
    }

    private void StopAllUnits()
    {
       
        foreach (var Unit in MouseManager.Current.Selections)
        {
            Unit.GetComponent<CommandManager>().AddCommand(Cmd_Stop.New(Unit.gameObject));
        }
    }
}
