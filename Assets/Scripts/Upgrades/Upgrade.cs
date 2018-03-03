using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeTier
{
    public string TeirName;
    public List<GameObject> upgrades = new List<GameObject>();
}
[System.Serializable]
public class Upgrade : MonoBehaviour {

    public string Name;
    public Sprite upgradeIcon;   

}
