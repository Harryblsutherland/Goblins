using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetingType
{
    Nearest,
    Random,
    Weakest,
    Strongest
}
public class TargetFinding : MonoBehaviour
{

    private bool aggressive;
    public float attackFindRange = 25;
    public PlayerSetupDefinition player;
    public TargetingType targetingType;
    private UnitInfo currentTarget;
    private float findTargetCounter;
    private float findTargetDelay = 1;
    private CommandManager commandManager;

    private void Start()
    {
        player = GetComponent<Player>().Info;
        commandManager = GetComponent<CommandManager>();

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
            if (aggressive == true)
            {
                FindAndAttack();
            }
        }
    }

    private UnitInfo FindWeakestUnitInRange()
    {

        float LowestEnemyHealth = 9999;
        UnitInfo WeakestEnemy = null;
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
                    if (unit.GetComponent<UnitInfo>().currentHealth < LowestEnemyHealth)
                    {
                        LowestEnemyHealth = unit.GetComponent<UnitInfo>().currentHealth;
                        WeakestEnemy = unit.GetComponent<UnitInfo>();
                    }
                }
            }
        }
        return WeakestEnemy;
    }
    private UnitInfo FindStrongesUnitInRange()
    {

        float HighestEnemyHealth = 0;
        UnitInfo StrongestEnemy = null;
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
                    if (unit.GetComponent<UnitInfo>().currentHealth > HighestEnemyHealth)
                    {
                        HighestEnemyHealth = unit.GetComponent<UnitInfo>().currentHealth;
                        StrongestEnemy = unit.GetComponent<UnitInfo>();
                    }
                }
            }
        }
        return StrongestEnemy;
    }
    public UnitInfo FindRandomUnitInRange()
    {

        float range = GetComponent<AttackManager>().GetMinimumWeaponRange();
        foreach (var _player in RtsManager.Current.Players)
        {
            if (_player == player)
            {
                continue;
            }
            foreach (var unit in _player.ActiveUnits)
            {
                if (Vector3.Distance(unit.transform.position, transform.position) < range)
                {
                    return unit.GetComponent<UnitInfo>();

                }
            }
        }
        return null;
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
    public UnitInfo FindTarget()
    {
        // if there is a specific target choose that
        if (currentTarget != null)
        {
            return null;
        }

        switch (targetingType)
        {
            case TargetingType.Nearest:
                return FindNearestEnemyInRange();
            case TargetingType.Random:
                return FindRandomUnitInRange();
            case TargetingType.Strongest:
                return FindStrongesUnitInRange();
            case TargetingType.Weakest:
                return FindWeakestUnitInRange();
                
        }
        
        Debug.Log("Target Spotted : " + currentTarget);
        return null;
    }
    void Update()
    {
        if (currentTarget == null && Aggressive)
        {
            findTargetCounter += Time.deltaTime;
            if (findTargetCounter > findTargetDelay)
            {
                FindAndAttack();
                findTargetCounter = 0;
            }
        }
    }
    void FindAndAttack()
    {
        currentTarget = FindTarget();
        if (currentTarget != null)
        {
            commandManager.InsertCommand(Cmd_Attack.New(transform.gameObject, currentTarget.gameObject, 25));
        }
    }
}

