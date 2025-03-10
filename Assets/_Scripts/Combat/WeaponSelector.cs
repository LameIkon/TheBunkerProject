using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class WeaponSelector : MonoBehaviour
{
    public static event Action<WeaponType> OnWeaponChanged; // Event 
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private WeaponType _currentWeaponType; // This is used for other scripts to access the weapon you are currently holding   
    private Dictionary<int, WeaponType> _allWeapontypes;



    private void Start()
    {
        PopulateWeaponTypes();
    }

    public void EquipWeapon(int bindindIndex)
    {
        if (_allWeapontypes.TryGetValue(bindindIndex, out WeaponType selectedWeapon))
        {
            _currentWeaponType = selectedWeapon; // Update the selected weapon type
        }
            OnWeaponChanged.Invoke(_currentWeaponType); // For other scripts to trigger. For example players animation, that needs to know what weapon to hold
    }

    private void PopulateWeaponTypes()
    {
        _allWeapontypes = new Dictionary<int, WeaponType> // InputAction index location and corresponding weapon
        {
            // A lot of temporary weapontypes. Will be changed
            {0, WeaponType.Unarmed },
            {1, WeaponType.Knife },
            {2, WeaponType.Rifle },
            {3, WeaponType.Shotgun },
            {4, WeaponType.Unarmed },
            {5, WeaponType.Unarmed },
            {6, WeaponType.Unarmed },
            {7, WeaponType.Unarmed },
            {8, WeaponType.Unarmed },
        };
    }


    public void ChangeWeapon(InputAction.CallbackContext context)
    {
        Debug.Log("ChangeWeapon");
        if (context.performed)
        {
            int bindingIndex = context.action.GetBindingIndexForControl(context.control); // Read the key pressed
            Debug.Log("the index pressed: " + bindingIndex);
            EquipWeapon(bindingIndex);
        }
    }


    //[SerializeField] private RangedWeaponSO _rangedWeapon;
    //[SerializeField] private MeleeWeaponSO _meleeWeapon;

    //private bool _isFiring = false; //we use this in order to have AUTO fire when holding down
    //private WeaponType _currentWeaponType;
    //public Weapon[] _Weapons;
    //public static Weapon _currentWeapon;

    //private void Update()
    //{
    //    //EquipWeapon();
    //    if (_isFiring)
    //    {
    //        UseWeapon(_currentWeapon);
    //    }
    //}

    //public void SetCurrentWeapon(WeaponType weaponType) // Called by other scripts
    //{
    //    _currentWeaponType = weaponType;
    //}

    //private void UseWeapon (Weapon currentWeapon)
    //{      
    //    currentWeapon.Fire();
    //}

    //public void Attack (InputAction.CallbackContext context)
    //{       
    //    if (context.started && !context.canceled) //Fires an event whenever action/key is pressed. Together with the one below the whole method basicly chekcs if the key is hold down. 
    //    {
    //        _isFiring = true;

    //    }

    //    if (context.canceled) //needs to be here to fire an event whenever we let go of the "action"/key.
    //    {
    //        _isFiring = false;
    //    }

    //}  
}
