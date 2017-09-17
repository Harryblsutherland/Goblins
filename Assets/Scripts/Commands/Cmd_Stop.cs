using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_Stop : Command {
    public override void CommandUpdate()
    {
        commandManager.NextCommand();
    }

    public override void Delete()
    {
    }

    public override void Execute()
    {
    }
}
