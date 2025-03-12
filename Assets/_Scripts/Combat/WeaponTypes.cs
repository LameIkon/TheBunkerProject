using System.Collections.Generic;

/// <summary>
/// These Enums are designed to be uniq meaning only one Scriptable Object may use one type of enum. 
/// Having more Scriptable Objects use the same enum will likely cause bugs. expecially with cooldown of weapons, since each enum has its own cooldown
/// When creating a new enum it must be part of WeaponType enum and then to one of the following Melee- or RangedWeaponType enums and then give a connection
/// In the dictionaries below.
/// </summary>

// Global access to check what type of weapon exists. Used by SOMeleeWeapon and SORangedWeapon scriptables and WeaponSelector script
public enum MeleeWeaponType
{
    Unarmed,
    Knife,
    GhoulMelee
}

public enum RangedWeaponType
{
    Pistol,
    Shotgun,
    Rifle
}

// Combined together. Reason ive split it up above is for the scriptable objects make it easier to select the right. otherwise i need all together
public enum WeaponType
{
    Unarmed,
    Knife,
    GhoulMelee,
    Pistol,
    Rifle,
    Shotgun
}

public static class WeaponTypes // Connect WeaponType to the two other enums.
{
    private static readonly Dictionary<WeaponType, MeleeWeaponType> _meleeWeapontype = new() // Connect type to melee weapons
    {
        // Human Weapons
        {WeaponType.Unarmed, MeleeWeaponType.Unarmed },
        {WeaponType.Knife, MeleeWeaponType.Knife },

        // Monster Weapons
        {WeaponType.GhoulMelee, MeleeWeaponType.GhoulMelee }

    };

    private static readonly Dictionary<WeaponType, RangedWeaponType> _rangedWeapontype = new() // Connect type to ranged weapons
    {
        {WeaponType.Pistol, RangedWeaponType.Pistol },
        {WeaponType.Rifle, RangedWeaponType.Rifle },
        {WeaponType.Shotgun, RangedWeaponType.Shotgun }
    };

    //public static bool IsMeleeWeapon(WeaponType weaponType)
    //{
    //    return _meleeWeapontype.ContainsKey(weaponType);
    //}

    //public static bool IsRangedWeapon(WeaponType weaponType)
    //{
    //    return _rangedWeapontype.ContainsKey(weaponType);
    //}

    public static bool TryGetMeleeType(WeaponType weaponType, out MeleeWeaponType meleeType) // For other script to check if you have a melee weapon
    {
        return _meleeWeapontype.TryGetValue(weaponType, out meleeType);
    }

    public static bool TryGetRangedType(WeaponType weaponType, out RangedWeaponType rangedType) // For other script to check if you have a ranged weapon
    {
        return _rangedWeapontype.TryGetValue(weaponType, out rangedType);
    }
}


