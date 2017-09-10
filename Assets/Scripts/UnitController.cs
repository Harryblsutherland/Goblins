﻿using System;
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
    void Start()
    {
    }
    public override void RightClickInSpace(Vector3 Point)
    {
        commandManager.AddCommand(NewCommand.MoveCommandAdd(transform.gameObject, Point));
    }
    public override void RightClickOnStructure(GameObject TargetUnit)
    {
        commandManager.AddCommand(NewCommand.MoveCommandAdd(transform.gameObject, TargetUnit.transform.position));
    }

    public override void RightClickOnUnit(GameObject TargetUnit)
    {
        if (TargetUnit.GetComponent<AttackInRange>().player.Name == GetComponent<AttackInRange>().player.Name)
        {
            commandManager.AddCommand(NewCommand.FollowCommandAdd(transform.gameObject, TargetUnit));
            Debug.Log("follow Command added");
        }
        
        commandManager.AddCommand(NewCommand.AttackCommandAdd(transform.gameObject, TargetUnit));
        Debug.Log("attack Command added");

    }
    // Update is called once per frame
    void Update()
    { 
    }
}
  