using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_Channel : Command {

    private float duration;
    private float timeEleapsed;
    private float triggerPoint;
    private string channelAnimation;
    private Command triggeredCommand;
    private Vector3 channelPoint;
    private Delegate ChannelAction;
    public Delegate CancelledAction;

    /// <summary>
    /// this command gets inserted at the front of the command queue to complete an activity animation and time out other events this is command is here to make sure the unit is only doing an action
    /// and nothing else it also allows for animation cancelling minigame.
    /// </summary>

    public static Cmd_Channel New(GameObject prGameObject, string prAnimation, float prDuration, float prActionTriggerPoint,Vector3 prChannelPoint ,Delegate prTriggerAction)
    {
        Cmd_Channel newcommand = prGameObject.AddComponent<Cmd_Channel>();
        newcommand.DefineFields(prAnimation, prDuration, prActionTriggerPoint, prChannelPoint, prTriggerAction);

        return newcommand;
    }
    public static Cmd_Channel New(GameObject prGameObject, string prAnimation, float prDuration, float prActionTriggerPoint, Vector3 prChannelPoint, Delegate prTriggerAction, Delegate prCancelAction)
    {
        Cmd_Channel newcommand = prGameObject.AddComponent<Cmd_Channel>();
        newcommand.DefineFields(prAnimation, prDuration, prActionTriggerPoint, prChannelPoint, prTriggerAction);
        newcommand.CancelledAction = prCancelAction;

        return newcommand;
    }
    public override void Awake()
    {
        base.Awake();
        timeEleapsed = 0;
    }
    public void DefineFields(string prAnimation, float prDuration, float prAttackTriggerPoint,Vector3 prChannelPoint, Delegate prTriggerAction)
    {
        channelAnimation = prAnimation;
        duration = prDuration;
        triggerPoint = prAttackTriggerPoint;
        channelPoint = prChannelPoint;
        ChannelAction = prTriggerAction;
    }
    public override void Execute()
    {
        commandManager.animator.Play(GetComponent<UnitAnimation>().Attack.name);
        transform.LookAt(channelPoint);
    }
    public override void Pause()
    {
    }
    public override void CommandUpdate()
    {

        timeEleapsed += Time.deltaTime;
        if (timeEleapsed >= duration)
        {
            commandManager.NextCommand();
        }
        if (timeEleapsed >= triggerPoint)
        {
            ChannelAction.DynamicInvoke();
        }
    }

    public override void Delete()
    {
        if (timeEleapsed < duration)
        {
            //action was canceled
            CancelledAction.DynamicInvoke();
        }
        commandManager.animator.Play(GetComponent<UnitAnimation>().Idle.name);
    }

 
}
