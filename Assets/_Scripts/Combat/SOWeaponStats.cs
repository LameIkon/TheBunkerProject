using UnityEngine;

public class SOWeaponStats : ScriptableObject
{
    [Header("Stats")]
    public float SO_MinDamage;
    public float SO_MaxDamage; 
    public float SO_AttackRate; // Speed of the attacks (attacks per second)
    public float SO_AttackRange; // How far the weapon can hit (range of attack)

    [Header("Critical Damage")]
    [Tooltip("CritChance is from 0-100. Each number is considered as %.")]
    public int SO_CritChance; // Chance to deal critical damage
    [Tooltip(" CritMultiplier will multiply with the given number. so the number 2 means you multiply 'damage*2'")]
    public float SO_CritMultiplyer; // Extra damage from a critical hit

    protected int DamageOutput()
    {
        float damage = Random.Range(SO_MinDamage, SO_MaxDamage);

        if (Random.Range(0, 100) <= SO_CritChance) 
        {
            Debug.Log("crit strike");
            damage *= SO_CritMultiplyer;
        }
        return (int)Mathf.Floor(damage); 
    }
}
