using UnityEngine;

[System.Serializable]
public struct HealthStats
{
    public int _MaxHealth;
    public int _MinHealth;

    public HealthStats(int maxHealth, int minHealth)
    {
        _MaxHealth = maxHealth; // Initialize to 100
        _MinHealth = minHealth; // Initialize to 0
    }
}

public sealed class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private HealthStats _healthStats;
    [SerializeField] private int _currentHealth;
    public bool _IsAtMaxHealth;
    public bool _IsDead;

    private void Start()
    {
        _healthBar.SetMaxHealth(_healthStats._MaxHealth);
        _healthStats = new(_healthStats._MaxHealth, _healthStats._MinHealth);
        _currentHealth = _healthStats._MaxHealth;
    }

    private void Update()
    {
        HealthChecker();
        
        /*  
         *      The if-statement(s) with an Input.GetKey() method are placeholder logic to demonstrate the function(s)
         *      that damages (and heals the player).
         *
         *      MUST BE REMOVED OR REPLACED AT A LATER TIME!!
         */

        #region REMOVE THIS IF-STATEMENT ONCE A PERMANENT SOLUTION IS PUT IN PLACE.
        
        if (Input.GetKey(KeyCode.I))
        {
            TakeDamage(1);
        }
        
        #endregion
    }
    
    /*
     *      The TakeDamage method should be called by Enemies and other
     *      things that are supposed to damage the player.
     *      It is called temporarily in Update() as a placeholder.
     *
     *      REMOVE THIS COMMENT ONCE A PERMANENT SOLUTION IS PUT IN PLACE.
     */

    public void TakeDamage(int amount)
    {
        if (_IsDead)
        {
            return;
        }
        _currentHealth -= amount;
        UpdateHealthBar();
    }

    public void GainHealth()
    {
        if (_IsDead)
        {
            return;
        }
        _currentHealth = _healthStats._MaxHealth;
        UpdateHealthBar();
    }

    private void HealthChecker()
    {
        if (_currentHealth <= _healthStats._MinHealth)
        {
            _IsDead = true;
            return;
        }

        if (_currentHealth != _healthStats._MaxHealth)
        {
            _IsAtMaxHealth = false;
            return;
        }

        if (_currentHealth == _healthStats._MaxHealth)
        {
            _IsAtMaxHealth = true;
        }
    }

    private void UpdateHealthBar()
    {
        _healthBar.SetHealth(_currentHealth);
    }
}