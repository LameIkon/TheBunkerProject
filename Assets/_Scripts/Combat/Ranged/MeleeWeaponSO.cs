using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Weapons/MeleeWeapon")]
public class MeleeWeaponSO : WeaponStats
{
    public MeleeWeaponType _RangedWeaponCategory;

    public enum MeleeWeaponType
    {
        Knife,
        Chair,
        Unarmed
    }
}
