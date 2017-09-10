using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackInRange : MonoBehaviour
{

    public GameObject weaponHit;
    public List<Weapon> Weapons = new List<Weapon>();
    public float findTargetDelay = 1;
    public float attackFindRange = 5;
    public UnitInfo target = null;
    public PlayerSetupDefinition player;
    public float findTargetCounter = 0;
    public bool Aggressive = true;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>().Info;
        Weapons = GetComponents<Weapon>().Cast<Weapon>().ToList();
        attackFindRange = GetMaximumWeaponRange();

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
        float Range = 5;
        foreach (var weapon in Weapons)
        {
            if (Range > weapon.range)
            {
                Range = weapon.range;
            }
        }
        return Range;
    }

    void FindTarget()
    {
        // if there is a specific target choose that
        if (target != null)
        {
            foreach (var weapon in Weapons)
            {
                weapon.Target = target;
            }
            return;
        }
        //  otherwise pick a nearby target
        float shortestDistanceEnemy = attackFindRange;
        foreach (var Weapon in Weapons)
        {
            if (Weapon.Target != null)
            {
                continue;
            }
            foreach (var _player in RtsManager.Current.Players)
            {
                if (_player == player)
                {
                    continue;
                }
                foreach (var unit in _player.ActiveUnits)
                {

                    if (Vector3.Distance(unit.transform.position, transform.position) < attackFindRange)
                    {
                        if (Vector3.Distance(unit.transform.position, transform.position) < shortestDistanceEnemy)
                        {
                            shortestDistanceEnemy = Vector3.Distance(unit.transform.position, transform.position);
                            Weapon.Target = unit.GetComponent<UnitInfo>();
                        }
                    }
                }
            }
        }
    }
    public void Attack()
    {
        foreach (var weapon in Weapons)
        {
            if (weapon.Target == null)
            {
                return;
            }
            if (Vector3.Distance(weapon.Target.transform.position, transform.position) > attackFindRange)
            {
                weapon.Target = null;
                return;
            }
            if (weapon.canAttack)
            {
                weapon.Attack();
                Debug.Log("calling Weapon to fire");
            }
        }
    }
    void Update()
    {
        if (Aggressive)
        {
            findTargetCounter += Time.deltaTime;
            if (findTargetCounter > findTargetDelay)
            {
                FindTarget();
                findTargetCounter = 0;
            }
        }
    }
}
