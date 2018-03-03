using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchUpgradeAction : ActionBehaviour {

    public float Cost = 0;
    public float BuildTime;
    public List<UpgradeTier> UpgradeTiers = new List<UpgradeTier>();
    public string UpgradeKey;
    public float costIncrement;
    public float buildTimeIncrement;
    private int currentUpgradeTier = 0;
    private PlayerSetupDefinition Player;
    private Action completionAction;
    private Action cancellationAction;
    private ProductionItem productionItem;


    


    void Start()
    {
        Player = GetComponent<Player>().Info;
        completionAction = () => ProductionAction();
        cancellationAction = () => CancellationAction();
    }

    public override Action GetClickAction()
    {
        return delegate ()
        {
            if (Player.Credits >= Cost)
            {
                GetComponent<ProductionManager>().addItemToProductionQueue(ProductionItem.New(gameObject, ButtonIcon, BuildTime, completionAction, cancellationAction));
                Player.Credits -= Cost;
            }
        };
    }

    private void CancellationAction()
    {
        Player.Credits += Cost;
    }

    private void ProductionAction()
    {
        UpgradeSelector.current.DefineButtons(UpgradeTiers[currentUpgradeTier].upgrades, UpgradeKey, Player);
        currentUpgradeTier++;
        if (currentUpgradeTier > UpgradeTiers.Count)    
        {
            Destroy(this);
        }
        Cost += costIncrement;
        BuildTime += buildTimeIncrement;

    }
}
