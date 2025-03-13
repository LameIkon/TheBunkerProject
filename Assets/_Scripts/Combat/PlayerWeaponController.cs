using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private WeaponType _currentWeaponType = WeaponType.Unarmed;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private string _playerId;

    private void OnEnable()
    {
        WeaponSelectionHandler.s_OnWeaponChanged += WeaponChecker;
    }

    private void OnDisable()
    {
        WeaponSelectionHandler.s_OnWeaponChanged -= WeaponChecker;
    }

    private void Awake()
    {
        _playerId = GetComponent<PlayerController>().GetUniqueEntityId();
    }

    private void WeaponChecker(WeaponType state) // Called from event to check current weapon
    {
        _currentWeaponType = state;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) //Fires an event whenever action/key is pressed.  
        {
            GlobalWeaponManager.Attack(_currentWeaponType, _attackPoint, _playerId);
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed) //Fires an event whenever action/key is pressed.  
        {
            GlobalWeaponManager.Instance.ReloadWeapon(_currentWeaponType, _playerId);
        }
    }
}
