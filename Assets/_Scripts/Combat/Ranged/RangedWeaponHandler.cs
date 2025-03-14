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
    
    private static Dictionary<string, HashSet<RangedWeaponType>> _activeCooldownCoroutines = new Dictionary<string, HashSet<RangedWeaponType>>(); // Track active coroutines for each player and their weapons
    private static Dictionary<string, HashSet<RangedWeaponType>> _reloadingWeapons = new Dictionary<string, HashSet<RangedWeaponType>>(); // Track active reloads for each player and their weapons

    public static event Action<int, int> OnAmmoChanged;

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

        EnsureWeaponDataExists(attackerId, rangedWeapon); // Create and store data to the dictionaries. Safeguard

        if (_reloadingWeapons.ContainsKey(attackerId) && _reloadingWeapons[attackerId].Contains(rangedWeapon)) // Check if entity is currently reloading for that weapon
        {
            return;
        }

        if (_weaponCooldowns[attackerId].ContainsKey(rangedWeapon) && _weaponCooldowns[attackerId][rangedWeapon] > 0f) // Check if entity has a cooldown for that weapon and if it still is on cooldown
        {
            return; // If weapon is still on cooldown return
        }

        if (_activeCooldownCoroutines[attackerId].Contains(rangedWeapon)) // Stop if there is an coroutine with that type running
        {
            return; // Return since you are still on cooldown
        }
        
        AmmunitionHandler weaponAmmo = _weaponAmmunition[attackerId][rangedWeapon]; // Take the entity and their weapon to be handled by ammunitionHandler
        if (!weaponAmmo.HasAmmo()) // If entity with that weapon has no ammo
        {
            Debug.Log("need reload");
            return; // Return since you dont have ammo
        }

        // All checkers/safeguards have been accepted and now you can begin your attack
        if (_rangedWeaponData.TryGetValue(rangedWeapon, out SORangedWeapon weaponData)) // Get the specific weapontype ScriptableObject 
        {
            Debug.Log("attacking using: " + weaponData.SO_RangedWeapontype); 
            weaponAmmo.ConsumeAmmo(); // Use ammunition
            weaponData.PerformAttack(attackerPosition); // Access scriptable object method to perform an attack
        }
        UpdatePlayerUI(rangedWeapon, attackerId); // Check if its the player to update UI
        _activeCooldownCoroutines[attackerId].Add(rangedWeapon); // add coroutine to hashset for cooldown
        GlobalWeaponManager.Instance.StartRangedWeaponCooldown(attackerId, rangedWeapon, weaponData.SO_AttackRate); // Starts the cooldown for the weapon. Coroutine needs to be called from an Monobehaviour
    }

    public static void ReloadWeapon(RangedWeaponType rangedWeapon, string attackerId)
    {
        EnsureWeaponDataExists(attackerId, rangedWeapon); // Safeguard

        if (_reloadingWeapons.ContainsKey(attackerId) && _reloadingWeapons[attackerId].Contains(rangedWeapon))
        {
            return; // Stop if already reloading
        }
        if (!_reloadingWeapons.ContainsKey(attackerId))
        {
            _reloadingWeapons[attackerId] = new HashSet<RangedWeaponType>();
        }

        AmmunitionHandler ammoHandler = _weaponAmmunition[attackerId][rangedWeapon]; // Get the ammo data

        //if (ammoHandler.CheckIfHaveMaxammo())
        //{
        //    Debug.Log("max magazine");
        //    return; // Stop if you have max ammo in magazine
        //}

        _reloadingWeapons[attackerId].Add(rangedWeapon);  
        ammoHandler.ReloadWeapon(attackerId); // Reload weapon      
    }

    public static void RemoveWeaponFromReloading(string attackerId, RangedWeaponType rangedWeapon)
    {
        if (_reloadingWeapons.ContainsKey(attackerId))
        {
            //Debug.Log(_reloadingWeapons[attackerId].Remove(rangedWeapon));
            _reloadingWeapons[attackerId].Remove(rangedWeapon);
            //Debug.Log(_reloadingWeapons[attackerId].Remove(rangedWeapon));
            //_reloadingWeapons[attackerId].Clear(); // not good for scalability
        }
    }

    public static AmmunitionHandler GetAmmunitionHandler(string attackerId, WeaponType weapon) // Get ammunitonHandler for each entity and for each of their weapons. When an entity needs their ammo refilled
    {
        if (WeaponTypes.TryGetRangedType(weapon, out RangedWeaponType rangedWeapon)) // Check if their weapon is a ranged weapon
        {        
            if (!_weaponAmmunition.ContainsKey(attackerId)) // If there is no such id then save it
            {
                _weaponAmmunition[attackerId] = new Dictionary<RangedWeaponType, AmmunitionHandler>();
            }       
            if (!_weaponAmmunition[attackerId].ContainsKey(rangedWeapon))  // If entity dont have a weapon stored then:
            {
                if (_rangedWeaponData.TryGetValue(rangedWeapon, out SORangedWeapon weaponData)) // Get the weapontype from ScriptableObject
                {
                    _weaponAmmunition[attackerId][rangedWeapon] = new AmmunitionHandler(weaponData); // Store the weapon 
                }
            }

            return _weaponAmmunition[attackerId][rangedWeapon]; // Return entity and their weapon. You are now able to access the AmmunitionHandler Instance for that entity and get its ammo
        }
        return null;
    }


    public static IEnumerator HandleCooldown(string attackerId, RangedWeaponType rangedWeapon, float cooldownTime)
    {
        EnsureWeaponDataExists(attackerId, rangedWeapon); // Create and store data to the dictionaries. Safeguard

        _weaponCooldowns[attackerId][rangedWeapon] = cooldownTime; // Set the cooldown time for the weapon

        // Wait until cooldown finishes
        while (_weaponCooldowns[attackerId][rangedWeapon] > 0f)
        {
            _weaponCooldowns[attackerId][rangedWeapon] -= Time.deltaTime; // Reduce cooldown time
            yield return null;
        }
        GlobalWeaponManager.Instance.CooldownCoroutineFinished(); // For debugging. 
        _weaponCooldowns[attackerId].Remove(rangedWeapon); // Once cooldown is done, remove the weapon from the cooldown list
        _activeCooldownCoroutines[attackerId].Remove(rangedWeapon); // Remove the coroutine from the list
    }


    public static void UpdatePlayerUI(RangedWeaponType rangedWeapon, string attackerId) // Only for player
    {
        if(attackerId == PlayerController.s_PlayerId) // Check if the id is identical to player id
        {
            EnsureWeaponDataExists(attackerId, rangedWeapon);
            if (_weaponAmmunition[attackerId].TryGetValue(rangedWeapon, out AmmunitionHandler ammoHandler))
            {
                int currentAmmo = ammoHandler.DisplayCurrentAmmo();
                int totalAmmo = ammoHandler.DisplayTotalAmmo();

                Debug.Log($"Current ammo: {currentAmmo} | total Ammo: {totalAmmo}");
                
                OnAmmoChanged?.Invoke(currentAmmo, totalAmmo);
            }         
        }
    }

    public static void UpdateAmmoForPlayer(string playerId, RangedWeaponType weaponType) // Player ammo UI
    {
        UpdatePlayerUI(weaponType, playerId);
    }


    private static void EnsureWeaponDataExists(string attackerId, RangedWeaponType rangedWeapon)
    {
        
        if (!_weaponAmmunition.ContainsKey(attackerId)) // Ensure ammunition data exists for the entity
        {
            _weaponAmmunition[attackerId] = new Dictionary<RangedWeaponType, AmmunitionHandler>();
        }

        if (!_weaponCooldowns.ContainsKey(attackerId)) // Ensure cooldown data exists for the entity
        {
            _weaponCooldowns[attackerId] = new Dictionary<RangedWeaponType, float>();
        }

        if (!_activeCooldownCoroutines.ContainsKey(attackerId)) // Ensure active cooldown coroutines exist for the entity
        {
            _activeCooldownCoroutines[attackerId] = new HashSet<RangedWeaponType>();
        }
       
        if (!_weaponAmmunition[attackerId].ContainsKey(rangedWeapon)) // Ensure ammo handler exists for the given weapon
        {
            if (_rangedWeaponData.TryGetValue(rangedWeapon, out SORangedWeapon weaponData))
            {
                _weaponAmmunition[attackerId][rangedWeapon] = new AmmunitionHandler(weaponData);
            }
        }
    }



}
