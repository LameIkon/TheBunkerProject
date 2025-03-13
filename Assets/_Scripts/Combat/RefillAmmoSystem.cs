using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillAmmoSystem : MonoBehaviour
{
    [SerializeField] private int _totalAmmoStockpileAmount; // Used later if stockpile should be refilled
    [SerializeField] private int _ammoStockpileAmount = 10; // Amount taken from stockpile



    private void OnTriggerEnter2D(Collider2D collision)
    {
        IIdentifiable userId = collision.GetComponent<IIdentifiable>();
        IWeaponUser weaponUserType = collision.GetComponent<IWeaponUser>();

        if (userId != null && weaponUserType != null)
        {
            string attackerId = userId.UniqueEntityId();
            RefillAmmo(attackerId, weaponUserType.GetEquippedWeapon());
        }
    }

    private void RefillAmmo(string attackerId, WeaponType weapon)
    {
        AmmunitionHandler ammoHandler = RangedWeaponHandler.GetAmmunitionHandler(attackerId, weapon);
        if (ammoHandler != null && _totalAmmoStockpileAmount >= 0)
        {
            // Apply
            ammoHandler.RestockAmmo(_ammoStockpileAmount);
            TakeFromStockPile();
        }
        else
        {
            Debug.Log("Ammo crate ran out of stock");
        }
    }

    private void TakeFromStockPile()
    {
        _totalAmmoStockpileAmount -= _ammoStockpileAmount;
    }
}
