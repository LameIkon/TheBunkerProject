using System;
using System.Collections;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    private bool _isFlipped = false;
    private Coroutine angleResetCoroutine;
    private float previousAngle;


    public void UpdateRotation(Transform parentObject, Transform pivot, float upAngle, float downAngle, float offsetDirection, float rotationSpeed, bool flipDefault)
    {
        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - pivot.position;

        // Determine whether to flip
        if (mousePos.x < pivot.position.x && !_isFlipped)
        {
            parentObject.localScale = new Vector3(-1, 1, 1);
            _isFlipped = true;
        }
        else if (mousePos.x > pivot.position.x && _isFlipped)
        {
            parentObject.localScale = new Vector3(1, 1, 1);
            _isFlipped = false;
        }
       

        // Adjust direction based on flipping
        if (_isFlipped)
        {
            direction.y = -direction.y; 
            direction.x = -direction.x;
        }

        // Flip angle constraints when flipped
        float minAngle = _isFlipped ? -upAngle : downAngle;
        float maxAngle = _isFlipped ? -downAngle : upAngle;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offsetDirection;
        
        float zAngle = pivot.localEulerAngles.z;
        if(flipDefault)
        {
            if (zAngle > 160f)
            {
                zAngle -= 360f;  // Convert angles greater than 180 to negative angles
            }
            if (Mathf.Round(zAngle) <= downAngle)
            {
                if (angleResetCoroutine == null)
                {
                    angleResetCoroutine = StartCoroutine(ResetAngleCoroutine(angle, OnResetComplete));
                }
            }
        }



            // Clamp within the flipped range
            float clampedAngle = Mathf.Clamp(angle, minAngle, maxAngle);

            Quaternion targetRotation = Quaternion.Euler(0, 0, clampedAngle);

            // Smoothly rotate the pivot object
            pivot.rotation = Quaternion.Slerp(pivot.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        

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
