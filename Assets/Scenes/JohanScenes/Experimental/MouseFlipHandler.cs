using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFlipHandler : MonoBehaviour
{
    // Returns the mouse position in world space
    public static Vector3 GetMouseWorldPosition(Camera camera)
    {
        return camera.ScreenToWorldPoint(Input.mousePosition);
    }


    // Flips the parent GameObject
    public static void FlipParent(Transform parentObject, bool shouldFlip)
    {
        if (parentObject != null)
        {
            // Flip the parent based on the condition
            parentObject.localScale = new Vector3(shouldFlip ? -1 : 1, 1, 1);
        }
    }
}
