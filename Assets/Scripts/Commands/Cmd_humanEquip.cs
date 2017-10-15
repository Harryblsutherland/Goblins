using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cmd_humanEquip : Command
{
    
    public GameObject targetBuilding;
    public bool aggression;
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
    public override void Awake()
    {
        base.Awake();
        aggression = false;
        relaxDistance = 10;
    }
    public override void Execute()
    {
        commandManager.animator.Play(GetComponent<UnitAnimation>().Idle.name);
        var distance = Vector3.Distance(targetBuilding.transform.position, transform.position);
    }

    public override void Pause()
    {
        base.Pause();
    }

    public override void CommandUpdate()
    {
        var distance = Vector3.Distance(targetBuilding.transform.position, transform.position);
        if (distance <= relaxDistance)
        {
            targetBuilding.GetComponent<StockManager>().EquipUnit(transform.gameObject);
        }
        else
        {
            commandManager.InsertCommand(Cmd_Move.New(gameObject, targetBuilding.transform.position));
        }
    }
    public override void Delete()
    {
        
    }

}
