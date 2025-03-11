using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MeleeWeaponHandler
{
    private static Dictionary<MeleeWeaponType, float> _weaponCooldowns = new Dictionary<MeleeWeaponType, float>();
    private static Dictionary<MeleeWeaponType, SOMeleeWeapon> _meleeWeaponData;


    public static void Initialize(SOMeleeWeapon[] allMeleeWeaponTypes)
    {
        _meleeWeaponData = new Dictionary<MeleeWeaponType, SOMeleeWeapon>();

        foreach (SOMeleeWeapon meleeWeapon in allMeleeWeaponTypes)
        {
            _meleeWeaponData[meleeWeapon.SO_MeleeWeaponType] = meleeWeapon;
        }
    }


    public static void PerformMeleeAttack(MeleeWeaponType meleeWeapon, Transform attacker)
    {     
        if (_weaponCooldowns.ContainsKey(meleeWeapon) && _weaponCooldowns[meleeWeapon] > 0f) // Check if the weapon is on cooldown
        {
            return; 
        }

        if (_meleeWeaponData.TryGetValue(meleeWeapon, out SOMeleeWeapon weaponData)) // Get the specific weapontype scriptable 
        {
            Debug.Log("attacked using: " + weaponData.SO_MeleeWeaponType);
            weaponData.PerfomAttack(attacker); // Access scriptable object method
            _weaponCooldowns[meleeWeapon] = weaponData.SO_AttackRate;
        }
    }

    public static void UpdateCooldowns()
    {
        List<MeleeWeaponType> weaponsToRemove = new List<MeleeWeaponType>();

        foreach (MeleeWeaponType key in _weaponCooldowns.Keys.ToList())
        {
            // Reduce the cooldown time
            _weaponCooldowns[key] -= Time.deltaTime;

            // If cooldown is finished, remove it from the dictionary
            if (_weaponCooldowns[key] <= 0f)
            {
                weaponsToRemove.Add(key);
            }
        }

        // Clean up the cooldown dictionary
        foreach (MeleeWeaponType weapon in weaponsToRemove)
        {
            _weaponCooldowns.Remove(weapon);
        }
    }

}
