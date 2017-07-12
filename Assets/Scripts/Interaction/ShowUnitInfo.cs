using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUnitInfo : Interaction
{

    public string Name;
    public float MaxHealth, CurrentHealth;

    public Sprite PortraitImage;

    public override void Select()
    {
        InfoManager.Current.SetImage(PortraitImage);
        InfoManager.Current.SetLines(
                                    Name,
                                    MaxHealth + " / " + CurrentHealth,
                                    GetComponent<Player>().Info.Name
                                    );
    }
    public override void Deselect()
    {
        InfoManager.Current.Clearbox();
    }

}
