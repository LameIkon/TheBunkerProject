using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmoDisplay : MonoBehaviour
{
    [SerializeField] private Image[] _currentAmmoSprite;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private string _playerId;

    private WeaponType _currentWeaponType = WeaponType.Unarmed;

    private readonly Dictionary<WeaponType, int> _ammoSpriteIndexMap = new()
    {
        { WeaponType.Pistol, 0 },
        { WeaponType.Rifle, 1 },
        { WeaponType.Shotgun, 2 }
    };


    private void OnEnable()
    {
        WeaponSelectionHandler.s_OnWeaponChanged += WeaponChecker;
    }

    private void OnDisable()
    {
        WeaponSelectionHandler.s_OnWeaponChanged -= WeaponChecker;
    }

    private void Start()
    {
        ChangeAmmoSprite();
    }

    private void WeaponChecker(WeaponType weapon)
    {
        _currentWeaponType = weapon;
        ChangeAmmoSprite();
    }

    private void Update()
    {
        //SetAmmoCount(_currentWeapon._weapon.SO_CurrentAmmoCount);
        //DisplayAmmoSprite();
        //WeaponSelection();
    }

    private void ChangeAmmoSprite()
    {
        if (_ammoSpriteIndexMap.TryGetValue(_currentWeaponType, out int spriteIndex))
        {
            foreach (Image sprite in _currentAmmoSprite) // Disable all sprites
            {
                sprite.enabled = false;
            }
            _currentAmmoSprite[spriteIndex].enabled = true; // Enable the selected
            _ammoText.text = SetAmmoCount();
        }
        else
        {
            foreach (Image sprite in _currentAmmoSprite) // Disable all sprites
            {
                sprite.enabled = false; // Dont show any UI
                _ammoText.text = ""; // Dont display anything
            }
        }
    }

    private string SetAmmoCount()
    {
        AmmunitionHandler ammoHandler = RangedWeaponHandler.GetAmmunitionHandler(_playerId, _currentWeaponType); // Get the player's ammunition details

        string currentAmmo = ammoHandler.DisplayCurrentAmmo().ToString();
        string totalAmmo = ammoHandler.DisplayTotalAmmo().ToString();

        string ammoUIDisplay = $"{currentAmmo} / {totalAmmo}"; // What will be displayed 

        return ammoUIDisplay;
    }

    private void DisplayAmmoSprite()
    {
        //if (_ammoSpriteIndexMap.TryGetValue(_currentWeapon._weapon._RangedWeaponCategory, out int ammoIndex))
        //{
        //    for (int i = 0; i < _currentAmmoSprite.Length; i++)
        //    {
        //        _currentAmmoSprite[i].gameObject.SetActive(i == ammoIndex);
        //    }
        //}

        //else // Default case
        //{
        //    foreach (var sprite in _currentAmmoSprite)
        //    {
        //        sprite.gameObject.SetActive(false);
        //    }           
        //}
    }

    private void WeaponSelection()
    {
        //for (int i = 0; i < _Weapons.Length; i++)
        //{
        //    if (_Weapons[i]._weapon._RangedWeaponCategory == RangedWeaponType.Knife && WeaponSelector._IsKnife)
        //    {
        //        _currentWeapon = _Weapons[i];
        //    }

        //    else if (_Weapons[i]._weapon._RangedWeaponCategory == RangedWeaponType.Pistol && WeaponSelector._IsPistol)
        //    {
        //        _currentWeapon = _Weapons[i];
        //    }

        //    else if (_Weapons[i]._weapon._RangedWeaponCategory == RangedWeaponType.Rifle && WeaponSelector._IsRifle)
        //    {
        //        _currentWeapon = _Weapons[i];
        //    }

        //    else if (_Weapons[i]._weapon._RangedWeaponCategory == RangedWeaponType.Shotgun && WeaponSelector._IsShotgun)
        //    {
        //        _currentWeapon = _Weapons[i];
        //    }
        //}
    }
}