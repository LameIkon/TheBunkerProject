using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionHandler
{
    public SORangedWeapon WeaponData { get; private set; }
    public int CurrentAmmo { get; private set; }

    public int AmmoInStorage { get; private set; }


    public AmmunitionHandler(SORangedWeapon weaponData)
    {
        WeaponData = weaponData;
        AmmoInStorage = weaponData.SO_Ammunition.SO_CurrentAmmoCount;
        CurrentAmmo = weaponData.SO_Ammunition.SO_CurrentAmmoCount._Value;
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
        CurrentAmmo = 10;
    }

    private void GainMagazine()
    {

    }
}
