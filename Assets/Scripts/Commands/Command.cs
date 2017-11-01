using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    public bool paused;
    public CommandManager commandManager;
    public Targeting Targeting;

    public virtual void Awake()
    {
        paused = false;
        commandManager = GetComponent<CommandManager>();
        Targeting = GetComponent<Targeting>();
    }
    public virtual void Pause()
    {
        paused = true;
    }
    public virtual void Execute()
    {

    }

        

    public abstract void CommandUpdate();
    public abstract void Delete();
}
