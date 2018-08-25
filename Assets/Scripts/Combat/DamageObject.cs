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
    public GameObject originObject;
    public float damage;
    public float Piercing;
    public DamageType damagetype;

    public DamageObject(float prDamage, float prPiercing, DamageType prDamageType)
    {
        damage = prDamage;
        damagetype = prDamageType;
    }
}