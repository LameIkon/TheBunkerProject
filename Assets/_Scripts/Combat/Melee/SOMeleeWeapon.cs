using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Weapons/MeleeWeapon")]
public class SOMeleeWeapon : SOWeaponStats
{
    [Header("Melee Components")]
    [SerializeField] private float SO_verticalAttackRange;
    public MeleeWeaponType SO_MeleeWeaponType;

    public void PerfomAttack(Transform attacker)
    {
        Vector2 attackOrigin = attacker.position; // attacker position
        Vector2 attackDirection = attacker.transform.right * Mathf.Sign(attacker.parent.localScale.x); ; // attack direction

        float attackWidth = SO_verticalAttackRange; // Vertical attack range
        float attackLength = SO_AttackRange; // Horizontal attack range



        RaycastHit2D[] hits = Physics2D.BoxCastAll(attackOrigin, new Vector2(attackWidth, attackLength), 0f, attackDirection, SO_AttackRange);  // Cast a ray in the attack direction with _AttackRadius length

        foreach (var hit in hits)
        {
            
            if (hit.collider != null) // Check if the raycast hit something
            {
                Debug.Log(hit.collider.name);

                if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable)) // Try to get an IDamageable component from the hit object
                {
                    // Apply damage to the enemy
                    damageable.TakeDamage(DamageOutput());
                    Debug.Log("Hit " + hit.collider.name);
                }
            }
        }

        // Inspector visualization
        Vector2 topLeft = attackOrigin + Vector2.up * (attackWidth / 2);
        Vector2 bottomLeft = attackOrigin - Vector2.up * (attackWidth / 2);
        Vector2 topRight = topLeft + attackDirection * SO_AttackRange;
        Vector2 bottomRight = bottomLeft + attackDirection * SO_AttackRange;

        // Draw the attack hitbox
        Debug.DrawLine(topLeft, topRight, Color.green, 0.5f);
        Debug.DrawLine(bottomLeft, bottomRight, Color.green, 0.5f);
        Debug.DrawLine(topLeft, bottomLeft, Color.red, 0.5f);
        Debug.DrawLine(topRight, bottomRight, Color.red, 0.5f);
    }
}
