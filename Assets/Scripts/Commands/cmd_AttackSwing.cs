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

    /// <summary>
    /// this command gets inserted at the front of the command queue to complete an attack animation and time out other events this is command is here to make sure is only doing an attack action
    /// and nothing else it also allows for animation cancelling minigame.
    /// </summary>

    public static Cmd_AttackSwing New(GameObject prGameObject, float prDuration, float prAttackTriggerPoint, Weapon prAttackingWeapon)
    {
        Cmd_AttackSwing newcommand = prGameObject.AddComponent<Cmd_AttackSwing>();
        newcommand.Duration = prDuration;
        newcommand.triggerPoint = prAttackTriggerPoint;
        newcommand.weapon = prAttackingWeapon;
        newcommand.triggered = false;
        newcommand.timeEleapsed = 0;

        return newcommand;
    }

    public override void Execute()
    {
        commandManager.animator.Play(GetComponent<UnitAnimation>().Attack.name);
        if (weapon.Target == null)
        {
            commandManager.NextCommand();
            return;
        }
        weapon.transform.LookAt(weapon.Target.transform.position);
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

        }
    }

    public override void Delete()
    {
    }
}