using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class WeaponSelectionHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    private WeaponType _currentWeaponType; // This is used for other scripts to access the weapon you are currently holding. Parameter for OnWeaponChanged Event    
    public static event Action<WeaponType> s_OnWeaponChanged; // Event 
    private static readonly Dictionary<int, WeaponType> s_allWeapontypes = new() // Compare PlayerInput index location to the corresponding weapontype
    {
        // A lot of temporary weapontypes. Will be changed
        {0, WeaponType.Unarmed },
        {1, WeaponType.Knife },
        {2, WeaponType.Rifle },
        {3, WeaponType.Shotgun },
        {4, WeaponType.Pistol },
        {5, WeaponType.Unarmed },
        {6, WeaponType.Unarmed },
        {7, WeaponType.Unarmed },
        {8, WeaponType.Unarmed },
    };

    private void EquipWeapon(int bindindIndex)
    {
        if (s_allWeapontypes.TryGetValue(bindindIndex, out WeaponType selectedWeapon)) // Check the index and take the corresponding weapon
        {
            _currentWeaponType = selectedWeapon; // Update the selected weapon type
        }
            s_OnWeaponChanged.Invoke(_currentWeaponType); // For other scripts to trigger. For example players animation, that needs to know what weapon to hold
    }


    public void ChangeWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int bindingIndex = context.action.GetBindingIndexForControl(context.control); // Read the key pressed
            EquipWeapon(bindingIndex);
        }
    }
}
