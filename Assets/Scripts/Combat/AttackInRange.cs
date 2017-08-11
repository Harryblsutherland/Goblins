using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInRange : MonoBehaviour {

    public GameObject weaponHit;
    public float findTargetDelay = 1;
    public float attackRange = 20;
    public float attackSpeed = 0.25f;
    public float attackDamage = 1;

    private ShowUnitInfo target;
    public PlayerSetupDefinition player;
    public float findTargetCounter = 0;
    public float attackCounter = 0;

	// Use this for initialization
	void Start () {
        player = GetComponent<Player>().Info;

	}
    void FindTarget()
    {
        if (target != null)
        {
            return;
        }
        foreach (var _player in RtsManager.Current.Players)
        {
            if (_player == player)
            {
                continue;
            }
            foreach (var unit in _player.ActiveUnits)
            {
                if (Vector3.Distance(unit.transform.position, transform.position) < attackRange)
                {
                    target = unit.GetComponent<ShowUnitInfo>();
                    return;
                }
            }
        }
    }
	
	// Update is called once per frame

    void Attack()
    {
        if (target == null)
        {
            return;
        }
        if (Vector3.Distance(target.transform.position, transform.position) > attackRange)
        {
            target = null;
            return;
        }
        target.CurrentHealth -= attackDamage;
        GameObject.Instantiate(weaponHit, target.transform.position, Quaternion.identity);
    }
    void Update ()
    {
        findTargetCounter += Time.deltaTime;
        if (findTargetCounter > findTargetDelay)
        {
            FindTarget();
            findTargetCounter = 0;
        }
        attackCounter += Time.deltaTime;
        if(attackCounter > attackSpeed)
        {
            Attack();
            attackCounter = 0;
        }
	}
}
