using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaceManager
{
    // this class handles race specific behaviour such as starting units, creating upgrade handlers and other racial features.
    public List<string> UpgradeKeys = new List<string>();
    [SerializeField]
    public List<UpgradeHandler> upgradeHandlers = new List<UpgradeHandler>();
  
    public UpgradeHandler GetUpgradeHandler(string upgradeKey)
    {
        foreach (var manager in upgradeHandlers)
        { 
            if(manager.upgradedunit == upgradeKey)
            {
                return manager;
            }
        }
        return null;
    }

    public void Generatehandlers()
    {
        //if(UpgradeManagers.Count > 1)
        //{
        //    return;
        //}
        foreach (var Key in UpgradeKeys)
        {
            var uh = new UpgradeHandler();
            uh.upgradedunit = Key;
            upgradeHandlers.Add(uh);
            Debug.Log( Key + " handler has been added to the game" + upgradeHandlers.Count.ToString() + upgradeHandlers[0].upgradedunit);
        }
    }
}
