using System.Collections;
using UnityEngine;


public class Door : Highlight
{
    [Header ("Door Components")]
    public Animator _Animator;
    public bool _IsOpen;



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
            }
            else
            {
                Debug.Log("Close");
                _Animator.Play("Open Door");
            }
            yield return new WaitForSeconds(0.6f);
            _IsOpen = !_IsOpen;
            _Interact = true;
        }
    }
}
