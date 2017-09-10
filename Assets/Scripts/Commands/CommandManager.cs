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
    public void InsertCommand(Command prNewCommand)
    {
        commandQueue.Insert(0,prNewCommand);
        if (commandQueue.Count >= 2)
        {
            commandQueue[1].Pause();
        }
        SetCurrentCommand();
    }

    public void FlushList()
    {
        foreach (var command in commandQueue)
        {
            Destroy(command);
        }
        commandQueue.Clear();
        currentCommand = null;
        SetCurrentCommand();
    }
    public void AddCommand(Command prCommand)
    {
        if ((!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && commandQueue.Count > 0))
        {
          FlushList();
        }
        commandQueue.Add(prCommand);
    }
    public void NextCommand()
    {
        commandQueue.Remove(currentCommand);
        currentCommand.Delete();
        Destroy(currentCommand);
        SetCurrentCommand();
    }
    private void SetCurrentCommand()
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
            SetCurrentCommand();
        }
        currentCommand.CommandUpdate();
    }
}
