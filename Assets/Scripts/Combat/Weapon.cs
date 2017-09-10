using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public DamageObject damageObject;

    public float range;
    public float minRange;
    public float attackRate;
    public float attackTriggerPoint;
    public float attackDuration;
    public float damage;
    public string damageType;
    public string attackanimation;
    public bool canAttack;
    private float attackCounter;

    private UnitInfo target;
    private CommandManager commandManager;

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
        damageObject = new DamageObject(damage, damageType);
    }

    public virtual void Attack()
    {
        commandManager.InsertCommand(NewCommand.AttackSwingCommandAdd(transform.gameObject, attackanimation, attackDuration, attackTriggerPoint, this));
        canAttack = false;
    }

    public virtual void Fire()
    {

    }


}
