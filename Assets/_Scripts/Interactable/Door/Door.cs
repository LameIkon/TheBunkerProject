using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Highlight _highlightScript;
    public bool _Interact  { get; private set; } // will change to a new system later
    private bool _isOpen;


    private void OnTriggerEnter2D(Collider2D collision)
    {       
        //if (collision.CompareTag("Player"))
        //{
        //    if (_highlightScript == null)
        //    {
        //        _highlightScript = GetComponentInChildren<Highlight>();
        //    }

        //    if (!_highlightScript.TriggerEnter(gameObject)) // Checks if you can interact or not
        //    {
        //        return; // If it can't it stops here
        //    }
        //}
        Debug.Log("entered");
        _Interact = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //_highlightScript.TriggerExit(gameObject);
            _Interact = false;
        }
    }

    public IEnumerator DoorTransition()
    {
        if (_Interact)
        {
            _Interact = false;
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
            _Interact = true;
        }
    }
}
