using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammunition", menuName = "Weapons/Ammunition")]
public class SOAmmunition : ScriptableObject
{
    [Header(("Ammo Settings"))]
    public int SO_MaxAmmoCapacity; // How much you can shoot before needing to reload
    public int SO_AmmoStorage; // Amount of ammo you have in your personal storage. This can be used by other scripts to check amount and to refill

    [Header(("Ammo loaded in gun - Dont change this"))]
    public int SO_CurrentAmmoCount; // Need to be public to be accessable otherwise dont touch this.

    [Header(("Bool Settings"))]
    public bool SO_DontConsumeAmmo; // Should you be able to fire nonstop without the need to reload

    public void ApplyAmmoChangeInWeapon(int amount)
    {
        SO_CurrentAmmoCount = Mathf.Clamp(SO_CurrentAmmoCount + amount, 0, SO_MaxAmmoCapacity); // Cant go below 0 or max value. Apply amount to current ammo. Reason we apply instead of replace is because you might have some ammo left over even when reloading
    }

    public void ApplyAmmoChangeToStorage(int amount)
    {
        SO_AmmoStorage += amount;
    }

    public SOAmmunition CreateInstance() // Initialization
    {
        SOAmmunition instance = Instantiate(this); // Clone this ScriptableObject
        int ammoToReload = Mathf.Min(SO_AmmoStorage, SO_MaxAmmoCapacity - SO_CurrentAmmoCount); // If you got ammo in storage then consume and reload the gun
        instance.SO_CurrentAmmoCount = ammoToReload; // Set initial ammunition
        instance.SO_AmmoStorage -= ammoToReload; // remove ammo from storage
        return instance;
    }
}
