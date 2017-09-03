using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    private bool paused;
    public CommandManager commandManager;

    public virtual void Awake()
    {
        paused = false;
        commandManager = GetComponent<CommandManager>();
    }
    public virtual void Pause()
    {
        paused = true;
    }
    public abstract void Execute();
    public abstract void CommandUpdate();
    public abstract void Delete();

}
