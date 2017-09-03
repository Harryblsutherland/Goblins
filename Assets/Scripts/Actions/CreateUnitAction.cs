using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateUnitAction : ActionBehaviour {

    public GameObject CreatedUnit;
    public float Cost = 0;
    private PlayerSetupDefinition Player;
    private StructureController inputController;

	// Use this for initialization
	void Start ()
    {
        Player = GetComponent<Player>().Info;
        inputController = GetComponent<StructureController>();
    }
	public override System.Action GetClickAction()
    {
        return delegate ()
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
                switch (inputController.RalliedObject.GetComponent<UnitInfo>().UnitType)
                {
                    case "Structure" :
                        unitInput.RightClickOnStructure(inputController.RalliedObject);
                        break;
                    case "Unit" :
                        unitInput.RightClickOnUnit(inputController.RalliedObject);
                        break;
                    default :
                        unitInput.RightClickInSpace(inputController.RalliedObject.transform.position);
                        break;
                }
            }
            else
            {
                unitInput.RightClickInSpace(inputController.Rallypoint);
            }
            Player.Credits -= Cost;
        };
    }
	// Update is called once per frame
	void Update () 
    {
		
	}
}
