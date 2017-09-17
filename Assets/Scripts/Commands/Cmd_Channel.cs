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

    /// <summary>
    /// this command gets inserted at the front of the command queue to complete an activity animation and time out other events this is command is here to make sure the unit is only doing an action
    /// and nothing else it also allows for animation cancelling minigame.
    /// </summary>

    public static Cmd_Channel New(GameObject prGameObject, string prAnimation, float prDuration, float prActionTriggerPoint, Delegate prTriggerAction)
    {
        Cmd_Channel newcommand = prGameObject.AddComponent<Cmd_Channel>();
        newcommand.DefineFields(prAnimation, prDuration, prActionTriggerPoint, prTriggerAction);
        return newcommand;
    }
    public void DefineFields(string prAnimation, float prDuration, float prAttackTriggerPoint, Delegate prTriggerAction)
    {
        duration = prDuration;
        triggerPoint = prAttackTriggerPoint;
        timeEleapsed = 0;
        ChannelAction = prTriggerAction;
        channelAnimation = prAnimation;
    }
    public override void Execute()
    {
        transform.LookAt(channelPoint);
        commandManager.animator.Play(channelAnimation);
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
    }

 
}
