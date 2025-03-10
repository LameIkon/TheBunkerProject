using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CurrentWeapon : MonoBehaviour
{
    public static event Action OnWeaponChanged; // Event 

    public RangedWeaponSO currentRangedWeapon;
    public MeleeWeaponSO currentMeleeWeapon;

    //private WeaponType _currentWeaponType;
    public WeaponType _WeaponType;
    private bool _isFiring = false; //we use this in order to have AUTO fire when holding down

    public Weapon[] _Weapons;
    public static Weapon _currentWeapon;
    

    private Dictionary<int, Weapon> _weas;

    public enum WeaponType
    {
        Unarmed,
        Knife,
        Pistol,
        Rifle,
        Shotgun
    }


    private void Update()
    {
        EquipWeapon();
        if (_isFiring)
        {
            UseWeapon(_currentWeapon);
        }
    }

    private void UseWeapon (Weapon currentWeapon)
    {      
        currentWeapon.Fire();
    }

    public void Attack (InputAction.CallbackContext context)
    {       
        if (context.started) //Fires an event whenever action/key is pressed. Together with the one below the whole method basicly chekcs if the key is hold down. 
        {
            _isFiring = true;
           
        }

        if (context.canceled) //needs to be here to fire an event whenever we let go of the "action"/key.
        {
            _isFiring = false;
        }
       
    }   

    public void EquipWeapon()
    {
        // Iterate over weapons and select based on WeaponType
        foreach (WeaponType weapon in _weaponType)
        {
            if (weapon._WeaponCategory == _currentWeaponType)
            {
                _currentWeapon = weapon;
                break;
            }
        }
        OnWeaponChanged.Invoke(); // For other scripts to trigger. For example players animation, that needs to know what weapon to hold
    }

    //public void SetCurrentWeapon(WeaponType weaponType) // Called by other scripts
    //{
    //    _currentWeaponType = weaponType;
    //}

    public void ChangeWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log(context);
        }
    }


}
