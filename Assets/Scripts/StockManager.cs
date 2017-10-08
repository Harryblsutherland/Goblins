using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockManager : MonoBehaviour
{
    public int Stockcount;
    public GameObject basic, withSword, withShield, withHorse, horseSword, horseShield;

    public void OnInteractCommand(GameObject prCommandedObject)
    {
        prCommandedObject.GetComponent<CommandManager>().AddCommand(Cmd_Move.New(prCommandedObject, transform.position));
        prCommandedObject.GetComponent<CommandManager>().AddCommand(Cmd_humanEquip.New(prCommandedObject, gameObject));
    }

    public void ReplaceUnit(GameObject prOldUnit)
    {
        Debug.Log("this unit is being replaced");
    }
}
