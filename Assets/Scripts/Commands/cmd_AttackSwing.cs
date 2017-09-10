using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_AttackSwing : Command
{

    private float Duration;
    private float timeEleapsed;
    private float triggerPoint;
    private bool triggered;
    private Weapon weapon;
    private string attackAnimation;

    /// <summary>
    /// this command gets inserted at the front of the command queue to complete an attack animation and time out other events this is command is here to make sure is only doing an attack action
    /// and nothing else it also allows for animation cancelling minigame.
    /// </summary>

    public void DefineFields(string prAnimation, float prDuration, float prAttackTriggerPoint, Weapon prAttackingWeapon)
    {
        Duration = prDuration;
        triggerPoint = prAttackTriggerPoint;
        weapon = prAttackingWeapon;
        triggered = false;
        timeEleapsed = 0;
        attackAnimation = prAnimation;
    }
    public override void Execute()
    {


        if (weapon.Target == null)
        {
            commandManager.NextCommand();
            return;
        }
        weapon.transform.LookAt(weapon.Target.transform);
        commandManager.animator.Play(attackAnimation);
        
    }
    public override void Pause()
    {

    }
    public override void CommandUpdate()
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
            Destroy(this);
        }
    }

    public override void Delete()
    {
    }
}