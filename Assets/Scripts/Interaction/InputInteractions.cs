using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInteractions : Interaction
{

    private bool selected = false;
    private bool isActive = false;
    private InputController Controller;
    private void Start()
    {
        Controller = GetComponent<InputController>();
    }

    public override void Deselect()
    {
        selected = false;
    }

    public override void Select()
    {
        selected = true;
    }
    private void Update()
    {
        if (selected && Input.GetMouseButtonDown(1))
        {
            if (Map.Current.mouseIsOverMap)
            {
                Controller.RightClickInSpace(Map.Current.MapPositionToWorld(Input.mousePosition));
            }
            if (Utilities.CheckMouseHit() != null)
            {
                var hit = (RaycastHit)Utilities.CheckMouseHit();
                switch (hit.transform.gameObject.tag)
                {
                    case "Structure":
                        Controller.RightClickOnStructure(hit.transform.gameObject);
                        break;
                    case "Unit":
                        Controller.RightClickOnUnit(hit.transform.gameObject);
                        break;
                    case "Gold":
                        Debug.Log("itsAMine");
                        Controller.RightClickOnMine(hit.transform.gameObject);
                        break;
                    default:
                        Controller.RightClickInSpace((Vector3)RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition));
                        break;
                }
            }

            // var info = hit.transform.GetComponent<UnitInfo>();

        }


    }


}
