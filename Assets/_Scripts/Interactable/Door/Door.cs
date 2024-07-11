using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool _interact; // will change to a new system later
    private bool _isOpen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _interact = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _interact = false;
    }

    public void UseDoor(InputAction.CallbackContext context)
    {
        if (context.performed && _interact)
        {
            StartCoroutine(DoorTransition());
        }
    }


    IEnumerator DoorTransition()
    {
        if (_interact)
        {
            _interact = false;
            if (_isOpen)
            {
                animator.Play("Close Door");
            }
            else
            {
                animator.Play("Open Door");
            }
            yield return new WaitForSeconds(0.6f);
            _isOpen = !_isOpen;
            _interact = true;
        }
    }
}
