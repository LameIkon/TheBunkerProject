using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTestScript : MonoBehaviour
{
    [SerializeField] private WeaponBase _currentWeapon;
    [SerializeField] private Transform _attackPoint;


    private void Attack()
    {
        StartCoroutine(_currentWeapon.Attack(this));
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Attack();
        }
    }


    private void OnDrawGizmosSelected()
    {
        _currentWeapon.Draw(this);
    }

    public Vector3 GetAttackPoint() => _attackPoint.position;
}
