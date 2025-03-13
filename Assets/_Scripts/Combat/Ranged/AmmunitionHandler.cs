using System.Collections;
using UnityEngine;

public class AmmunitionHandler
{
    private SOAmmunition ammoData;
    private SORangedWeapon weaponData; // Used to get the reload time needed for that weapon

    public AmmunitionHandler(SORangedWeapon weaponData)
    {
        this.weaponData = weaponData;
        ammoData = weaponData.SO_Ammunition.CreateInstance(); // Create a new instance of ammo for the player
    }

    public int GetCurrentAmmo() => ammoData.SO_CurrentAmmoCount;

    public bool HasAmmo()
    {
        return ammoData.SO_CurrentAmmoCount > 0;
    }

    public void ConsumeAmmo()
    {
        if (ammoData.SO_DontConsumeAmmo)
        {
            Debug.Log($"You dont consume ammo. CurrentAmmo: {ammoData.SO_CurrentAmmoCount} AmmoStorage: {ammoData.SO_AmmoStorage}");
            return;
        }

        if (HasAmmo())
        {
            Debug.Log(ammoData.SO_CurrentAmmoCount-1);
            Debug.Log(ammoData.SO_AmmoStorage);
            ammoData.ApplyAmmoChange(-1); // Decrease ammo count
        }
    }

    public void ReloadWeapon()
    {
        if (!CheckIfCanReload()) // Check if entity has ammo
        {
            Debug.Log("Not enough ammo to reload.");
            return; // Dont reload
        }
        GlobalWeaponManager.Instance.StartReloadingWeapon(this, weaponData.reloadTime);
        //int ammoToReload = Mathf.Min(ammoData.SO_AmmoStorage, ammoData.SO_MaxAmmoCapacity - ammoData.SO_CurrentAmmoCount); 
        //ammoData.ApplyAmmoChange(ammoToReload); // Increase ammo count
        //ammoData.SO_AmmoStorage -= ammoToReload; // Decrease reserve ammo
    }

    private bool CheckIfCanReload()
    {
        bool haveAmmo = ammoData.SO_AmmoStorage > 0;
        return haveAmmo;
    }

    public IEnumerator ReloadCoroutine(float reloadTime)
    {
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime); 

        int ammoToReload = Mathf.Min(ammoData.SO_AmmoStorage, ammoData.SO_MaxAmmoCapacity - ammoData.SO_CurrentAmmoCount);
        ammoData.ApplyAmmoChange(ammoToReload); // Increase ammo count
        ammoData.SO_AmmoStorage -= ammoToReload; // Decrease reserve ammo

        Debug.Log($"Reload complete. Current Ammo: {ammoData.SO_CurrentAmmoCount} Ammo Storage: {ammoData.SO_AmmoStorage}");
    }
}
