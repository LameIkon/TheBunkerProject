using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeapon", menuName = "Weapons/RangedWeapon")]
public class SORangedWeapon : SOWeaponStats
{
    public RangedWeaponType SO_RangedWeapontype;

    [Space(5f)] 
    [Header("Ammunition")]
    public GameObject _BulletPrefab;
    public int _MaxAmmoCapacity;
    public IntReferencer SO_Magazine;
    public IntVariable SO_CurrentAmmoCount;
    private bool _fullMagazine;
    private bool _emptyMagazine;

    public void SetAmmoToMax() //Used in Awake in Weapon.cs
    {
        SO_CurrentAmmoCount.SetValue(_MaxAmmoCapacity);
    }

    public void UpdateAmmoCount()
    {
        SO_Magazine.SetValue(SO_CurrentAmmoCount);
        CheckIfMagazineIsFullOrEmpty();
    }

    private void CheckIfMagazineIsFullOrEmpty()
    {
        _fullMagazine = (SO_Magazine.GetValue() == _MaxAmmoCapacity);
        _emptyMagazine = (SO_Magazine.GetValue() <= 0);
    }

    public void ReduceAmmoByShooting()
    {
        if (!_emptyMagazine)
        {
            SO_CurrentAmmoCount.ApplyChange(-1); 
        }
    }

    public void GainAmmo()
    {
        if (!_fullMagazine)
        {
            SetAmmoToMax();
        }
    }
}
