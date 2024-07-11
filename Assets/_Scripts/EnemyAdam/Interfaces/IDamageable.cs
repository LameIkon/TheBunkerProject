public interface IDamageable
{
    void Damage(float damageAmount);

    void Die();
    
    float _MaxHealth { get; set; }
    
    float _CurrentHealth { get; set; }
}
