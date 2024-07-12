using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Melee")]
public class MeleeWeapon : WeaponBase
{

    [Header("Attack Radius")]
    public Vector3 _AttackPoint = Vector3.zero;
    public float _AttackRadius = 1f;

    private RaycastHit2D[] _hits;
    private List<IDamageable> _iDamageables = new List<IDamageable>(); // This List is made for future profing the way we handle hits.


    // In the future the implementation should change such that it relies on the animation that plays when attacking.
    public override IEnumerator Attack(AttackTestScript player)
    {
        // Takes all the GameObject that it overlaps with 
        _hits = Physics2D.CircleCastAll(player.GetAttackPoint(), _AttackRadius, player.transform.right);

        // Get the IDamageable interface and damage all the GameObjects it overlaps with.
        foreach(RaycastHit2D hit in _hits) 
        {
            IDamageable iDamageable = hit.collider.gameObject.GetComponent<IDamageable>();

            if (iDamageable != null && !_iDamageables.Contains(iDamageable)) 
            {
                iDamageable.TakeDamage(DamageOutput());
                _iDamageables.Add(iDamageable);
            }
        }
        
        yield return null;

        // Here all the IDamageables are returned to a state where they can be damaged again
        _iDamageables.Clear();

    }


    // This is for a visualising the area the melee attack hits
    public override void Draw(AttackTestScript player) 
    {
        Vector3 attackPoint = player.GetAttackPoint();
        Gizmos.DrawWireSphere(attackPoint, _AttackRadius);
    }

}
