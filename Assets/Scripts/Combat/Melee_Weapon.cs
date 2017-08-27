using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Weapon : Weapon {


    public override void Fire()
    {
        base.Fire();
        Target.RecieveDamage(damageObject);
    }
}
