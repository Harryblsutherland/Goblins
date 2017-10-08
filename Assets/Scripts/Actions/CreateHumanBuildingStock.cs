using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHumanBuildingStock : ActionBehaviour
{

    public float Cost = 0;
    public float BuildTime;
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
        GetComponent<StockManager>().Stockcount += 1; 
    }
}
