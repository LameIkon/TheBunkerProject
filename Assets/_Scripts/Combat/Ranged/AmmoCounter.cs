using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] private Image[] _currentAmmoSprite;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private Weapon[] _Weapons;

    private readonly Dictionary<RangedWeaponType, int> _ammoSpriteIndexMap = new()
    {
        { RangedWeaponType.Pistol, 0 },
        { RangedWeaponType.Rifle, 1 },
        { RangedWeaponType.Shotgun, 2 },
        { RangedWeaponType.Knife, 3 }
    };

    private void Update()
    {
        SetAmmoCount(_currentWeapon._weapon._CurrentAmmoCount);
        DisplayAmmoSprite();
        WeaponSelection();
    }

    private void SetAmmoCount(IntVariable ammoCount)
    {
        _currentWeapon._weapon.UpdateAmmoCount();

        if(_currentWeapon._weapon._RangedWeaponCategory != RangedWeaponType.Knife)
        {
            _ammoText.text = ammoCount.GetValue().ToString();
        }

        else if (_currentWeapon._weapon._RangedWeaponCategory == RangedWeaponType.Knife)
        {
            _ammoText.text = "∞";
        }        
    }

    private void DisplayAmmoSprite()
    {
        if (_ammoSpriteIndexMap.TryGetValue(_currentWeapon._weapon._RangedWeaponCategory, out int ammoIndex))
        {
            for (int i = 0; i < _currentAmmoSprite.Length; i++)
            {
                _currentAmmoSprite[i].gameObject.SetActive(i == ammoIndex);
            }
        }

        else // Default case
        {
            foreach (var sprite in _currentAmmoSprite)
            {
                sprite.gameObject.SetActive(false);
            }           
        }
    }

    private void WeaponSelection()
    {
        for (int i = 0; i < _Weapons.Length; i++)
        {
            if (_Weapons[i]._weapon._RangedWeaponCategory == RangedWeaponType.Knife && CurrentWeapon._IsKnife)
            {
                _currentWeapon = _Weapons[i];
            }

            else if (_Weapons[i]._weapon._RangedWeaponCategory == RangedWeaponType.Pistol && CurrentWeapon._IsPistol)
            {
                _currentWeapon = _Weapons[i];
            }

            else if (_Weapons[i]._weapon._RangedWeaponCategory == RangedWeaponType.Rifle && CurrentWeapon._IsRifle)
            {
                _currentWeapon = _Weapons[i];
            }

            else if (_Weapons[i]._weapon._RangedWeaponCategory == RangedWeaponType.Shotgun && CurrentWeapon._IsShotgun)
            {
                _currentWeapon = _Weapons[i];
            }
        }
    }
}