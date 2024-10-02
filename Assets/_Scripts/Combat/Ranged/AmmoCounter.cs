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

    private readonly Dictionary<GunType, int> _ammoSpriteIndexMap = new()
    {
        { GunType.Pistol, 0 },
        { GunType.Rifle, 1 },
        { GunType.Shotgun, 2 },
        { GunType.Knife, 3 }
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

        if(_currentWeapon._weapon._WeaponCategory != GunType.Knife)
        {
            _ammoText.text = ammoCount.GetValue().ToString();
        }

        else if (_currentWeapon._weapon._WeaponCategory == GunType.Knife)
        {
            _ammoText.text = "∞";
        }        
    }

    private void DisplayAmmoSprite()
    {
        if (_ammoSpriteIndexMap.TryGetValue(_currentWeapon._weapon._WeaponCategory, out int ammoIndex))
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
            if (_Weapons[i]._weapon._WeaponCategory == GunType.Knife && CurrentWeapon._IsKnife)
            {
                _currentWeapon = _Weapons[i];
            }

            else if (_Weapons[i]._weapon._WeaponCategory == GunType.Pistol && CurrentWeapon._IsPistol)
            {
                _currentWeapon = _Weapons[i];
            }

            else if (_Weapons[i]._weapon._WeaponCategory == GunType.Rifle && CurrentWeapon._IsRifle)
            {
                _currentWeapon = _Weapons[i];
            }

            else if (_Weapons[i]._weapon._WeaponCategory == GunType.Shotgun && CurrentWeapon._IsShotgun)
            {
                _currentWeapon = _Weapons[i];
            }
        }
    }
}