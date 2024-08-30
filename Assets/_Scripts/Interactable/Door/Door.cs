using System.Collections;
using UnityEngine;


public class Door : Highlight
{
    [Header ("Door Components")]
    public Animator _Animator;
    public bool _IsOpen;
    [SerializeField] private bool _allowAutomaticClose;
    private float _closingTimer = 2f;
    public Coroutine _AutomaticDoorCloseCoroutine;


    public IEnumerator DoorTransition()
    {
        if (_Interact)
        {
            Debug.Log("called for door");
            _Interact = false;
            if (_IsOpen)
            {
                Debug.Log("Open");
                _Animator.Play("Close Door");
                _IsOpen = false;
            }
            else
            {
                Debug.Log("Close");
                _Animator.Play("Open Door");
                _IsOpen = true;
            }
            yield return new WaitForSeconds(0.6f);
            _Interact = true;
        }
    }

    public IEnumerator DoorAutomaticClose()
    {
        if (_allowAutomaticClose)
        {
            Debug.Log("Triggered");        
            yield return new WaitForSeconds(_closingTimer); // Wait a bit before closing door automatic
            _IsOpen = false;
            _Animator.Play("Close Door"); // Close Door           
        }
    }
}
