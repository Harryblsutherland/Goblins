using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command

{
    private bool paused;
    public CommandManager commandManager;
    public abstract void Pause();
    public abstract void Execute();
    public abstract void Update();
    public abstract void Delete();

}
