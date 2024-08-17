using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeToPistol : CurrentWeapon
{
    public void ChangeWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _IsKnife = false;
            _IsPistol = true;
            _IsRifle = false;
            _IsShotgun = false;
            print("Knife is: " + _IsKnife + " |Pistol is: " + _IsPistol + " |Rifle is: " + _IsRifle + " |Shotgun is: " + _IsShotgun);
        }
    }
}
