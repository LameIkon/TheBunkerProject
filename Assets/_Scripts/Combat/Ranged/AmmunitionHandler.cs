using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionHandler
{
    public SORangedWeapon WeaponData { get; private set; }
    public int CurrentAmmo { get; private set; }


    public AmmunitionHandler(SORangedWeapon weaponData)
    {
        WeaponData = weaponData;
        CurrentAmmo = weaponData.SO_CurrentAmmoCount._Value;
    }

    public bool HasAmmo() => CurrentAmmo > 0;

    public void ConsumeAmmo()
    {
        if (CurrentAmmo > 0)
        {
            CurrentAmmo--;
            Debug.Log(CurrentAmmo);
        }
    }

    public void ReloadWeapon()
    {
        CurrentAmmo = WeaponData.SO_MaxAmmoCapacity;
    }

    private void GainMagazine()
    {

    }
}
