using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammunition", menuName = "Weapons/Ammunition")]
public class SOAmmunition : ScriptableObject
{
    [Header(("Ammo Settings"))]
    public int SO_MaxAmmoCapacity; // How much you can shoot before needing to reload
    public int SO_AmmoStorage; // Amount of magazines you 

    [Header(("Ammo loaded in gun - Dont change this"))]
    public int SO_CurrentAmmoCount;

    [Header(("Bool Settings"))]
    public bool SO_DontConsumeAmmo;
    //public bool SO_DontReload;

    public void ApplyAmmoChange(int amount)
    {
        SO_CurrentAmmoCount = Mathf.Clamp(SO_CurrentAmmoCount + amount, 0, SO_MaxAmmoCapacity); // 
    }

    public SOAmmunition CreateInstance()
    {
        SOAmmunition instance = Instantiate(this); // Clone the scriptable object
        int ammoToReload = Mathf.Min(SO_AmmoStorage, SO_MaxAmmoCapacity - SO_CurrentAmmoCount); // If you got ammo in storage then consume and reload the gun
        instance.SO_CurrentAmmoCount = ammoToReload; // Set initial ammunition
        instance.SO_AmmoStorage = ammoToReload; // remove ammo from storage
        return instance;
    }
}
