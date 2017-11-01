using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiSupport : MonoBehaviour {

    public List<GameObject> goblins = new List<GameObject>();
    public List<GameObject> warrens = new List<GameObject>();
    public PlayerSetupDefinition Player = null;

    public static aiSupport GetSupport(GameObject go)
    {
        return go.GetComponent<aiSupport>();
    }
    public void refresh()
    {
        goblins.Clear();
        warrens.Clear();
        foreach (var unit in Player.ActiveUnits)
        {
            if (unit.GetComponent<UnitInfo>().Name.Contains("Peasant"))
            {
                goblins.Add(unit);
            }
            if (unit.GetComponent<UnitInfo>().Name.Contains("Warren"))
            {
                warrens.Add(unit);
            }
        }
    }
    
}
