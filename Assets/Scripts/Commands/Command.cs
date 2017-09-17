﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    public bool paused;
    public CommandManager commandManager;
    public AttackInRange Targeting;

    public virtual void Awake()
    {
        paused = false;
        commandManager = GetComponent<CommandManager>();
        Targeting = GetComponent<AttackInRange>();
        //Targeting.Aggressive = false;
    }
    public virtual void Pause()
    {
        paused = true;
    }
    public abstract void Execute();
    public abstract void CommandUpdate();
    public abstract void Delete();
}
