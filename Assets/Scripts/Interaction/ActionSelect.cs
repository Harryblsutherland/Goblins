using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelect : Interaction
{

    public override void Deselect()
    {
        ActionManager.Current.ClearButton();
    }

    public override void Select()
    {
        ActionManager.Current.ClearButton();
        foreach (var actionBehaviour in GetComponents<ActionBehaviour>())
        {
            ActionManager.Current.AddButton(
                                            actionBehaviour.ButtonIcon,
                                            actionBehaviour.GetClickAction()
                                            );

        }
    }
}
