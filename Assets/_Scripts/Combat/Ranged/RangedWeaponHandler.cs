using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RangedWeaponHandler
{
    private static Dictionary<RangedWeaponType, SORangedWeapon> _rangedWeaponData;


    public static void Initialize(SORangedWeapon[] allRangedWeaponTypes)
    {
        _rangedWeaponData = new Dictionary<RangedWeaponType, SORangedWeapon>();

        foreach (SORangedWeapon rangedWeapon in allRangedWeaponTypes)
        {
            _rangedWeaponData[rangedWeapon.SO_RangedWeapontype] = rangedWeapon;
        }
    }


    public static void PerfomRangedAttack(RangedWeaponType rangedWeapon)
    {
        if (_rangedWeaponData.TryGetValue(rangedWeapon, out SORangedWeapon weaponData))
        {   
            
        }
    }
}
