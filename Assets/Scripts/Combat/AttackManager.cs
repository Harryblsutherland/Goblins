using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class AttackManager : MonoBehaviour
{
    public List<Weapon> Weapons = new List<Weapon>();
    private UnitInfo target = null;
    public float attackFindRange = 25;
    public PlayerSetupDefinition player;


    public UnitInfo Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
            foreach (var weapon in Weapons)
            {
                weapon.Target = target;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Weapons = GetComponents<Weapon>().Cast<Weapon>().ToList();
    }

    public float GetMaximumWeaponRange()
    {
        float Range = 5;
        foreach (var weapon in Weapons)
        {
            if (Range < weapon.range)
            {
                Range = weapon.range;
            }
        }
        return Range;
    }

    public float GetMinimumWeaponRange()
    {
        float Range = Weapons[0].range;
        foreach (var weapon in Weapons)
        {
            if (Range > weapon.range)
            {
                Range = weapon.range;
            }
        }
        return Range;
    }

    public void Attack()
    {
        foreach (var weapon in Weapons)
        {
            if (weapon.Target == null)
            {
                continue;
            }
            if ((Vector3.Distance(weapon.Target.transform.position, transform.position) > attackFindRange) || (weapon.Target.gameObject == null))
            {
                weapon.Target = null;
                continue;
            }
            weapon.Attack();
        }
    }
}
