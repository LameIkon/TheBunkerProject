using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun")]
public class WeaponSO : ScriptableObject
{
    public GunType _WeaponCategory;
    public GameObject _BulletPrefab;
    [Space(5f)]
    
    [Header("Stats")] 
    public float _Damage;
    public float _FireRate;
    public float _Range;
    public int _MaxAmmoCapacity;
    [Space(5f)] 
    
    [Header("Ammunition")]
    public IntReferencer _Magazine;
    public IntVariable _CurrentAmmoCount;
    [Space(5f)] 
    
    [Header("Critical Damage")]
    public int _CritChance;
    public float _CritDamage;

    private bool _fullMagazine;
    private bool _emptyMagazine;

    public void SetAmmoToMax()
    {
        _CurrentAmmoCount.ApplyChange(_MaxAmmoCapacity);
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

public enum GunType
{
    Pistol,
    Shotgun,
    Rifle
}