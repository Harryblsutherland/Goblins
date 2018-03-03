using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Weapon : Weapon {

    public Projectile missle;
    public GameObject shootPoint;
    public float projectilespeed;
    public override void Fire()
    {
        damageobject.damage = damage;
        for (int i = 0; i < attacks; i++)
        {
            Projectile newprojectile = Instantiate(missle, shootPoint.transform.position, transform.rotation).GetComponent<Projectile>();

            newprojectile.ShootingUnit = transform.gameObject;
            newprojectile.Damage = damageobject;
            newprojectile.speed = projectilespeed;
            newprojectile.target = target;
        }
    }
}

