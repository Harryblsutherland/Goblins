using System;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

    public static MouseManager Current;
    private bool isShiftDown;
    private Rect Box;

    private Vector2 boxStart = Vector2.zero;
    private Vector2 boxEnd = Vector2.zero;
    private List<Interactive> NewSelections = new List<Interactive>();
    private List<Interactive> selections = new List<Interactive>();

    public List<Interactive> Selections
    {
        get
        {
            return selections;
        }
        set
        {
            selections = value;
        }
    }

    private MouseManager()
    {
        Current = this;
    }
	// Update is called once per frame
	void LateUpdate () {

        SingleUnitSelected();
        CreateBox();
        addNewselections(NewSelections,selections);
        checkforDeselection();
        NewSelections.Clear();


    }

    private void checkforDeselection()
    {
        if(NewSelections.Count == 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }
            var terrain = hit.transform.GetComponent<Terrain>();
            if (terrain == null)
            {
                foreach (var sel in selections)
                {
                    if (sel != null) sel.Deselect();
                }
                selections.Clear();
            }
        }
    }

    public void addNewselections(List<Interactive> prNewSelections,List<Interactive> prCurrentSelections)
    {
        if (prCurrentSelections.Count > 0 && prNewSelections.Count > 0)
        {
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                foreach (var sel in prCurrentSelections)
                {
                    if (sel != null) sel.Deselect();
                }
                prCurrentSelections.Clear();
            }
        }
        foreach (var unit in prNewSelections)
        {
            prCurrentSelections.Add(unit);
            unit.Select();
        }

    }
    private void SingleUnitSelected()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse0))
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
            var interact = hit.transform.GetComponent<Interactive>();
            if (interact == null)
            {
                return;
            }
            NewSelections.Add(interact);
        }
    }
    private void CreateBox()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                boxStart = Input.mousePosition;
            }
            boxEnd = Input.mousePosition;

            Box = new Rect(
                         boxStart.x,
                         Screen.height - boxStart.y,
                         boxEnd.x - boxStart.x,
                         -1 * (boxEnd.y - boxStart.y)
                         );

        }
        else
        {
            if (boxEnd != Vector2.zero && boxStart != Vector2.zero)
            {
                HandleUnitSelection();
             
            }
            boxStart = Vector2.zero;
            boxEnd = Vector2.zero; 
        }
    }
    private void HandleUnitSelection()
    {
        
        //if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        //{
        //    foreach (var sel in selections)
        //    {
        //        if (sel != null) sel.Deselect();
        //    }
        //    selections.Clear();
        //}
        var square = Utilities.GetViewportBounds(Camera.main, boxStart, boxEnd);
        foreach (var player in RtsManager.Current.Players)
        {

            if (player.IsAi == false)
            {
                foreach (var unit in player.ActiveUnits)
                {
                    if (!square.Contains(Camera.main.WorldToViewportPoint(unit.transform.position)))
                    {
                        continue;
                    }
                    var interact = unit.GetComponent<Interactive>();
                    if (interact == null)
                    {
                        continue;
                    }
                    NewSelections.Add(interact);

                }
            }
        }
        
    }

    private void OnGUI()
    {
        if (boxStart == Vector2.zero & boxEnd == Vector2.zero)
        {
            return;
        }

        Utilities.DrawScreenRect(Box, new Color(0.8f, 0.8f, 0.95f, 0.25f));
        Utilities.DrawScreenRectBorder(Box, 2, new Color(0.0f, 0.8f, 0.95f));
    }
}
