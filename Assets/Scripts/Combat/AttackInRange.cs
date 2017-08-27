using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackInRange : MonoBehaviour
{

    public GameObject weaponHit;
    public List<Weapon> Weapons = new List<Weapon>();
    public float findTargetDelay = 1;
    public float attackRange = 20;
    public float attackSpeed = 0.25f;
    public float attackDamage = 1;


    private UnitInfo target;
    public PlayerSetupDefinition player;
    public float findTargetCounter = 0;
    public float attackCounter = 0;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>().Info;
        Weapons = GetComponents<Weapon>().Cast<Weapon>().ToList();
    }
    void FindTarget()
    {
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
                    if (Vector3.Distance(unit.transform.position, transform.position) < Weapon.range)
                    {
                        Weapon.Target = unit.GetComponent<UnitInfo>();
                        Debug.Log("targetFound");

                        continue;
                    }
                }
            }
        }
    }

    // Update is called once per frame

    void Attack()
    {
        foreach (var weapon in Weapons)
        {
            if (weapon.Target == null)
            {
                return;
            }
            if (Vector3.Distance(weapon.Target.transform.position, transform.position) > attackRange)
            {
                weapon.Target = null;
                return;
            }
           
            weapon.Attack();
        }
    }
    void Update()
    {
        findTargetCounter += Time.deltaTime;
        if (findTargetCounter > findTargetDelay)
        {
            FindTarget();
            findTargetCounter = 0;
        }
        attackCounter += Time.deltaTime;
        if (attackCounter > attackSpeed)
        {
            Attack();
            attackCounter = 0;
        }
    }
}
