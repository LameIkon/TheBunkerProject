using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : ScriptableObject
{
    [Space(5f)]
    [Header("Stats")]
    public float _Damage; // How much damage the weapon does
    public float _AttackSpeed; // Speed of the attacks (attacks per second)
    public float _Range; // How far the weapon can hit (range of attack)

    [Space(5f)]
    [Header("Critical Damage")]
    public int _CritChance; // Chance to deal critical damage
    public float _CritDamage; // Extra damage from a critical hit
}
