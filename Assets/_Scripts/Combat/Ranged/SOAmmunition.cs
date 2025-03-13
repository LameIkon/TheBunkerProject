using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammunition", menuName = "Weapons/Ammunition")]
public class SOAmmunition : ScriptableObject
{
    public int SO_MaxAmmoCapacity;
    public IntReferencer SO_Magazine;
    public IntVariable SO_CurrentAmmoCount;

    public bool SO_CanConsumeAmmo;
    public bool SO_CanReload;
}
