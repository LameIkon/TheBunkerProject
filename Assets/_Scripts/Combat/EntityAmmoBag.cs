using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAmmoBag : MonoBehaviour
{
    //[SerializeField] private SOAmmunition ammoTemplate;
    //private SOAmmunition ammoInstance;

    //public void InitializeAmmo() // initial ammo amount
    //{
    //    if (ammoTemplate != null)
    //    {
    //        ammoInstance = ammoTemplate.CreateInstance(); // Create a unique instance
    //    }
    //}

    //private void GainAmmo(int ammoGain)
    //{
    //    ammoInstance.ApplyAmmoChange(ammoGain);
    //}

    //private void Reload()
    //{
    //    if (ammoInstance != null && ammoInstance.SO_CanReload)
    //    {
    //        int ammoNeeded = ammoInstance.SO_MaxAmmoCapacity - ammoInstance.SO_CurrentAmmoCount;
    //        int ammoToReload = Mathf.Min(ammoInstance.SO_Magazine, ammoNeeded);

    //        ammoInstance.ApplyAmmoChange(ammoToReload);
    //        ammoInstance.SO_Magazine -= ammoToReload;
    //    }
    //}

}
