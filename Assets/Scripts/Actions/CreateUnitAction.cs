using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateUnitAction : ActionBehaviour {

    public GameObject CreatedUnit;
    public float Cost = 0;
    public float BuildTime;
    private PlayerSetupDefinition Player;
    private StructureController inputController;
    private Action completionAction;
    private Action cancellationAction;
    private ProductionItem productionItem;
        
	// Use this for initialization
	void Start ()
    {
        Player = GetComponent<Player>().Info;
        inputController = GetComponent<StructureController>();
        completionAction = () => ProductionAction();
        cancellationAction = () => CancellationAction();

    }
    public override Action GetClickAction()
    {
        return delegate ()
        {
            if (Player.Credits >= Cost)
            {
                GetComponent<ProductionManager>().addItemToProductionQueue(ProductionItem.New(gameObject, ButtonIcon, BuildTime, completionAction, cancellationAction));
                Player.Credits -= Cost;
            }
        };
    }
    private void CancellationAction()
    {
        Player.Credits += Cost;
    }
    //this action will be passed to the item on the command queue to be executed after completion
    private void ProductionAction()
    {
        if (Player.Credits < Cost)
        {
            Debug.Log("Cannot Create, It costs" + Cost);
            return;
        }
        var newUnit = (GameObject)GameObject.Instantiate(
                                                    CreatedUnit,
                                                    transform.position,
                                                    Quaternion.identity);
        newUnit.AddComponent<Player>().Info = Player;
        var nav = newUnit.AddComponent<RightClickNavigation>();
        newUnit.AddComponent<ActionSelect>();
        // This section defines the commands for newly created units.
        var commandManager = newUnit.GetComponent<CommandManager>();
        var unitInput = newUnit.GetComponent<UnitController>();
        if (inputController.RalliedObject != null)
        {
            switch (inputController.RalliedObject.tag)
            {
                case "Structure":
                    unitInput.RightClickOnStructure(inputController.RalliedObject);
                    break;
                case "Unit":
                    unitInput.RightClickOnUnit(inputController.RalliedObject);
                    break;
                case "Gold":
                    unitInput.RightClickOnMine(inputController.RalliedObject);
                    break;
                default:
                    unitInput.RightClickInSpace(inputController.RalliedObject.transform.position);
                    break;
            }
        }
        else
        {
            unitInput.RightClickInSpace(inputController.Rallypoint);
        }
    }
	// Update is called once per frame
	void Update () 
    {
		
	}
}
