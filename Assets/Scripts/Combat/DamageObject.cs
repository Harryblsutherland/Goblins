using UnityEngine;


public enum DamageType
{

    Piercing,
    Slashing,
    Crushing,
    Magic
}

[System.Serializable]
public class DamageObject
{

    public float damage;
    public DamageType damagetype;
}