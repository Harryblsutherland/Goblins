using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



[System.Serializable]
public class StatChange
{

    public float rawIncrease;
    public float percentageIncrease;
    public Stats stat;
    private List<float> totalValueSwings = new List<float>();
    private List<float> values = new List<float>();
    private StatusEffect statusEffect;
    private AttackManager attackManager;
    private UnitInfo info;
    private NavMeshAgent movement;



    public void DefineStats(StatusEffect prStatusEffect, AttackManager prAttackManager, UnitInfo prInfo, NavMeshAgent prMovement)
    {


        statusEffect = prStatusEffect;
        attackManager = prAttackManager;
        info = prInfo;
        movement = prMovement;
        GetStat(stat);
        ChangeStats();
    }
    private void SetStat(Stats stat)
    {
        switch (stat)
        {
            case Stats.MaxHealth:
               info.MaxHealth += totalValueSwings[0];
                break;
            case Stats.Armor:
               info.armour += totalValueSwings[0];
                break;
            case Stats.HealthRegen:
               info.healthRegen += totalValueSwings[0];
                break;
            case Stats.MoveSpeed:
               movement.speed += totalValueSwings[0];
                break;
            case Stats.AttackDamage:
                for (int i = 0; i <attackManager.Weapons.Count; i++)
                {
                   attackManager.Weapons[i].damage += totalValueSwings[i];
                }
                break;
            case Stats.AttackPiercing:
                for (int i = 0; i < attackManager.Weapons.Count; i++)
                {
                    attackManager.Weapons[i].ArmorPeircing += totalValueSwings[i];
                }
                break;
            case Stats.AttackSpeed:
                rawIncrease = 0;
                percentageIncrease *= -1;
                for (int i = 0; i <attackManager.Weapons.Count; i++)
                {
                    var o = i * 3;
                   attackManager.Weapons[i].attackRate += totalValueSwings[o];
                   attackManager.Weapons[i].attackTriggerPoint += totalValueSwings[o+1];
                   attackManager.Weapons[i].attackDuration += totalValueSwings[o+2];

                }
                break;
            case Stats.AttackRange:
                for (int i = 0; i <attackManager.Weapons.Count; i++)
                {
                   attackManager.Weapons[i].range += totalValueSwings[i];
                }
                break;
        }
    }
    private float GetStat(Stats stat)
    {
        switch (stat)
        {
            case Stats.MaxHealth:
                values.Add(info.MaxHealth);
                break;
            case Stats.Armor:
                values.Add(info.armour);
                break;
            case Stats.HealthRegen:
                values.Add(info.healthRegen);
                break;
            case Stats.MoveSpeed:
                values.Add(movement.speed);
                break;
            case Stats.AttackDamage:
                for (int i = 0; i <attackManager.Weapons.Count; i++)
                {
                    values.Add(attackManager.Weapons[i].damage);
                }
                break;
            case Stats.AttackPiercing:
                for (int i = 0; i < attackManager.Weapons.Count; i++)
                {
                    values.Add(attackManager.Weapons[i].ArmorPeircing);
                }
                break;
            case Stats.AttackSpeed:
                rawIncrease = 0;
                percentageIncrease *= -1;
                for (int i = 0; i <attackManager.Weapons.Count; i++)
                {

                    
                    values.Add(attackManager.Weapons[i].attackRate);
                    values.Add(attackManager.Weapons[i].attackTriggerPoint);
                    values.Add(attackManager.Weapons[i].attackDuration);

                }
                break;
            case Stats.AttackRange:
                for (int i = 0; i <attackManager.Weapons.Count; i++)
                {
                    values.Add(attackManager.Weapons[i].range);
                }
                break;
        }
        return 0;
    }

    private void ChangeStats()
    {
        for (int i = 0; i < values.Count; i++)
        {
            totalValueSwings.Add(0);
            totalValueSwings[i] = (values[i] * percentageIncrease) + rawIncrease;
        }
        SetStat(stat);
    }
    public void RevertStats()
    {
        for (int i = 0; i < values.Count; i++)
        {
            totalValueSwings[i] *= -1;
        }
        SetStat(stat);
    }
    
}
