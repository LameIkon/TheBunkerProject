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
        RangedWeaponHandler.OnAmmoChanged += SetAmmoCount;
    }

    private void OnDisable()
    {
        WeaponSelectionHandler.s_OnWeaponChanged -= WeaponChecker;
        RangedWeaponHandler.OnAmmoChanged -= SetAmmoCount;
    }

    private void Start()
    {
        _playerId = PlayerController.s_PlayerId;
        ChangeAmmoSprite();
    }

    private void WeaponChecker(WeaponType weapon)
    {
        _currentWeaponType = weapon;
        ChangeAmmoSprite();
        if (WeaponTypes.TryGetRangedType(weapon, out RangedWeaponType rangedWeaponType))
        {
            Debug.Log("called" + _playerId + "is the player id");
            RangedWeaponHandler.UpdateAmmoForPlayer(_playerId, rangedWeaponType);
        }
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
            //_ammoText.text = SetAmmoCount();
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

    private void SetAmmoCount(int currentAmmo, int totalAmmo)
    {
        string ammoUIDisplay = $"{currentAmmo} / {totalAmmo}";
        _ammoText.text = ammoUIDisplay;
    }

    public void UpdateAmmoUIAfterReload(int currentAmmo, int totalAmmo)
    {
        SetAmmoCount(currentAmmo, totalAmmo);
    }
}