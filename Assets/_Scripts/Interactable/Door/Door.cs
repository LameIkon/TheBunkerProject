using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Highlight _highlightScript;
    private bool _interact; // will change to a new system later
    private bool _isOpen;
    public static bool _ThisDoorOnly;


    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.CompareTag("Player"))
        {
            if (_highlightScript == null)
            {
                _highlightScript = GetComponentInChildren<Highlight>();
            }

            if (!_highlightScript.TriggerEnter(gameObject)) // Checks if you can interact or not
            {
                return; // If it can't it stops here
            }
            _interact = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _highlightScript.TriggerExit(gameObject);
            _interact = false;
        }
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
