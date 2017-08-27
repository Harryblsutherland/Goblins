using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class cmd_AttackSwing : Command {

    
    private float Duration;
    private float timeEleapsed;
    private float triggerPoint;
    private bool triggered;
    private Weapon weapon;

    private string animation;

    public cmd_AttackSwing(CommandManager prCommandManager, string prAnimation, float prDuration, float prAttackTriggerPoint, Weapon prAttackingWeapon)
    {
        commandManager = prCommandManager;
        Duration = prDuration;
        triggerPoint = prAttackTriggerPoint;
        weapon = prAttackingWeapon;
        triggered = false;
        timeEleapsed = 0;
        animation = prAnimation;
    }

    public override void Delete()
    {
        
    }

    public override void Execute()
    {
        if (weapon.Target == null)
        {
            commandManager.NextCommand();
            return;
        }
        weapon.transform.LookAt(weapon.Target.transform);
        commandManager.animator.Play(animation);
        if (commandManager.commandQueue.Count >= 2)
        {
            commandManager.commandQueue[1].Pause();
        }
        

    }

    public override void Pause()
    {
        
    }

    public override void Update()
    {
        if (weapon.Target == null)
        {
            commandManager.NextCommand();
            return;
        }
        timeEleapsed += Time.deltaTime;
        if (timeEleapsed >= Duration)
        {
            commandManager.NextCommand();
        }
        if (timeEleapsed >= triggerPoint && !triggered)
        {
            weapon.Fire();
            triggered = true;
            Debug.Log("cashmeOutSide");
        }
    }
 }