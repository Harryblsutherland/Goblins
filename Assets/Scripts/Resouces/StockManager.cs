using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockManager : MonoBehaviour
{
    public int Stockcount;
    public List<GameObject> CurrentUnits = new List<GameObject>();
    public List<GameObject> Newunits = new List<GameObject>();

    public void EquipUnit(GameObject prOldUnit)
    {
        var replacingUnit = ChooseUnit(prOldUnit);
        if (replacingUnit != null && Stockcount > 0)
        {
            Vector3 position = prOldUnit.transform.position;
            var newUnit = (GameObject)GameObject.Instantiate(
                                                            replacingUnit,
                                                            prOldUnit.transform.position,
                                                            Quaternion.identity
                                                            );
        
            newUnit.AddComponent<Player>().Info = prOldUnit.GetComponent<Player>().Info;
            // this section defines upgrades for newly created units
            var unitInfo = newUnit.GetComponent<UnitInfo>();
            foreach(var key in unitInfo.upgradeKeys)
            {
                GetComponent<Player>().Info.raceManager.GetUpgradeHandler(key).EquipUpgradeToUnit(newUnit);
            }

            var nav = newUnit.AddComponent<RightClickNavigation>();
            newUnit.AddComponent<ActionSelect>();
            Destroy(prOldUnit);
            Stockcount -= 1; 
        }
        else
        {
            prOldUnit.GetComponent<CommandManager>().NextCommand();
        }
        Debug.Log("this unit is being replaced");
    }

    private GameObject ChooseUnit(GameObject prOldUnit)
    {
        
        for(var i = 0; i < CurrentUnits.Count; i++)
        {
           if (prOldUnit.GetComponent<UnitInfo>().Name  == CurrentUnits[i].GetComponent<UnitInfo>().Name)
            {
                return Newunits[i];
            }
        }
        return null;
    }
}
