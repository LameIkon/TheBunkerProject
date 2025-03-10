using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Weapons/MeleeWeapon")]
public class MeleeWeaponSO : WeaponStats
{
    public MeleeWeaponType _MeleeWeaponCategory;

    public enum MeleeWeaponType
    {
        Unarmed,
        Knife,
        Chair
    }
}
