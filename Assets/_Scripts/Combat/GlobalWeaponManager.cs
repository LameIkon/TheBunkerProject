using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalWeaponManager : MonoBehaviour
{
    [SerializeField] private SORangedWeapon[] _rangedWeapons; // For the game to know how many scriptable ranged weapons exist
    [SerializeField] private SOMeleeWeapon[] _meleeWeapons; // For the game to know how many scriptable melee weapons exist

    [SerializeField] private int _activeWeaponCooldownCoroutines = 0; // For debugging. checking how many entities have attacked and is on cooldown.
    [SerializeField] private int _activeWeaponReloadCoroutines = 0; // For debugging. checking how many entities have attacked and is on cooldown.

    public static GlobalWeaponManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance of WeaponManager exists
        }

        RangedWeaponHandler.Initialize(_rangedWeapons);
        MeleeWeaponHandler.Initialize(_meleeWeapons);
    }

    public static void Attack(WeaponType currentweaponType, Transform attackpoint, string attackerId)
    {
        // Check what type of weapon you currently have
        if (WeaponTypes.TryGetMeleeType(currentweaponType, out MeleeWeaponType meleeWeapon)) // You use melee
        {
            MeleeWeaponHandler.PerformMeleeAttack(meleeWeapon, attackpoint, attackerId); // use meleeWeapon and know its own position
        }
        else if (WeaponTypes.TryGetRangedType(currentweaponType, out RangedWeaponType rangedWeapon)) // You use range
        {
            RangedWeaponHandler.PerfomRangedAttack(rangedWeapon, attackpoint, attackerId);
        }
    }

    public void ReloadWeapon(WeaponType currentWeaponType, string userId)
    {
        //string attackerId = _attackPoint.GetInstanceID().ToString(); // Unique attacker ID from the transform
        if (WeaponTypes.TryGetRangedType(currentWeaponType, out RangedWeaponType rangedWeapon))
        {
            RangedWeaponHandler.ReloadWeapon(rangedWeapon, userId); // Reload the weapon based on the current weapon type
        }
    }


    public void StartMeleeWeaponCooldown(string attackerId, MeleeWeaponType meleeWeapon, float cooldownTime) // Start the cooldown of specific weapon by specific user
    {
        _activeWeaponCooldownCoroutines++; // For debugging
        StartCoroutine(MeleeWeaponHandler.HandleCooldown(attackerId, meleeWeapon, cooldownTime));
    }

    public void StartRangedWeaponCooldown(string attackerId, RangedWeaponType rangedWeapon, float cooldownTime) // Start the cooldown of specific weapon by specific user
    {
        _activeWeaponCooldownCoroutines++; // For debugging
        StartCoroutine(RangedWeaponHandler.HandleCooldown(attackerId, rangedWeapon, cooldownTime));  
    }

    public void StartReloadingWeapon(AmmunitionHandler ammoHandler, float reloadTime) // Start the reload for a specific weapon
    {
        _activeWeaponReloadCoroutines++; // For debugging
        StartCoroutine(ammoHandler.ReloadCoroutine(reloadTime));
    }

    public void CooldownCoroutineFinished() // For debugging
    {
        _activeWeaponCooldownCoroutines--;    
    }

    public void ReloadCoroutineFinished() // For debugging
    {
        _activeWeaponReloadCoroutines--;
    }


    private int _playercCurrentAmmo;
    private int _playerTotalAmmo;

    public void DisplayPlayerAmmo(int currentAmmoData, int totalAmmoData) // Not good that every entity calls this after shooting but cant figure out something else
    {
        _playercCurrentAmmo = currentAmmoData;
        _playerTotalAmmo = totalAmmoData;
        string ammoUIDisplay = $"{_playercCurrentAmmo} / {_playerTotalAmmo}";
    }

    public int GetCurrentAmmo()
    {
        // Return the current ammo for the player
        return _playercCurrentAmmo;
    }

    public int GetTotalAmmo()
    {
        // Return the total ammo for the player
        return _playerTotalAmmo;
    }


}
