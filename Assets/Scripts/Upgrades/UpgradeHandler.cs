using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler {

    public string upgradedunit;
    public List<GameObject> upgrades = new List<GameObject>();
   
    public void AddUpgradeToUnitType(GameObject prAddedUpGrade)
    {
        upgrades.Add(prAddedUpGrade);
        Debug.Log(prAddedUpGrade.ToString() + "upgrade added" + upgrades[0].ToString());
    }
    public void EquipUpgradeToUnit(GameObject prUpgradingUnit)
    {
        foreach(var upgrade in upgrades)
        {
           GameObject.Instantiate(upgrade,prUpgradingUnit.transform);
        }
    }
}
