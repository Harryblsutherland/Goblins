using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{

    private List<StatusEffect> statuses = new List<StatusEffect>();

    public void NewStatusEffect(StatusEffect statusObject)
    {

        statuses.Add(statusObject);

    }

}
