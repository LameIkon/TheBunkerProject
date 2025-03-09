using System;
using UnityEngine;
using UnityEngine.InputSystem;



public class CurrentWeapon : MonoBehaviour
{
    public static event Action OnWeaponChanged; // Event 

    public RangedWeaponSO currentRangedWeapon;
    public MeleeWeaponSO currentMeleeWeapon;

    //private WeaponType _currentWeaponType;
    public WeaponType _WeaponCategory;
    private bool _isFiring = false; //we use this in order to have AUTO fire when holding down

    public Weapon[] _Weapons;
    public static Weapon _currentWeapon;


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
        WeaponSelection();
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

    public void WeaponSelection()
    {
        // Iterate over weapons and select based on WeaponType
        foreach (Weapon weapon in _Weapons)
        {
            if (weapon._WeaponCategory == _currentWeaponType)
            {
                _currentWeapon = weapon;
                break;
            }
        }
        OnWeaponChanged.Invoke(); // For other scripts to trigger. For example players animation, that needs to know what weapon to hold
    }

    public void SetCurrentWeapon(WeaponType weaponType) // Called by other scripts
    {
        _currentWeaponType = weaponType;
    }



}
