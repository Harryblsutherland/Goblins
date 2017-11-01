using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_Stop : Command {

    public static Cmd_Stop New(GameObject prGameObject)
    {
        Cmd_Stop newcommand = prGameObject.AddComponent<Cmd_Stop>();
        return newcommand;
    }
    public override void CommandUpdate()
    {
        commandManager.NextCommand();
    }

    public override void Delete()
    {
    }

    public override void Execute()
    {
        commandManager.animator.Play(GetComponent<UnitAnimation>().Idle.name);
    }
}