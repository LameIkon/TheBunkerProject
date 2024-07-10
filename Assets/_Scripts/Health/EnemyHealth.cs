using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private FloatVariable _health;
    [SerializeField] private FloatVariable _maxHealth;

    private float _startHealth = 150;

    private void Awake()
    {
        if ( _health == null ) 
        {
            _health = ScriptableObject.CreateInstance<FloatVariable>();
            _health.SetValue(_maxHealth);
        }

        if(_maxHealth == null)
        {
            _maxHealth = ScriptableObject.CreateInstance<FloatVariable>();
            _maxHealth.SetValue(_startHealth);
        }

        _health.SetValue(_maxHealth);
        
    }


    public void TakeDamage (float damage)
    {
        _health.ApplyChange(-damage);

        if(_health._Value <= 0)
        {            
            print("Enemy Dead");
            Destroy(gameObject);
        }
    }
}
