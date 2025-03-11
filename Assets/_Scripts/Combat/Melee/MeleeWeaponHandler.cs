using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MeleeWeaponHandler
{
    public static Dictionary<string, Dictionary<MeleeWeaponType, float>> _weaponCooldowns = new Dictionary<string, Dictionary<MeleeWeaponType, float>>();  // For weapon cooldown. The dictionary holds a string and then another dictionary. First it looks for an entity and this entity(like player) can hold multiple weapons with their cooldowns 
    private static Dictionary<MeleeWeaponType, SOMeleeWeapon> _meleeWeaponData; // Access to scriptableObject from the given enum


    public static void Initialize(SOMeleeWeapon[] allMeleeWeaponTypes) //Populate dictionary with all melee weapons in an awake method
    {
        _meleeWeaponData = new Dictionary<MeleeWeaponType, SOMeleeWeapon>();

        foreach (SOMeleeWeapon meleeWeapon in allMeleeWeaponTypes) // Foreach ScriptableObject in array
        {
            _meleeWeaponData[meleeWeapon.SO_MeleeWeaponType] = meleeWeapon; // Assign to dictionary the ScriptableObject to the corresponding enum
        }
    }


    public static void PerformMeleeAttack(MeleeWeaponType meleeWeapon, Transform attacker)
    {
        string attackerId = attacker.GetInstanceID().ToString(); // Uniq id to the instance of the one perfoming the attack
        if (!_weaponCooldowns.ContainsKey(attackerId)) // If there is no such id then save it
        {
            _weaponCooldowns[attackerId] = new Dictionary<MeleeWeaponType, float>();
        }

        if (_weaponCooldowns[attackerId].ContainsKey(meleeWeapon) && _weaponCooldowns[attackerId][meleeWeapon] > 0f) // Check if entity has a cooldown for that weapon and if it still is on cooldown
        {
            return; // If weapon is still on cooldown, return early
        }

        if (_meleeWeaponData.TryGetValue(meleeWeapon, out SOMeleeWeapon weaponData)) // Get the specific weapontype scriptable 
        {
            Debug.Log("attacked using: " + weaponData.SO_MeleeWeaponType);
            weaponData.PerfomAttack(attacker); // Access scriptable object method
            //_weaponCooldowns[attackerId][meleeWeapon] = weaponData.SO_AttackRate; // Set the weapon on cooldown
            WeaponManager.Instance.StartMeleeWeaponCooldownCoroutine(attackerId, meleeWeapon, weaponData.SO_AttackRate); // Starts the cooldown for the weapon
        }
    }

    //public static void UpdateCooldowns() // WeaponManager handles cooldown of all weapons
    //{
    //    foreach (KeyValuePair<string, Dictionary<MeleeWeaponType, float>> attackerCooldowns in _weaponCooldowns) // Iterate through each attacker 
    //    {
    //        string attackerId = attackerCooldowns.Key;

    //        List<MeleeWeaponType> weaponsToRemove = new List<MeleeWeaponType>(); //List for which weapons have finished cooldown

    //        foreach (KeyValuePair<MeleeWeaponType, float> pair in attackerCooldowns.Value.ToList()) // Look through each weapon and cooldown timer
    //        {
    //            attackerCooldowns.Value[pair.Key] -= Time.deltaTime; // Reduce the cooldown time. Reduces the float value

    //            if (attackerCooldowns.Value[pair.Key] <= 0f) // If cooldown is finished, remove it from the dictionary
    //            {
    //                weaponsToRemove.Add(pair.Key);
    //            }
    //        }

    //        foreach (MeleeWeaponType weapon in weaponsToRemove) // Clean up the cooldown dictionary
    //        {
    //            attackerCooldowns.Value.Remove(weapon);
    //        }
    //    }  
    //}

    public static IEnumerator HandleCooldown(string attackerId, MeleeWeaponType meleeWeapon, float cooldownTime)
    {
        if (!_weaponCooldowns.ContainsKey(attackerId)) // If attacker doesn't exist in the cooldown dictionary, add it
        {
            _weaponCooldowns[attackerId] = new Dictionary<MeleeWeaponType, float>();
        }

        Dictionary<MeleeWeaponType, float> attackerCooldowns = _weaponCooldowns[attackerId];
        attackerCooldowns[meleeWeapon] = cooldownTime; // Set the cooldown time for the weapon

        // Wait until cooldown finishes
        while (attackerCooldowns[meleeWeapon] > 0f)
        {
            attackerCooldowns[meleeWeapon] -= Time.deltaTime; // Reduce cooldown time
            yield return null;
        }     
        attackerCooldowns.Remove(meleeWeapon); // Once cooldown is done, remove the weapon from the cooldown list
        WeaponManager.Instance.CoroutineFinished();
    }

}
