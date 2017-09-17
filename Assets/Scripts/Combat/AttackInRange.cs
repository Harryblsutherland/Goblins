using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackInRange : MonoBehaviour
{

    public GameObject weaponHit;
    public List<Weapon> Weapons = new List<Weapon>();
    public float findTargetDelay = 1;
    public float attackFindRange = 20;
    private UnitInfo target = null;
    public PlayerSetupDefinition player;
    public float findTargetCounter = 0;
    private bool aggressive = true;

    public UnitInfo Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
            foreach(var weapon in Weapons)
            {
                weapon.Target = target;
            }
        }
    }

    public bool Aggressive
    {
        get
        {
            return aggressive;
        }

        set
        {
            aggressive = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>().Info;
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

    public void AttackNearestTarget()
    {
        if (FindNearestEnemyInRange() != null)
        {
            {
                GetComponent<CommandManager>().InsertCommand(Cmd_Attack.New(transform.gameObject, FindNearestEnemyInRange().gameObject, 60));
            }
        }
    }
    public UnitInfo FindNearestEnemyInRange()
    {
        float shortestDistanceEnemy = attackFindRange;
        UnitInfo NearestEnemy = null;
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
                        NearestEnemy = unit.GetComponent<UnitInfo>();
                    }
                }
            }
        }
        return NearestEnemy;
    }
    void FindTarget()
    {
        // if there is a specific target choose that
        if (Target != null)
        {
            foreach (var weapon in Weapons)
            {
                weapon.Target = Target;
            }
            return;
        }
        float shortestDistanceEnemy = attackFindRange;
        foreach (var Weapon in Weapons)
        {
            if (Weapon.Target != null)
            {
                continue;
            }
            Weapon.Target = FindNearestEnemyInRange();
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
            if ((Vector3.Distance(weapon.Target.transform.position, transform.position) > attackFindRange) || (weapon.Target.gameObject == null))
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
        if (Target == null && Aggressive)
        {
            findTargetCounter += Time.deltaTime;
            if (findTargetCounter > findTargetDelay)
            {
                AttackNearestTarget();
                findTargetCounter = 0;
            }
        }
    }
}
