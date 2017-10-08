using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cmd_humanEquip : Command
{
    
    public GameObject targetBuilding;
    public bool aggression;
    public string animationclip;
    public float relaxDistance;
    /// <summary>
    /// this command moves the unit to a building and calls the replace unit function
    /// </summary>

    public static Cmd_humanEquip New(GameObject prGameObject, GameObject prBuildingToEquip)
    {
        Cmd_humanEquip newcommand = prGameObject.AddComponent<Cmd_humanEquip>();
        newcommand.targetBuilding = prBuildingToEquip;

        return newcommand;
    }
    public static Cmd_Move New(GameObject prGameObject, Vector3 prPoint, bool Agressive)
    {
        Cmd_Move newcommand = prGameObject.AddComponent<Cmd_Move>();
        newcommand.destination = prPoint;
        newcommand.aggression = Agressive;
        return newcommand;
    }
    public override void Awake()
    {
        base.Awake();
        aggression = false;
        relaxDistance = 5;
    }
    public override void Execute()
    {
        commandManager.animator.Play(GetComponent<UnitAnimation>().Idle.name);
        var distance = Vector3.Distance(targetBuilding.transform.position, transform.position);
        if (distance <= relaxDistance)
        {
            targetBuilding.GetComponent<StockManager>().ReplaceUnit(gameObject);
        }
    }

    public override void Pause()
    {

    }

    public override void CommandUpdate()
    {
        var distance = Vector3.Distance(targetBuilding.transform.position, transform.position);
        if (distance <= relaxDistance)
        {
            targetBuilding.GetComponent<StockManager>().ReplaceUnit(gameObject);
        }
        else
        {
            commandManager.commandQueue.Insert(1, Cmd_Move.New(transform.gameObject, targetBuilding.transform.position));
        }
    }
    public override void Delete()
    {
        
    }

}
