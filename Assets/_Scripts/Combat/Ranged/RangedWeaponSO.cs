using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeapon", menuName = "Weapons/RangedWeapon")]
public class RangedWeaponSO : WeaponStats
{
    public RangedWeaponType _RangedWeaponCategory;

    [Space(5f)] 
    [Header("Ammunition")]
    public GameObject _BulletPrefab;
    public int _MaxAmmoCapacity;
    public IntReferencer _Magazine;
    public IntVariable _CurrentAmmoCount;
    private bool _fullMagazine;
    private bool _emptyMagazine;

    public void SetAmmoToMax() //Used in Awake in Weapon.cs
    {
        _CurrentAmmoCount.SetValue(_MaxAmmoCapacity);
    }

    public void UpdateAmmoCount()
    {
        _Magazine.SetValue(_CurrentAmmoCount);
        CheckIfMagazineIsFullOrEmpty();
    }

    private void CheckIfMagazineIsFullOrEmpty()
    {
        _fullMagazine = (_Magazine.GetValue() == _MaxAmmoCapacity);
        _emptyMagazine = (_Magazine.GetValue() <= 0);
    }

    public void ReduceAmmoByShooting()
    {
        if (!_emptyMagazine)
        {
            _CurrentAmmoCount.ApplyChange(-1); 
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
