using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : MonoBehaviour
{


    private Command currentCommand;
    public List<Command> commandQueue = new List<Command>();
    public Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void FlushList()
    {
        foreach (var command in commandQueue)
        {
            command.Delete();
        }
        commandQueue.Clear();
        currentCommand = null;
    }
    public void NextCommand()
    {
        commandQueue.Remove(currentCommand);
        currentCommand.Delete();
        setCurrentCommand();
    }
    private void setCurrentCommand()
    {
        currentCommand = commandQueue.FirstOrDefault();
        if (currentCommand != null)
        {
            currentCommand.Execute();
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (currentCommand == null)
        {
            if (commandQueue.Count == 0)
            {
                return;
            }
            setCurrentCommand();
        }
        currentCommand.Update();
    }
}
