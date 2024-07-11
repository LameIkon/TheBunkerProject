using System.Collections;
using UnityEngine;

public class LadderHandler : MonoBehaviour
{
    public static bool _CanUseLadder; // CHecks when a player collides with latter
    public static bool _IsUsingLadder;
    public static bool _ExitLadder; // CHecks when a player hits the end of a latter
    public static bool _LadderOnCoolDown; // Delay before can use latter again

    // Update is called once per frame
    void Update()
    {
        if (_LadderOnCoolDown)
        {
            StartCoroutine(LadderCooldown());
        }
    }

    private IEnumerator LadderCooldown() // When exiting a ladder. Small cooldown
    {
        _LadderOnCoolDown = false;
        _CanUseLadder = false;
        yield return new WaitForSeconds(1f);
        
        // Can be expanded. Stopped since im too lazy to continue

    }

}
