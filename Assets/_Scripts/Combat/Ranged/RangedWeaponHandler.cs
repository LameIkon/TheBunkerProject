using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class RangedWeaponHandler
{
    private static Dictionary<string, Dictionary<RangedWeaponType, AmmunitionHandler>> _weaponAmmunition = new Dictionary<string, Dictionary<RangedWeaponType, AmmunitionHandler>>(); // Entity has a weapon and that weapon has ammo 
    private static Dictionary<RangedWeaponType, SORangedWeapon> _rangedWeaponData; // Access to scriptableObject from the given enum
    private static Dictionary<string, Dictionary<RangedWeaponType, float>> _weaponCooldowns = new Dictionary<string, Dictionary<RangedWeaponType, float>>();  // For weapon cooldown. The dictionary holds a string and then another dictionary. First it looks for an entity and this entity(like player) can hold multiple weapons with their cooldowns 
    private static Dictionary<string, HashSet<RangedWeaponType>> _activeCooldownCoroutines = new Dictionary<string, HashSet<RangedWeaponType>>(); // Track active coroutines


    public static void Initialize(SORangedWeapon[] allRangedWeaponTypes) // Check what type of weapons exist
    {
        _rangedWeaponData = new Dictionary<RangedWeaponType, SORangedWeapon>();

        foreach (SORangedWeapon rangedWeapon in allRangedWeaponTypes)
        {
            _rangedWeaponData[rangedWeapon.SO_RangedWeapontype] = rangedWeapon;
        }
    }


    public static void PerfomRangedAttack(RangedWeaponType rangedWeapon, Transform attackerPosition, string attackerId)
    {
        Debug.Log("unique attacker id: " + attackerId);

        EnsureWeaponDataExists(attackerId, rangedWeapon);

        if (_weaponCooldowns[attackerId].ContainsKey(rangedWeapon) && _weaponCooldowns[attackerId][rangedWeapon] > 0f) // Check if entity has a cooldown for that weapon and if it still is on cooldown
        {
            return; // If weapon is still on cooldown return
        }

        if (_activeCooldownCoroutines[attackerId].Contains(rangedWeapon)) // Stop if there is an coroutine with that type running
        {
            return; // Skip starting the coroutine if it's already active
        }

        //CheckWeaponAmmunition(rangedWeapon, attackerPosition, attackerId);
        
        AmmunitionHandler weaponAmmo = _weaponAmmunition[attackerId][rangedWeapon];
        if (!weaponAmmo.HasAmmo()) // If you dont have ammunition
        {
            Debug.Log("need reload");
            //ReloadWeapon(rangedWeapon, attackerPosition);
            return; // need reload
        }

        if (_rangedWeaponData.TryGetValue(rangedWeapon, out SORangedWeapon weaponData)) // Get the specific weapontype scriptable 
        {
            Debug.Log("attacked using: " + weaponData.SO_RangedWeapontype);
            weaponAmmo.ConsumeAmmo(); // Use ammunition
            weaponData.PerformAttack(attackerPosition); // Access scriptable object method
        }
        UpdatePlayerUI(rangedWeapon, attackerId); // Check if its the player 
        _activeCooldownCoroutines[attackerId].Add(rangedWeapon); // add coroutine to hashset
        GlobalWeaponManager.Instance.StartRangedWeaponCooldown(attackerId, rangedWeapon, weaponData.SO_AttackRate); // Starts the cooldown for the weapon. Coroutine needs to be called from not an abstract class
    }

    //private static void CheckWeaponAmmunition(RangedWeaponType rangedWeapon, Transform attacker, string attackerId)
    //{
    //    if (!_weaponAmmunition.ContainsKey(attackerId)) // If there is no such id then save it
    //    {
    //        _weaponAmmunition[attackerId] = new Dictionary<RangedWeaponType, AmmunitionHandler>();
    //    }
    //    if (!_weaponAmmunition[attackerId].ContainsKey(rangedWeapon))
    //    {
    //        if (_rangedWeaponData.TryGetValue(rangedWeapon, out SORangedWeapon weaponData))
    //        {
    //            _weaponAmmunition[attackerId][rangedWeapon] = new AmmunitionHandler(weaponData);
    //        }
    //    }

    //    UpdatePlayerUI(rangedWeapon, attackerId);
    //}

    public static void ReloadWeapon(RangedWeaponType rangedWeapon, string attackerId)
    {
        if (!_weaponAmmunition.ContainsKey(attackerId)) // If there is no such id then save it
        {
            _weaponAmmunition[attackerId] = new Dictionary<RangedWeaponType, AmmunitionHandler>();
        }
        if (_weaponAmmunition.ContainsKey(attackerId) && _weaponAmmunition[attackerId].ContainsKey(rangedWeapon))
        {
            AmmunitionHandler ammoHandler = _weaponAmmunition[attackerId][rangedWeapon];
            ammoHandler.ReloadWeapon(attackerId); // Reload weapon
        }
    }

    public static AmmunitionHandler GetAmmunitionHandler(string attackerId, WeaponType weapon)
    {
        if (WeaponTypes.TryGetRangedType(weapon, out RangedWeaponType rangedWeapon))
        {        
            if (!_weaponAmmunition.ContainsKey(attackerId)) // If there is no such id then save it
            {
                _weaponAmmunition[attackerId] = new Dictionary<RangedWeaponType, AmmunitionHandler>();
            }       
            if (!_weaponAmmunition[attackerId].ContainsKey(rangedWeapon))  // Ensure the ammunition handler exists for the weapon
            {
                if (_rangedWeaponData.TryGetValue(rangedWeapon, out SORangedWeapon weaponData))
                {
                    _weaponAmmunition[attackerId][rangedWeapon] = new AmmunitionHandler(weaponData);
                }
            }

            return _weaponAmmunition[attackerId][rangedWeapon]; // Return the found or newly created ammo handler
        }
        return null;
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
        GlobalWeaponManager.Instance.CooldownCoroutineFinished(); // For debugging
        attackerCooldowns.Remove(rangedWeapon); // Once cooldown is done, remove the weapon from the cooldown list
        _activeCooldownCoroutines[attackerId].Remove(rangedWeapon); // Remove the coroutine from the list
    }


    public static event Action<int, int> OnAmmoChanged;
    public static void UpdatePlayerUI(RangedWeaponType rangedWeapon, string attackerId)
    {
        Debug.Log("checking");
        if(attackerId == PlayerController.s_PlayerId) // Check if the id is identical to player id
        {
            EnsureWeaponDataExists(attackerId, rangedWeapon);
            Debug.Log("Is player!");
            if (_weaponAmmunition[attackerId].TryGetValue(rangedWeapon, out AmmunitionHandler ammoHandler))
            {
                int currentAmmo = ammoHandler.DisplayCurrentAmmo();
                int totalAmmo = ammoHandler.DisplayTotalAmmo();

                Debug.Log($"RangedManager | Current ammo: {currentAmmo} | total Ammo: {totalAmmo}");
                
                OnAmmoChanged?.Invoke(currentAmmo, totalAmmo);
            }         
        }
        else
        {
            Debug.Log($"This is not the player: {attackerId} | player id is: {PlayerController.s_PlayerId}");
        }
    }

    public static void UpdateAmmoForPlayer(string playerId, RangedWeaponType weaponType)
    {
        UpdatePlayerUI(weaponType, playerId);
    }


    private static void EnsureWeaponDataExists(string attackerId, RangedWeaponType rangedWeapon)
    {
        // Ensure ammunition data exists for the attacker
        if (!_weaponAmmunition.ContainsKey(attackerId))
        {
            _weaponAmmunition[attackerId] = new Dictionary<RangedWeaponType, AmmunitionHandler>();
        }

        // Ensure cooldown data exists for the attacker
        if (!_weaponCooldowns.ContainsKey(attackerId))
        {
            _weaponCooldowns[attackerId] = new Dictionary<RangedWeaponType, float>();
        }

        // Ensure active cooldown coroutines exist for the attacker
        if (!_activeCooldownCoroutines.ContainsKey(attackerId))
        {
            _activeCooldownCoroutines[attackerId] = new HashSet<RangedWeaponType>();
        }

        // Ensure ammo handler exists for the given weapon
        if (!_weaponAmmunition[attackerId].ContainsKey(rangedWeapon))
        {
            if (_rangedWeaponData.TryGetValue(rangedWeapon, out SORangedWeapon weaponData))
            {
                _weaponAmmunition[attackerId][rangedWeapon] = new AmmunitionHandler(weaponData);
            }
        }
    }



}
