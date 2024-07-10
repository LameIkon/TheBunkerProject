using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{

    private float _damage;
    private GunType _gunType;
    private float _fireRate;
    private int _critChance;
    private float _critDamage;

    private float _nextTimeTofire = 0f;

    [SerializeField] private WeaponSO _Weapon;
    [SerializeField] private Transform _shootingPoint;


    private void Awake()
    {
        _damage = _Weapon._Damage;
        _fireRate = _Weapon._FireRate;
        _gunType = _Weapon._GunType; 
        _critChance = _Weapon._CritChance;
        _critDamage = _Weapon._CritDamage;   
    }

    public abstract void Shoot(InputAction.CallbackContext context);
   
    public void Fire()
    {
        if(CheckFireRate())
        {            
            InstanceBullet(_shootingPoint);     
            
        }        
    }

    public GameObject InstanceBullet (Transform origin)
    {
        GameObject bullet = Instantiate(_Weapon._bulletPrefab, origin.position, transform.rotation, null);

        return bullet;
    }
      
    bool CheckFireRate()
    {
        if(Time.time > _nextTimeTofire)
        {
            _nextTimeTofire = Time.time + (1f / _fireRate);            
            return true;
        }       
        return false;
    }
}
