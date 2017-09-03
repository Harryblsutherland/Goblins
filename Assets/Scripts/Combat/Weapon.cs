using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    public float range;
    public float minRange;
    public float attackRate;
    public float attackTriggerPoint;
    public float attackDuration;
    public float damage;
    public string damageType;
    public string attackanimation;
    public DamageObject damageObject;
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

    public virtual void Start()
    {
        commandManager = GetComponent<CommandManager>();
        damageObject = new DamageObject(damage, damageType);

    }
    public virtual void Attack()
    {
        commandManager.commandQueue.Insert(0, NewCommand.AttackSwingCommandAdd(transform.gameObject,attackanimation,attackDuration,attackTriggerPoint,this));
    }
    public virtual void Fire()
    {

    }


}
