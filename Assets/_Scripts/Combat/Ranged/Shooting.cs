using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting2 : Weapon
{
    public override void Shoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Fire();       
            
        }
    }    
}
