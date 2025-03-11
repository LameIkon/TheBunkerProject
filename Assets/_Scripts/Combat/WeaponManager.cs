using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private SORangedWeapon[] _rangedWeapons; // For the game to know how many scriptable ranged weapons exist
    [SerializeField] private SOMeleeWeapon[] _meleeWeapons; // For the game to know how many scriptable melee weapons exist

    private void Awake()
    {
        RangedWeaponHandler.Initialize(_rangedWeapons);
        MeleeWeaponHandler.Initialize(_meleeWeapons);
    }

    private void Update()
    {
        MeleeWeaponHandler.UpdateCooldowns(); // Handles attack rate for every instance of weapons... idk if this is performance friendly
    }
}
