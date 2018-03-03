using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatusEffect : MonoBehaviour {


    public float duration;
    public bool indefinite;
    public bool positive;
    public bool removable;
    public Sprite buffIcon;
    public List<StatChange> statChanges = new List<StatChange>();
    private GameObject affectedUnit;
    private float timeElapsed;
    private AttackManager attackManager;
    private UnitInfo info;
    private NavMeshAgent movement;



    public void Start()
    {

        affectedUnit = gameObject.transform.parent.gameObject;
        attackManager = affectedUnit.GetComponent<AttackManager>();
        info = affectedUnit.GetComponent<UnitInfo>();
        movement = affectedUnit.GetComponent<NavMeshAgent>();


        foreach (var change in statChanges)
        {
            change.DefineStats(this, attackManager, info, movement);
        }

        affectedUnit.GetComponent<StatusEffectManager>().NewStatusEffect(this);
        Debug.Log("effect added");
    }


    private void Update()
    {
        if (indefinite)
        {
            return;
        }
        timeElapsed += Time.deltaTime;
        if(timeElapsed > duration)
        {
            foreach (var change in statChanges)
            {
                change.RevertStats();
                
            }
            Destroy(this);
        }
    }


}
