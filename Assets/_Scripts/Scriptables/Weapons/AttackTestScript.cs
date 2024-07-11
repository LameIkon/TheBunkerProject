using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTestScript : MonoBehaviour
{
    [SerializeField] private WeaponBase _currentWeapon;

    private void Attack() 
    {
        _currentWeapon.Attack(this);
    }


    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Attack();
        }
    }

}
