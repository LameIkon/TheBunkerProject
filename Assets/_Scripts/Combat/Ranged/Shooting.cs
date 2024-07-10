using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : Weapon
{
    public override void Shoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Fire();            
        }
    }

}
