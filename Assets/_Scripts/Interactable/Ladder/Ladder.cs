using System.Collections;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    //public static bool _CanUseLadder; // CHecks when a player collides with latter
    //public static bool _IsUsingLadder;
    ////public static bool _ExitLadder; // Checks when a player hits the end of a latter
    private bool _ladderOnCoolDown; // Delay before can use latter again

    public bool _Interact  { get; private set; }  // Used to check when you can interact
    public bool _ExitLadder  { get; private set; }  // Used to check when you can interact
    public bool _UsingLadder  { get; private set; }  // Used to check when you can interact

    // Update is called once per frame
    void Update()
    {
        if (_ladderOnCoolDown)
        {
            StartCoroutine(LadderCooldown());
        }
    }

    private IEnumerator LadderCooldown() // When exiting a ladder. Small cooldown
    {
        _ladderOnCoolDown = false;
        //_CanUseLadder = false;
        yield return new WaitForSeconds(1f);
        
        // Can be expanded. Stopped since im too lazy to continue

    }

     public void SetInteract(bool interact) // On trigger it will say you can interact 
     {
        _Interact = interact;
     }

    public void CurrentlyUsingLadder(bool isUsingLadder) // Is currently interacting with ladder
    {
        _UsingLadder = isUsingLadder;
    }

     public void SetExit(bool exit) // leave ladder
     {
        _ExitLadder = exit;
     }

}
