using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CurrentWeapon : MonoBehaviour
{
    public static bool _IsKnife = false;
    public static bool _IsPistol = false;
    public static bool _IsRifle = false;
    public static bool _IsShotgun = false;  

    public Weapon[] _Weapons;
    public static Weapon _currentWeapon;


    private void Update()
    {
        WeaponSelection();
    }

    private void UseWeapon (Weapon currentWeapon)
    {      
            currentWeapon.Fire();
    }

    public void Attack (InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            UseWeapon(_currentWeapon);
        }        
    }   

    private void WeaponSelection()
    {        
        for (int i = 0 ; i < _Weapons.Length; i++)
        {
            //if (_Weapons[i]._weapon._WeaponCategory == GunType.Knife && _IsKnife)
            //{
            //    _currentWeapon = _Weapons[i];
            //    print(i);
            //}

            if(_Weapons[i]._weapon._WeaponCategory == GunType.Pistol && _IsPistol)
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
