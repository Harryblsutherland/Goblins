using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : InputController
{
    private CommandManager commandManager;
    // Use this for initialization
    private void Awake()
    {
        commandManager = GetComponent<CommandManager>();
    }
    public override void RightClickOnMine(GameObject TargetObject)
    {
        if (TargetObject.GetComponent<ResourceNode>().canmine())
        {
            commandManager.AddCommand(Cmd_Gather.New(gameObject, TargetObject));
        }
    }
    public override void RightClickInSpace(Vector3 Point)
    {
        commandManager.AddCommand(Cmd_Move.New(transform.gameObject, Point));

    }
    public override void RightClickOnStructure(GameObject TargetUnit)
    {
        if (TargetUnit.GetComponent<StockManager>() != null)
        {
            commandManager.AddCommand(Cmd_humanEquip.New(gameObject, TargetUnit));
            return;
        }
        if (TargetUnit.GetComponent<Player>().Info.Name == GetComponent<Player>().Info.Name)
        {
            commandManager.AddCommand(Cmd_Move.New(transform.gameObject, TargetUnit.transform.position));
        }
        else
        {
            commandManager.AddCommand(Cmd_Attack.New(transform.gameObject, TargetUnit));
        }


    }

    public override void RightClickOnUnit(GameObject TargetUnit)
    {
        if (TargetUnit.GetComponent<Player>().Info.Name == GetComponent<Player>().Info.Name)
        {
            commandManager.AddCommand(Cmd_Follow.New(transform.gameObject, TargetUnit));
        }
        else
        {
            commandManager.AddCommand(Cmd_Attack.New(transform.gameObject, TargetUnit));
        }
    }
    void Update()
    {
    }
}
