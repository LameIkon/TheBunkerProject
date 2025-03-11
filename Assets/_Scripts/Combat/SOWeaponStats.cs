using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOWeaponStats : ScriptableObject
{
    [Space(5f)]
    [Header("Stats")]
    public float SO_MinDamage;
    public float SO_MaxDamage; 
    public float SO_AttackSpeed; // Speed of the attacks (attacks per second)
    public float SO_AttackRange; // How far the weapon can hit (range of attack)

    [Space(5f)]
    [Header("Critical Damage")]
    public int SO_CritChance; // Chance to deal critical damage
    public float SO_CritMultiplyer; // Extra damage from a critical hit


    protected int DamageOutput()
    {
        float damage = Random.Range(SO_MinDamage, SO_MaxDamage);

        if (Random.Range(0, 100) <= SO_CritChance) 
        {
            damage *= SO_CritMultiplyer;
        }

        return (int)Mathf.Floor(damage); 
    }
}
