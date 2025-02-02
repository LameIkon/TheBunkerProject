using UnityEngine;

public class SimpleHeadMove : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 6f; // Adjust in inspector

    void Update()
    {
        Mousedirection();
    }

    private void Mousedirection()
    {
        // Mouse position in viewport space (normalized)
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToViewportPoint(mousePosition); // Use ScreenToViewportPoint

        // Get direction based on the mouse position (relative to the camera's forward)
        Vector2 direction = new Vector2(mousePosition.x - 0.5f, mousePosition.y - 0.5f); // Center the mouse position

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, -direction);

        // Transition to new direction
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
