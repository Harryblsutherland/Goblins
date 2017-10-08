using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : ActionBehaviour
{

    public Action onClickAction;
    public override Action GetClickAction()
    {
        return delegate ()
        {
            ClickConfirmation.current.StartClickConfirmation(transform.gameObject, onClickAction);
        };
    }

    void Awake()
    {
        onClickAction = () => MoveallSelectedUnits();
    }

    private void MoveallSelectedUnits()
    {
        var destination = (Vector3)RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition);
        foreach (var Unit in MouseManager.Current.Selections)
        {
            Unit.GetComponent<CommandManager>().AddCommand(Cmd_Move.New(Unit.gameObject, destination));
        }
    }

}

