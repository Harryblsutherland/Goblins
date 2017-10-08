using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionItem : MonoBehaviour
{
    public Sprite ItemImage;
    public Action Oncompletion;
    public Action OnCancelation;
    public float duration;

    public static ProductionItem New(GameObject prGameObject, Sprite prItemImage, float prDuration, Action prOnCompletion, Action prOnCancelation)
    {
        ProductionItem newItem = prGameObject.AddComponent<ProductionItem>();
        newItem.duration = prDuration;
        newItem.ItemImage = prItemImage;
        newItem.Oncompletion = prOnCompletion;
        newItem.OnCancelation = prOnCancelation;

        return newItem;
    }

}
