using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public abstract class Weapon : AmmoCounter
{
    private float _damage;
    private GunType _gunType;
    private float _fireRate;
    private int _critChance; //25 gives 25% chance
    private float _critDamage; //needs to be in decimal. 1.25 give 125% crit damage
    private float _range; //not sure if need, raycast Linebullet still instansiate even if you arent in range to hit enemy maybe a animation so we cant see the bullet line would work

    private float _nextTimeTofire = 0f;

    //[SerializeField] private WeaponSO _weapon; //ScriptableObject Weapon needs to placed here.
    [SerializeField] private Transform _shootingPoint; //where we shoot from
    

    private void Awake()
    {
        _weapon.SetAmmoToMax();
        _damage = _weapon._Damage;
        _fireRate = _weapon._FireRate;
        _gunType = _weapon._WeaponCategory;
        _critChance = _weapon._CritChance;
        _critDamage = _weapon._CritDamage;
        _range = _weapon._Range;
    }

   // public abstract void Shoot(InputAction.CallbackContext context);

    public void Fire()
    {
        if(CurrentWeapon._currentWeapon._weapon._WeaponCategory != GunType.Knife) //If not a Knife
        {
            if (CheckFireRate() && _weapon._CurrentAmmoCount > 0)
            {
                RayCastShoot(_shootingPoint.position, _shootingPoint.right, _range);
                _weapon.ReduceAmmoByShooting(); //takes 1 from ammo amount
                //InstanceBullet(_shootingPoint);            
            }
        }

        else if (CurrentWeapon._currentWeapon._weapon._WeaponCategory == GunType.Knife)
        {
            //SETUP LOGIC FOR KNIFE
        }
        

        void RayCastShoot(Vector2 origin, Vector2 direction, float range) //can add a layermask to check if the layer is hit.
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, range); //can add a layermask to check if the layer is hit.
            GameObject bullet = Instantiate(_weapon._BulletPrefab, _shootingPoint.position, Quaternion.identity);
            LineRenderer bulletLine = bullet.GetComponent<LineRenderer>();

            if (bulletLine != null)
            {
                bulletLine.SetPosition(0, _shootingPoint.position);
                bulletLine.SetPosition(1, hit.point);
            }

            if (hit)
            {
                Health enemy = hit.transform.GetComponent<Health>();
                //Vector2 hitpos;
                if (hit.collider != null)
                {
                    if (enemy != null)
                    {
                        if(CritChance(_critChance))
                        {
                            enemy.TakeDamage(_damage * _critDamage);
                            print("A critical hit dealt: " + _damage * _critDamage + " damage!");
                        }

                        else if (!CritChance(_critChance))
                        {
                            enemy.TakeDamage(_damage);
                            print("no critical hit");
                        }
                       
                    }                    
                }
            }
            else
            {
                // Setup logic if bullet doesn't hit any colliders.
                //TEST. Probably needs something better
                bulletLine.SetPosition(0, _shootingPoint.position);
                bulletLine.SetPosition(1, _shootingPoint.position + _shootingPoint.right * 100);
                //TEST END
            }
            Destroy(bullet, 0.04f);
        }

        //public GameObject InstanceBullet(Transform origin)
        //{
        //    GameObject bullet = Instantiate(_weapon._bulletPrefab, origin.position, transform.rotation, null);

        //    return bullet;
        //}

        bool CheckFireRate()
        {
            if (Time.time > _nextTimeTofire)
            {
                _nextTimeTofire = Time.time + (1f / _fireRate);
                return true;
            }
            return false;
        }

        bool CritChance(int hitChance)
        {
            int randomNumber = Random.Range(0, 101);
            if(randomNumber <= hitChance)
            {
                return true;
            }

            else 
            { 
                return false; 
            }
        }
    }
}
