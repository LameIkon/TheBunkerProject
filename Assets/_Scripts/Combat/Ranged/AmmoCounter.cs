using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AmmoCounter : MonoBehaviour
{
    [SerializeField] private Image[] _currentAmmoSprite;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] public WeaponSO _weapon;

    private readonly Dictionary<GunType, int> _ammoSpriteIndexMap = new()
    {
        { GunType.Pistol, 0 },
        { GunType.Shotgun, 1 },
        { GunType.Rifle, 2 } 
    };


    private void Update()
    {
        SetAmmoCount(_weapon._CurrentAmmoCount);
        DisplayAmmoSprite();

        /*
         *      The if-statements with an Input.GetKey() method are placeholder logic to
         *      demonstrate the functions that reduces and replenishes ammunition.
         *
         *      TODO: MUST BE REMOVED OR REPLACED AT A LATER TIME!!
         */

        #region THESE IF-STATEMENTS MUST BE DELETED IN THE FUTURE

  
        if (Input.GetKeyDown(KeyCode.R))
        {
            _weapon.GainAmmo();
        }

        #endregion
    }

    private void SetAmmoCount(IntVariable ammoCount)
    {
        _weapon.UpdateAmmoCount();
        _ammoText.text = ammoCount.GetValue().ToString();
    }

    private void DisplayAmmoSprite()
    {
        if (_ammoSpriteIndexMap.TryGetValue(_weapon._WeaponCategory, out int ammoIndex))
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
            // Activate melee sprite here
        }
    }
}