using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;

public class CurrentWeapon : MonoBehaviour
{
    public static bool _IsKnife = false;
    public static bool _IsPistol = false;
    public static bool _IsRifle = false;
    public static bool _IsShotgun = false;

    private bool _isFiring = false; //we use this in order to have AUTO fire when holding down

    public Weapon[] _Weapons;
    public static Weapon _currentWeapon;


    private void Update()
    {
        WeaponSelection();

        if(_isFiring)
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
        if (context.started && !context.canceled) //Fires an event whenever action/key is pressed. Together with the one below the whole method basicly chekcs if the key is hold down. 
        {
            _isFiring = true;
           
        }

        if(context.canceled) //needs to be here to fire an event whenever we let go of the "action"/key.
        {
            _isFiring = false;
        }
       
    }   

    private void WeaponSelection()
    {        
        for (int i = 0 ; i < _Weapons.Length; i++)
        {
            if (_Weapons[i]._weapon._WeaponCategory == GunType.Knife && _IsKnife)
            {
                _currentWeapon = _Weapons[i];
            }

            else if (_Weapons[i]._weapon._WeaponCategory == GunType.Pistol && _IsPistol)
            {
                _currentWeapon = _Weapons[i];               
            }

            else if (_Weapons[i]._weapon._WeaponCategory == GunType.Rifle && _IsRifle)
            {
                _currentWeapon = _Weapons[i];                
            }

            else if (_Weapons[i]._weapon._WeaponCategory == GunType.Shotgun && _IsShotgun)
            {
                _currentWeapon = _Weapons[i];                
            }            
        }        
    }    

}
