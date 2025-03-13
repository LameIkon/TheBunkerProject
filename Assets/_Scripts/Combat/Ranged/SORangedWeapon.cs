using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeapon", menuName = "Weapons/RangedWeapon")]
public class SORangedWeapon : SOWeaponStats
{
    [Header("Weapon Components")]
    public SOAmmunition SO_Ammunition;
    public float reloadTime;
    public RangedWeaponType SO_RangedWeapontype;

    public void PerformAttack(Transform attacker)
    { 
        
        Vector2 attackOrigin = attacker.position; // attacker position
        Vector2 attackDirection = attacker.transform.right * Mathf.Sign(attacker.parent.localScale.x); ; // attack direction

        float attackLength = SO_AttackRange; // Horizontal attack range
        RaycastHit2D[] hits = Physics2D.RaycastAll(attackOrigin, attackDirection, SO_AttackRange);

        Debug.DrawRay(attackOrigin, attackDirection * attackLength, Color.red, 0.5f);


        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != attacker.parent.gameObject && hit.collider.gameObject != attacker.gameObject) // Check if the raycast hit something and not themselves
            {
                // Try to get IDamageable from the hit object
                if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable)) // Check the gameobject for the component
                {
                    damageable.TakeDamage(DamageOutput());
                }
                else if (hit.collider.transform.parent != null && hit.collider.transform.parent.TryGetComponent<IDamageable>(out damageable)) // Otherwise look at parent gameobject for component
                {
                    // If not found on the object, check the parent
                    damageable.TakeDamage(DamageOutput());
                }
            }

        }
    }
}
