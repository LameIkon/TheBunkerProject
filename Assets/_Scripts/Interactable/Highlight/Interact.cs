using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    private Door _doorScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject)
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }

    public void UseDoor(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //StartCoroutine();
        }
    }
}
