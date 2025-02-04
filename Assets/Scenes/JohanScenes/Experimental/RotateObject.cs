using UnityEngine;

public class RotateObject : MousePosition
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float _rotationSpeed = 6f; // Adjust in inspector
    [SerializeField] private float _offsetDirection = 0f; // Adjust in inspector. Offsets sprite

    [SerializeField] private float _downAngle = -40f; // Minimum rotation angle
    [SerializeField] private float _upAngle = 40f;  // Maximum rotation angle

    private float _initialAngle;
    void Start()
    {

       
        _initialAngle = pivot.eulerAngles.z; // Store initial angle
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        Mousedirection();
    }

    private void Mousedirection()
    {
        // Get the mouse position in world space
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - pivot.position; 

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _offsetDirection;

        // Convert to local angle relative to the initial angle
        float relativeAngle = Mathf.DeltaAngle(_initialAngle, angle);

        // Clamp the relative angle within the allowed range
        float clampedAngle = Mathf.Clamp(relativeAngle, _downAngle, _upAngle);

        // Compute final rotation by applying the clamped angle to the initial angle
        float finalAngle = _initialAngle + clampedAngle;

        Quaternion targetRotation = Quaternion.Euler(0, 0, finalAngle);

        // Smoothly rotate the pivot object
        pivot.rotation = Quaternion.Slerp(pivot.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}
