using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun")]
public class WeaponSO : ScriptableObject
{
    public GunType _GunType;

    public float _Damage;
    public float _FireRate;
    public int _CritChance;
    public float _CritDamage;
    public float _Range;
    public GameObject _bulletPrefab;

}

public enum GunType
{
    Pistol,
    Shotgun,
    Rifle
}