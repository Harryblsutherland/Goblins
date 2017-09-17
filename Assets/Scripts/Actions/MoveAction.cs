using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : ActionBehaviour
{

    public System.Action onClickAction;
    public override Action GetClickAction()
    {
        return delegate ()
        {
            ClickConfirmation.current.StartClickConfirmation(transform.gameObject, onClickAction);

        };
    }

    void Awake()
    {
        // ButtonIcon = Resources.Load("moveicon") as Sprite;
        onClickAction = () => { GetComponent<CommandManager>().AddCommand(Cmd_Move.New(transform.gameObject, (Vector3)RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition))); };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
