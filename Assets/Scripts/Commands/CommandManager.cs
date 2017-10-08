using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : MonoBehaviour
{


    private Command currentCommand;
    public List<Command> commandQueue = new List<Command>();
    public Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play(GetComponent<UnitAnimation>().Idle.name);
    }
    void Start()
    {
        
    }
    public void InsertCommand(Command prNewCommand)
    {
        commandQueue.Insert(0, prNewCommand);
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
            command.Delete();
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
        if (currentCommand != null)
        {
            currentCommand.Delete();
            commandQueue.Remove(currentCommand);
            Destroy(currentCommand);
        }
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
                animator.Play(GetComponent<UnitAnimation>().Idle.name);
                return;
            }
            SetCurrentCommand();
        }
        currentCommand.CommandUpdate();
    }
}
