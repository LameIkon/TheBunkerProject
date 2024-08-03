using System.Collections;
using UnityEngine;


public class Door : Highlight
{
    [Header ("Door Components")]
    [SerializeField] private Animator animator;
    private bool _isOpen;


    public IEnumerator DoorTransition()
    {
        if (_Interact)
        {
            Debug.Log("called for door");
            _Interact = false;
            if (_isOpen)
            {
                Debug.Log("Open");
                animator.Play("Close Door");
            }
            else
            {
                Debug.Log("Close");
                animator.Play("Open Door");
            }
            yield return new WaitForSeconds(0.6f);
            _isOpen = !_isOpen;
            _Interact = true;
        }
    }
}
