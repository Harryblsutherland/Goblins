﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : Interaction
{

    public string Name;
    public float MaxHealth, CurrentHealth;
    bool show = false;
    public Sprite PortraitImage;
    public string UnitType;

    public override void Select()
    {
        show = true;
    }
    public void RecieveDamage(DamageObject prAttack)
    {
        CurrentHealth -= prAttack.damage;
    }
    void Update()
    {
        if (!show)
        {
            return;
        }
        InfoManager.Current.SetImage(PortraitImage);
        InfoManager.Current.SetLines(
                                    Name,
                                    MaxHealth + " / " + CurrentHealth,
                                    GetComponent<Player>().Info.Name
                                    );
    }
    public override void Deselect()
    {
        show = false;
        InfoManager.Current.Clearbox();
    }

}
