using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackAction : ActionBehaviour
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
        onClickAction = () => AttackWithAllUnits();
    }

    private void AttackWithAllUnits()
    {
        var destination = (Vector3)RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition);
        if (Utilities.CheckMouseHit() != null)
        {
            var hit = (RaycastHit)Utilities.CheckMouseHit();
            foreach (var Unit in MouseManager.Current.Selections)
            {

                switch (hit.transform.gameObject.tag)
                {
                    case "Structure":
                        Unit.GetComponent<CommandManager>().AddCommand(Cmd_Attack.New(Unit.gameObject, hit.transform.gameObject));
                        break;
                    case "Unit":
                        Unit.GetComponent<CommandManager>().AddCommand(Cmd_Attack.New(Unit.gameObject, hit.transform.gameObject));
                        break;
                    case "Gold":
                        Debug.Log("itsAMine");
                        Unit.GetComponent<CommandManager>().AddCommand(Cmd_Move.New(Unit.gameObject, destination, true));
                        break;
                    default:
                        Unit.GetComponent<CommandManager>().AddCommand(Cmd_Move.New(Unit.gameObject, destination, true));
                        break;
                }
            }
        }
    }
}
