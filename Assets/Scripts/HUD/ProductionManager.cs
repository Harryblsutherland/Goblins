using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProductionManager : Interaction
{
    public List<ProductionItem> productionQueue = new List<ProductionItem>();
    private int maxQueue = 5;
    private float CurrentDuration;

    public override void Deselect()
    {
        InfoManager.Current.progressHud.SetActive(false);

    }

    public override void Select()
    {
        UpdateProductionQueue();
        InfoManager.Current.progressHud.SetActive(true);

    }

    public void removeItemFromList(int index)
    {
        productionQueue[index].OnCancelation.Invoke();
        Destroy(productionQueue[index]);
        productionQueue.RemoveAt(index);
        CurrentDuration = 0;

    }

    public void UpdateProductionQueue()
    {

        for (var i = 0; i < maxQueue; i++)
        {
            if (i >= productionQueue.Count)
            {
                InfoManager.Current.productionItems[i].GetComponent<Image>().color = Color.clear;
            }
            else
            {
                InfoManager.Current.productionItems[i].GetComponent<Image>().sprite = productionQueue[i].ItemImage; 
                InfoManager.Current.productionItems[i].GetComponent<Image>().color = Color.white;
            }

        }
        if (productionQueue.FirstOrDefault() != null)
        {
            InfoManager.Current.progressBar.fillAmount = CurrentDuration / productionQueue.FirstOrDefault().duration;
        }
        InfoManager.Current.CurrentlyDisplayed = transform.gameObject;
    }

    public void addItemToProductionQueue(ProductionItem NewItem)
    {
        if (productionQueue.Count < maxQueue)
        {
            productionQueue.Add(NewItem);
        }
        else
        {
            NewItem.OnCancelation.Invoke();
            Destroy(NewItem);
        }
    }

    void Update()
    {
        if (productionQueue.Count > 0)
        {
            CurrentDuration += Time.deltaTime;
            if (productionQueue.FirstOrDefault().duration <= CurrentDuration)
            {
                productionQueue.FirstOrDefault().Oncompletion.Invoke();
                productionQueue.Remove(productionQueue.FirstOrDefault());
                CurrentDuration = 0;
            }
        }
        else
        {
            CurrentDuration = 0;
        }

     
    }
}
