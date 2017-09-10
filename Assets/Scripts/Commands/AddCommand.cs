using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NewCommand  {

/// <summary>
/// this Class is a factory for each of the different commands its primary purpose is deifining the variables needed for each command as monobehaviours cannot have constructors.
/// </summary>

    public static Cmd_Move MoveCommandAdd(GameObject go, Vector3 Point)
    {
        Cmd_Move newcommand = go.AddComponent<Cmd_Move>();
        newcommand.destination = Point;

        return newcommand;
    }
    public static Cmd_Patrol PatrolCommandAdd(GameObject go, Vector3 Point)
    {
        Cmd_Patrol newcommand = go.AddComponent<Cmd_Patrol>();
        newcommand.pointA = Point;

        return newcommand;
    }
    public static Cmd_AttackSwing AttackSwingCommandAdd(GameObject go, string prAnimation, float prDuration, float prAttackTriggerPoint, Weapon prAttackingWeapon)
    {
        Cmd_AttackSwing newcommand = go.AddComponent<Cmd_AttackSwing>();
        newcommand.DefineFields(prAnimation, prDuration, prAttackTriggerPoint, prAttackingWeapon);

        return newcommand;
    }

    public static Cmd_Follow FollowCommandAdd(GameObject go, GameObject targetUnit)
    {
        Cmd_Follow newCommand = go.AddComponent<Cmd_Follow>();
        newCommand.followedUnit = targetUnit;

        return newCommand;
    }
    public static Cmd_Attack AttackCommandAdd(GameObject go, GameObject targetUnit)
    {
        Cmd_Attack newCommand = go.AddComponent<Cmd_Attack>();
        newCommand.TargetUnit = targetUnit;

        return newCommand;
    }

}
