using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillAmmoSystem : MonoBehaviour
{
    /// <summary>
    /// Simple refill system to test that entities can replenish ammo. This should be developed further to check for each type of ammo it has in stockpile
    /// </summary>
    [SerializeField] private int _totalAmmoStockpileAmount; // Used later if stockpile should be refilled
    [SerializeField] private int _ammoStockpileAmount = 10; // Amount taken from stockpile



    private void OnTriggerEnter2D(Collider2D collision)
    {
        IIdentifiable userId = collision.GetComponent<IIdentifiable>(); // We need to know which user has entered the trigger
        IWeaponUser weaponUserType = collision.GetComponent<IWeaponUser>(); // We need to know what weapon the user is holding. This should be changed later, because it sounds like a problem that you must hold the specific weapon

        if (userId != null && weaponUserType != null)
        {
            string attackerId = userId.GetUniqueEntityId();
            RefillAmmo(attackerId, weaponUserType.GetEquippedWeapon());
        }
    }

    private void RefillAmmo(string attackerId, WeaponType weapon)
    {
        AmmunitionHandler ammoHandler = RangedWeaponHandler.GetAmmunitionHandler(attackerId, weapon); // Get the user, which weapon and what ammo it uses
        if (ammoHandler != null && _totalAmmoStockpileAmount >= 0)
        {
            // Apply
            ammoHandler.RestockAmmo(_ammoStockpileAmount);
            TakeFromStockPile();
            if (WeaponTypes.TryGetRangedType(weapon, out RangedWeaponType rangedWeaponType)) // Only for player to update UI
            {
                RangedWeaponHandler.UpdatePlayerUI(rangedWeaponType, attackerId);
            }
        }
        else
        {
            Debug.Log("Ammo crate ran out of stock or not the correct weapon");
        }
    }

    private void TakeFromStockPile() // Ammo in this stockpilel
    {
        _totalAmmoStockpileAmount -= _ammoStockpileAmount;
    }
}
