using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : ActionBehaviour {

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
        onClickAction = () => PatrolAllSelectedUnits();
    }

    private void PatrolAllSelectedUnits()
    {
        var destination = (Vector3)RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition);
        foreach (var Unit in MouseManager.Current.Selections)
        {
            Unit.GetComponent<CommandManager>().AddCommand(Cmd_Patrol.New(Unit.gameObject, destination));
        }
    }
}
