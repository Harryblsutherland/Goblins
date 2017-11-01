using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiSupport : MonoBehaviour {

    public List<GameObject> peasants = new List<GameObject>();
    public List<GameObject> warrens = new List<GameObject>();
    public PlayerSetupDefinition Player = null;

    public static aiSupport GetSupport(GameObject go)
    {
        return go.GetComponent<aiSupport>();
    }
    public void refresh()
    {
        peasants.Clear();
        warrens.Clear();
        foreach (var unit in Player.ActiveUnits)
        {
            if (unit.GetComponent<UnitInfo>().Name.Contains("Peasant"))
            {
                peasants.Add(unit);
            }
            if (unit.GetComponent<UnitInfo>().Name.Contains("Warren"))
            {
                warrens.Add(unit);
            }
        }
    }
    
}
