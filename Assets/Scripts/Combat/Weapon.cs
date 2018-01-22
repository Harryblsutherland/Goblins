using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public List<DamageObject> damageObject = new List<DamageObject>();
    public float range;
    public float minRange;
    public float attackRate;
    public float attackTriggerPoint;
    public float attackDuration;
    public bool canAttack;
    public TargetingType targetingType;
    protected float attackCounter;
    protected UnitInfo target;
    protected CommandManager commandManager;

    public UnitInfo Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    private void Update()
    {
        if (canAttack)
        {
            return;
        }
        attackCounter += Time.deltaTime;
        if (attackCounter > attackRate)
        {
            canAttack = true;
            attackCounter = 0;
        }
    }

    public virtual void Start()
    {
        commandManager = GetComponent<CommandManager>();
    }

    public virtual void Attack()
    {
        if (canAttack)
        {
            commandManager.InsertCommand(Cmd_AttackSwing.New(transform.gameObject, attackDuration, attackTriggerPoint, this));
            canAttack = false;

        }
    }

    public virtual void Fire()
    {

    }


}
