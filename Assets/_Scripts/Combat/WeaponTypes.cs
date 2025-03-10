// Global access to check what type of weapon exists. Used by SOMeleeWeapon and SORangedWeapon scriptables and WeaponSelector script
public enum MeleeWeaponType
{
    Unarmed,
    Knife,
    Chair
}

public enum RangedWeaponType
{
    Pistol,
    Shotgun,
    Rifle
}

// Combined together. Reason ive split it up is for the scriptable objects make it easier to select the right. otherwise i need all together
public enum WeaponType
{
    Unarmed,
    Knife,
    Pistol,
    Rifle,
    Shotgun
}