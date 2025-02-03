using UnityEngine;

public class RotateObject : MousePosition
{
    [SerializeField] private float _rotationSpeed = 6f; // Adjust in inspector
    [SerializeField] private float _offsetDirection = 0f; // Adjust in inspector. Offsets sprite

    void Update()
    {
        Mousedirection();
    }

    private void Mousedirection()
    {
        // Get the mouse position in world space
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction from the player to the mouse position
        Vector3 direction = mousePos - transform.position;

        // Calculate the target rotation for the gun (aligns with direction)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the offset to the angle
        angle += _offsetDirection;

        // Create the desired rotation
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}
