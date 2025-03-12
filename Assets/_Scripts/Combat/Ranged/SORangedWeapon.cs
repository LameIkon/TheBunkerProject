using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeapon", menuName = "Weapons/RangedWeapon")]
public class SORangedWeapon : SOWeaponStats
{
    public RangedWeaponType SO_RangedWeapontype;

    [Space(5f)] 
    [Header("Ammunition")]
    public GameObject _BulletPrefab;
    public int _MaxAmmoCapacity;
    public IntReferencer SO_Magazine;
    public IntVariable SO_CurrentAmmoCount;
    private bool _fullMagazine;
    private bool _emptyMagazine;

    public void PerformAttack(Transform attacker)
    {
        
        Vector2 attackOrigin = attacker.position; // attacker position
        Vector2 attackDirection = attacker.transform.right * Mathf.Sign(attacker.parent.localScale.x); ; // attack direction

        float attackLength = SO_AttackRange; // Horizontal attack range
        RaycastHit2D hit = Physics2D.Raycast(attackOrigin, attackDirection, SO_AttackRange);
        ReduceAmmoByShooting(); //takes 1 from ammo amount


        if (hit.collider != null && hit.collider.gameObject != attacker.parent.gameObject && hit.collider.gameObject != attacker.gameObject) // Check if the raycast hit something and not themselves
            {
                //Debug.Log(hit.collider.name);

                // Try to get IDamageable from the hit object
                if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable)) // Check the gameobject for the component
                {
                    damageable.TakeDamage(DamageOutput());
                    //Debug.Log("Hit " + hit.collider.name);
                }
                else if (hit.collider.transform.parent != null && hit.collider.transform.parent.TryGetComponent<IDamageable>(out damageable)) // Otherwise look at parent gameobject for component
                {
                    // If not found on the object, check the parent
                    damageable.TakeDamage(DamageOutput());
                    //Debug.Log("Hit parent of " + hit.collider.name);
                }
            }
    }


    private void SetAmmoToMax() //Used in Awake in Weapon.cs
    {
        SO_CurrentAmmoCount.SetValue(_MaxAmmoCapacity);
    }

    private void UpdateAmmoCount()
    {
        SO_Magazine.SetValue(SO_CurrentAmmoCount);
        CheckIfMagazineIsFullOrEmpty();
    }

    private void CheckIfMagazineIsFullOrEmpty()
    {
        _fullMagazine = (SO_Magazine.GetValue() == _MaxAmmoCapacity);
        _emptyMagazine = (SO_Magazine.GetValue() <= 0);
    }

    private void ReduceAmmoByShooting()
    {
        if (!_emptyMagazine)
        {
            SO_CurrentAmmoCount.ApplyChange(-1); 
        }
    }

    private void GainAmmo()
    {
        if (!_fullMagazine)
        {
            SetAmmoToMax();
        }
    }
}
