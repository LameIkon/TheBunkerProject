using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillAmmoSystem : MonoBehaviour
{
    [SerializeField] private int _totalAmmoStockpileAmount; // Used later if stockpile should be refilled
    [SerializeField] private int _ammoStockpileAmount = 10; // Amount taken from stockpile



    private void OnTriggerEnter2D(Collider2D collision)
    {
        IWeaponUser weaponUser = collision.GetComponent<IWeaponUser>();

        if (weaponUser != null)
        {
            string attackerId = weaponUser.GetAttackerId();
            RangedWeaponType weaponType = weaponUser.GetEquippedWeapon();

            RefillAmmo(attackerId, weaponType);
        }
    }

    private void RefillAmmo(string attackerId, RangedWeaponType rangedWeapon)
    {
        AmmunitionHandler ammoHandler = RangedWeaponHandler.GetAmmunitionHandler(attackerId, rangedWeapon);
        Debug.Log(ammoHandler);
        if (ammoHandler != null)
        {
            // Apply the refill amount
            ammoHandler.RestockAmmo(_ammoStockpileAmount);
            TakeFromStockPile();
        }
    }

    private void TakeFromStockPile()
    {
        _totalAmmoStockpileAmount -= _ammoStockpileAmount;
    }
}
