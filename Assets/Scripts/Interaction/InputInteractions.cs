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
            var es = UnityEngine.EventSystems.EventSystem.current;
            if (es != null && es.IsPointerOverGameObject())
                return;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }
            // var info = hit.transform.GetComponent<UnitInfo>();
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

            //if (info == null)
            //{
            //    return;
            //}
            //else if (info.unitType == "Structure")
            //{
            //    Controller.RightClickOnStructure(hit.transform.gameObject);
            //}
            //else if (hit.transform.gameObject.tag.Equals("Gold"))
            //{
            //    Debug.Log("itsAMine");
            //    Controller.RightClickOnMine(info.gameObject);
            //}
            //else
            //{
            //    Controller.RightClickOnUnit(hit.transform.gameObject);

            //}
        }


    }


}
