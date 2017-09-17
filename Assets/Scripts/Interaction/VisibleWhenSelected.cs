using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleWhenSelected : Interaction {

    public List<GameObject> DisplayItems = new List<GameObject>();
    public override void Deselect()
    {
        foreach (var DisplayItem in DisplayItems)
        {
            DisplayItem.SetActive(false);
        }
        
    }

    public override void Select()
    {
        foreach (var DisplayItem in DisplayItems)
        {
            DisplayItem.SetActive(true);
        }
    }

    // Use this for initialization
    void Start ()
    {
        foreach (var DisplayItem in DisplayItems)
        {
            DisplayItem.SetActive(false);
        }
    }
	
}
