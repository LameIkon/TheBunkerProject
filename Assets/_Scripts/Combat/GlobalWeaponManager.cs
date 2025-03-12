using UnityEngine;

public class GlobalWeaponManager : MonoBehaviour
{
    [SerializeField] private SORangedWeapon[] _rangedWeapons; // For the game to know how many scriptable ranged weapons exist
    [SerializeField] private SOMeleeWeapon[] _meleeWeapons; // For the game to know how many scriptable melee weapons exist

    [SerializeField] private int _activeCoroutines = 0; // For debugging. checking how many entities have attacked and is on cooldown.

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

    private void Update()
    {
        //MeleeWeaponHandler.UpdateCooldowns(); // Handles attack cooldown for every instance of weapons... idk if this is performance friendly
    }

    public static void Attack(WeaponType currentweaponType, Transform attackpoint)
    {
        // Check what type of weapon you currently have
        if (WeaponTypes.TryGetMeleeType(currentweaponType, out MeleeWeaponType meleeWeapon)) // You use melee
        {
            MeleeWeaponHandler.PerformMeleeAttack(meleeWeapon, attackpoint); // use meleeWeapon and know its own position
        }
        else if (WeaponTypes.TryGetRangedType(currentweaponType, out RangedWeaponType rangedWeapon)) // You use range
        {
            RangedWeaponHandler.PerfomRangedAttack(rangedWeapon);
        }
    }


    public void StartMeleeWeaponCooldown(string attackerId, MeleeWeaponType meleeWeapon, float cooldownTime)
    {
        _activeCoroutines++; // For debugging
        StartCoroutine(MeleeWeaponHandler.HandleCooldown(attackerId, meleeWeapon, cooldownTime));  // Start the cooldown of specific weapon by specific user
    }

    public void CoroutineFinished() // For debugging
    {
        _activeCoroutines--;    
    }
}
