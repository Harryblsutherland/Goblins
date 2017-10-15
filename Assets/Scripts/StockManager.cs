using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockManager : MonoBehaviour
{
    public int Stockcount;
    public GameObject oneSword, twoSwords, swordShield, mountedOneSword, mountedTwoSword, mountedShieldSword;

    public void OnInteractCommand(GameObject prCommandedObject)
    {
        prCommandedObject.GetComponent<CommandManager>().AddCommand(Cmd_Move.New(prCommandedObject, transform.position));
        prCommandedObject.GetComponent<CommandManager>().AddCommand(Cmd_humanEquip.New(prCommandedObject, gameObject));
    }

    public void EquipUnit(GameObject prOldUnit)
    {
        var replacingUnit = ChooseUnit(prOldUnit);
        if (replacingUnit != null || Stockcount >= 0)
        {
            Vector3 position = prOldUnit.transform.position;
            var newUnit = (GameObject)GameObject.Instantiate(
                                                            replacingUnit,
                                                            transform.position,
                                                            Quaternion.identity
                                                            );
            newUnit.AddComponent<Player>().Info = prOldUnit.GetComponent<Player>().Info;
            var nav = newUnit.AddComponent<RightClickNavigation>();
            newUnit.AddComponent<ActionSelect>();
            Destroy(prOldUnit);
        }
        else
        {
            prOldUnit.GetComponent<CommandManager>().NextCommand();
        }
        Debug.Log("this unit is being replaced");
    }

    private GameObject ChooseUnit(GameObject prOldUnit)
    {
        GameObject newUnit;
        switch (prOldUnit.GetComponent<UnitInfo>().Name)
        {
            case "Peasant":
                newUnit = oneSword;
                break;
            case "Fighter":
                newUnit = twoSwords;
                break;
            case "Shieldsman":
                newUnit = swordShield;
                break;
            case "MountedPeasant":
                newUnit = mountedOneSword;
                break;
            case "MountedFighter":
                newUnit = mountedTwoSword;
                break;
            case "MountedShieldsman":
                newUnit = mountedShieldSword;
                break;
            default:
                newUnit = null;
                break;
        }
        return newUnit;
    }
}
