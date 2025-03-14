using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private WeaponType _currentWeaponType = WeaponType.Unarmed;
    [SerializeField] private Transform _attackPoint; // Attack point. This need improvement to know location better. a gun and a rifle should have different points
    [SerializeField] private string _playerId; // Used to update ammo UI

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
        if (context.started)  
        {
            InvokeRepeating(nameof(PerformAttack),0.01f, 0.01f); // Repeatedly call the fire while holding attack down
        }
        else if (context.canceled)
        {
            CancelInvoke(nameof(PerformAttack)); // Cancel the repeat call
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GlobalWeaponManager.Instance.ReloadWeapon(_currentWeaponType, _playerId);
        }
    }

    private void PerformAttack()
    {
        GlobalWeaponManager.Attack(_currentWeaponType, _attackPoint, _playerId);
    }
}
