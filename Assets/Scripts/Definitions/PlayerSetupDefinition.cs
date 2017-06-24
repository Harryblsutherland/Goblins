using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSetupDefinition {

    public string Name;
    public Transform Location;
    public Color AccentColor;
    public bool isAi;
    public List<GameObject> StartingUnits = new List<GameObject>();
    private List<GameObject> _ActiveUnits = new List<GameObject>();

    public float Credits;

    public List<GameObject> ActiveUnits
    {get{return _ActiveUnits;}
    }
}
