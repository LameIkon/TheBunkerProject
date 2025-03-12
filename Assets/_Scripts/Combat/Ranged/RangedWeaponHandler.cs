using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RangedWeaponHandler
{
    private static Dictionary<string, Dictionary<RangedWeaponType, float>> _weaponCooldowns = new Dictionary<string, Dictionary<RangedWeaponType, float>>();  // For weapon cooldown. The dictionary holds a string and then another dictionary. First it looks for an entity and this entity(like player) can hold multiple weapons with their cooldowns 
    private static Dictionary<RangedWeaponType, SORangedWeapon> _rangedWeaponData; // Access to scriptableObject from the given enum
    private static Dictionary<string, HashSet<RangedWeaponType>> _activeCooldownCoroutines = new Dictionary<string, HashSet<RangedWeaponType>>(); // Track active coroutines


    public static void Initialize(SORangedWeapon[] allRangedWeaponTypes) // Check what type of weapons exist
    {
        _rangedWeaponData = new Dictionary<RangedWeaponType, SORangedWeapon>();

        foreach (SORangedWeapon rangedWeapon in allRangedWeaponTypes)
        {
            _rangedWeaponData[rangedWeapon.SO_RangedWeapontype] = rangedWeapon;
        }
    }


    public static void PerfomRangedAttack(RangedWeaponType rangedWeapon, Transform attacker)
    {
         string attackerId = attacker.GetInstanceID().ToString(); // Uniq id to the instance of the one perfoming the attack
        Debug.Log("unique attacker id: " + attackerId);
        if (!_weaponCooldowns.ContainsKey(attackerId)) // If there is no such id then save it
        {
            _weaponCooldowns[attackerId] = new Dictionary<RangedWeaponType, float>();
        }

        if (_weaponCooldowns[attackerId].ContainsKey(rangedWeapon) && _weaponCooldowns[attackerId][rangedWeapon] > 0f) // Check if entity has a cooldown for that weapon and if it still is on cooldown
        {
            return; // If weapon is still on cooldown return
        }

        if (!_activeCooldownCoroutines.ContainsKey(attackerId)) // Check if a coroutine is already running for this attacker and weapon type
        {
            _activeCooldownCoroutines[attackerId] = new HashSet<RangedWeaponType>(); // Add to hashset over active coroutines
        }

        if (_activeCooldownCoroutines[attackerId].Contains(rangedWeapon)) // Stop if there is an coroutine with that type running
        {
            return; // Skip starting the coroutine if it's already active
        }

        if (_rangedWeaponData.TryGetValue(rangedWeapon, out SORangedWeapon weaponData)) // Get the specific weapontype scriptable 
        {
            Debug.Log("attacked using: " + weaponData.SO_RangedWeapontype);
            weaponData.PerformAttack(attacker); // Access scriptable object method
            //_weaponCooldowns[attackerId][meleeWeapon] = weaponData.SO_AttackRate; // Set the weapon on cooldown
        }

        _activeCooldownCoroutines[attackerId].Add(rangedWeapon); // add coroutine to hashset
        GlobalWeaponManager.Instance.StartRangedWeaponCooldown(attackerId, rangedWeapon, weaponData.SO_AttackRate); // Starts the cooldown for the weapon. Coroutine needs to be called from not an abstract class
    }

    public static IEnumerator HandleCooldown(string attackerId, RangedWeaponType rangedWeapon, float cooldownTime)
    {
        if (!_weaponCooldowns.ContainsKey(attackerId)) // If attacker doesn't exist in the cooldown dictionary, add it
        {
            Debug.Log(attackerId);
            _weaponCooldowns[attackerId] = new Dictionary<RangedWeaponType, float>();
        }

        Dictionary<RangedWeaponType, float> attackerCooldowns = _weaponCooldowns[attackerId];
        attackerCooldowns[rangedWeapon] = cooldownTime; // Set the cooldown time for the weapon

        // Wait until cooldown finishes
        while (attackerCooldowns[rangedWeapon] > 0f)
        {
            attackerCooldowns[rangedWeapon] -= Time.deltaTime; // Reduce cooldown time
            yield return null;
        }
        GlobalWeaponManager.Instance.CoroutineFinished(); // For debugging
        attackerCooldowns.Remove(rangedWeapon); // Once cooldown is done, remove the weapon from the cooldown list
        _activeCooldownCoroutines[attackerId].Remove(rangedWeapon); // Remove the coroutine from the list
    }
}
