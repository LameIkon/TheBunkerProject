using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private FloatVariable _health; // ONLY SET ON PLAYER - Enemies need to be empty in order to create seperate floatvariables. 
    [SerializeField] private FloatVariable _startHealth; //start health. For monsters this sets what their health is going to be when created. For player, it reset health to starthealth on awake.

    private void Awake()
    {
        if ( _health == null ) //for monsters
        {
            _health = ScriptableObject.CreateInstance<FloatVariable>();
            _health.SetValue(_startHealth);
        }

        _health.SetValue(_startHealth); //for player
    }


    public void TakeDamage (float damage) //used for combat
    {
        _health.ApplyChange(-damage);

        if(_health._Value <= 0)
        {                   
            Destroy(gameObject);
        }
    }

    public void HealDamage (float healAmount) //only used if ghouls needs to heal for some reason? Player heals with MedKitViewer
    {
        _health.ApplyChange(healAmount);
    }
}
