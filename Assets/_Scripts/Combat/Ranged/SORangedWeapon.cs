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
        RaycastHit2D hit = Physics2D.Raycast(attackOrigin, attackDirection, SO_AttackRange);

        Debug.DrawRay(attackOrigin, attackDirection * attackLength, Color.red, 0.5f);


        if (hit.collider != null && hit.collider.gameObject != attacker.parent.gameObject && hit.collider.gameObject != attacker.gameObject) // Check if the raycast hit something and not themselves
            {
            //Debug.Log(hit.collider.name);

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


    //[Space(5f)] 
    //[Header("Ammunition")]
    //public GameObject SO_BulletPrefab;
    //public int SO_MaxAmmoCapacity;
    //public IntReferencer SO_Magazine;
    //public IntVariable SO_CurrentAmmoCount;
    //private bool SO_fullMagazine;
    //private bool SO_emptyMagazine;


    //[SerializeField] private int SO_magazineMaxCapacity; // How many magazines 
    //[SerializeField] private int SO_currentMagazineAmmo; // how much ammo in current magazine
    //[SerializeField] private int SO_currentMagazineAmount; // how many magazines an entity currently has

    //public bool CheckAmmonition()
    //{
    //    Debug.Log(SO_Ammunition.SO_InitialAmmoCount._Value);
    //    int currentValue = SO_Ammunition.SO_InitialAmmoCount._Value;
    //    if (currentValue > 0)
    //    {;
    //        return true;
    //    }
    //    return false;
    //}

    //private void ReduceAmmoByShooting()
    //{
    //    if (SO_emptyMagazine)
    //    {
    //        SO_Ammunition.SO_InitialAmmoCount.ApplyChange(-1);
    //    }
    //}


    //private void SetAmmoToMax() 
    //{
    //    SO_Ammunition.SO_InitialAmmoCount.SetValue(SO_Ammunition.SO_MaxAmmoCapacity);
    //}


    //private void UpdateAmmoCount()
    //{
    //    SO_Ammunition.SO_Magazine.SetValue(SO_Ammunition.SO_InitialAmmoCount);
    //    CheckIfMagazineIsFullOrEmpty();
    //}

    //private void CheckIfMagazineIsFullOrEmpty()
    //{
    //    SO_fullMagazine = (SO_Ammunition.SO_Magazine.GetValue() == SO_Ammunition.SO_MaxAmmoCapacity);
    //    SO_emptyMagazine = (SO_Ammunition.SO_Magazine.GetValue() <= 0);
    //}

    //private void GainAmmo()
    //{
    //    if (!SO_fullMagazine)
    //    {
    //        SetAmmoToMax();
    //    }
    //}

    //private void ReloadWeapon()
    //{

    //}

    //private void GainMagazine()
    //{

    //}
}
