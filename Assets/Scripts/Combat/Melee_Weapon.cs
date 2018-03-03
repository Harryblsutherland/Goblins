using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Weapon : Weapon {


    public override void Fire()
    {
        damageobject.damage = damage;
        for(int i = 0;i < attacks; i++)
        {
            Target.RecieveDamage(damageobject);
        }
    }
}
