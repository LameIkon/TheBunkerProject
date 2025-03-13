public interface IWeaponUser
{
    string GetAttackerId();  // Get the unique ID for the attacker
    RangedWeaponType GetEquippedWeapon();  // Get the weapon type the player is using
}