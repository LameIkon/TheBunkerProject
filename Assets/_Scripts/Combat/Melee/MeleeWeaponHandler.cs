using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeleeWeaponHandler
{
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
        if (_meleeWeaponData.TryGetValue(meleeWeapon, out SOMeleeWeapon weaponData)) // Get the specific weapontype scriptable 
        {
            Debug.Log("attacked using: " + weaponData.SO_MeleeWeaponType);
            weaponData.PerfomAttack(attacker); // Access scriptable object method
        }
    }

}
