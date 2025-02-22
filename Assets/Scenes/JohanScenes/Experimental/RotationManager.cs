using System;
using System.Collections;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    private bool _isFlipped = false;
    private Coroutine angleResetCoroutine;
    private float previousAngle;
    public static bool CanRotate;

    public static RotationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) // if instance already exists destroy
        {
            Destroy(this);
        }
        else // Else make this the singleton
        {
            Instance = this;
        }
    }

    public void UpdateRotation(Transform parentObject, Transform pivot, float upAngle, float downAngle, float offsetDirection, float rotationSpeed, bool flipDefault)
    {
        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - pivot.position;

        // Determine whether to flip
        if (mousePos.x < pivot.position.x && !_isFlipped) // Look left
        {
            parentObject.localScale = new Vector3(-1, 1, 1);
            _isFlipped = true;
        }
        else if (mousePos.x > pivot.position.x && _isFlipped) // Look right
        {
            parentObject.localScale = new Vector3(1, 1, 1);
            _isFlipped = false;
        }

        // Flip angle constraints when flipped
        float minAngle = _isFlipped ? -upAngle : downAngle;
        float maxAngle = _isFlipped ? -downAngle : upAngle;

        // If flipped we use minus else it will be a plus
        float angle = Mathf.Atan2(
            _isFlipped ? -direction.y : direction.y,
            _isFlipped ? -direction.x : direction.x
        ) * Mathf.Rad2Deg + offsetDirection;

        float zAngle = pivot.localEulerAngles.z;

        // Clamp within the flipped range
        float clampedAngle = Mathf.Clamp(angle, minAngle, maxAngle);

        Quaternion targetRotation = Quaternion.Euler(0, 0, clampedAngle);

        // Smoothly rotate the pivot object
        pivot.rotation = Quaternion.Slerp(pivot.rotation, targetRotation, rotationSpeed * Time.deltaTime);



        // experimental code below. Currently does nothing
        if (flipDefault)
        {
            if (zAngle > 160f)
            {
                zAngle -= 360f;  // Convert angles greater than 180 to negative angles
            }
            if (Mathf.Round(zAngle) <= downAngle)
            {
                if (angleResetCoroutine == null)
                {
                    angleResetCoroutine = StartCoroutine(ResetAngleCoroutine(angle,OnResetComplete));
                }
            }
        }



           
        

    }

    private IEnumerator ResetAngleCoroutine(float angel, Action<float> OnResetComplete)
    {
        Debug.Log("called");
        yield return new WaitForSeconds(2f);  // Wait for 2 seconds before resetting
        Debug.Log("Angle reset to 0 after delay");
        angleResetCoroutine = null;
        OnResetComplete(angel);
    }

    private void OnResetComplete(float angel)
    {
        Debug.Log("called after coroutine");
    }
}
