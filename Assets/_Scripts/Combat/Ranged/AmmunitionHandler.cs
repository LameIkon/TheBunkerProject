using System.Collections;
using UnityEngine;

public class AmmunitionHandler
{
    private SOAmmunition ammoData; // Get the ammodata. Used to initialize the amount you start with and what settings related to the ammo usage
    private SORangedWeapon weaponData; // Used to get the reload time needed for that weapon

    public AmmunitionHandler(SORangedWeapon weaponData)
    {
        this.weaponData = weaponData;
        ammoData = weaponData.SO_Ammunition.CreateInstance(); // Create a new instance of ammo for the entity
    }

    public bool HasAmmo()
    {
        return ammoData.SO_CurrentAmmoCount > 0;
    }

    public void ConsumeAmmo()
    {
        if (ammoData.SO_DontConsumeAmmo) // If boolean checked you will never consume ammo but still do damage
        {
            return;
        }

        if (HasAmmo())
        {
            ammoData.ApplyAmmoChangeInWeapon(-1); // Decrease ammo count
        }
    }

    public void RestockAmmo(int amount) // For restocking personal ammo storage
    {
        ammoData.ApplyAmmoChangeToStorage(amount);  // Change the storage amount
    }

    public void ReloadWeapon(string entityId, RangedWeaponType rangedWeapon)
    {
        if (!CheckIfMissingAmmo()) // Check if entity has ammo
        {
            Debug.Log("Not enough ammo to reload.");
            RangedWeaponHandler.RemoveWeaponFromReloading(entityId, rangedWeapon);
            return; // Dont reload
        }
        GlobalWeaponManager.Instance.StartReloadingWeapon(this, weaponData.reloadTime, entityId); // Start reloading.
    }

    public void StopReloading(string entityId, RangedWeaponType rangedWeapon)
    {
        GlobalWeaponManager.Instance.StopReloadingWeapon(entityId, rangedWeapon); // Request GlobalWeaponManager to stop the reload coroutine
    }

    private bool CheckIfMissingAmmo() 
    {
        bool haveAmmo = ammoData.SO_AmmoStorage > 0; // Look in your personal storage for ammo. Can only reload if you are missing ammo from magazine
        return haveAmmo;
    }

    public bool CheckIfHaveMaxAmmo()
    {
        bool haveMaxAmmo = ammoData.SO_MaxMagazineCapacity == ammoData.SO_AmmoStorage; // Look in your personal storage for ammo. If you have max ammo in magazine then dont reload
        return haveMaxAmmo;
    }

    public IEnumerator ReloadCoroutine(float reloadTime, string entityId)
    {
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime); 

        int ammoToReload = Mathf.Min(ammoData.SO_AmmoStorage, ammoData.SO_MaxMagazineCapacity - ammoData.SO_CurrentAmmoCount); // Check how much ammo needs to be refilled
        ammoData.ApplyAmmoChangeInWeapon(ammoToReload); // Increase ammo count
        ammoData.SO_AmmoStorage -= ammoToReload; // Decrease reserve ammo
        GlobalWeaponManager.Instance.ReloadCoroutineFinished();
        RangedWeaponHandler.RemoveWeaponFromReloading(entityId, weaponData.SO_RangedWeapontype); // remove from dictionary over reloading allow you to shoot again

        if (entityId == PlayerController.s_PlayerId)
        {
            RangedWeaponHandler.UpdatePlayerUI(weaponData.SO_RangedWeapontype, entityId);
        }


        Debug.Log($"Reload complete. Current Ammo: {ammoData.SO_CurrentAmmoCount} Ammo Storage: {ammoData.SO_AmmoStorage}");
    }


    // For player Display:
    public int DisplayCurrentAmmo()
    {
        int currentAmmo = ammoData.SO_CurrentAmmoCount;
        //Debug.Log(ammoData.SO_CurrentAmmoCount);
        return currentAmmo;
    }

    public int DisplayTotalAmmo()
    {
        int totalAmmo = ammoData.SO_AmmoStorage;
        //Debug.Log(ammoData.SO_AmmoStorage);
        return totalAmmo;
    }
}
