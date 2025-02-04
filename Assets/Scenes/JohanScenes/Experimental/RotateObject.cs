using UnityEngine;

public class RotateObject : MousePosition
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float _rotationSpeed = 6f; // Adjust in inspector
    [SerializeField] private float _offsetDirection = 0f; // Adjust in inspector. Offsets sprite

    [SerializeField] private float _downAngle = -40f; // Minimum rotation angle
    [SerializeField] private float _upAngle = 40f;  // Maximum rotation angle

    [SerializeField] private Transform parentObject;
    private bool _isFlipped = false;


    void Update()
    {
        Mousedirection();
    }

    private void Mousedirection()
    {
        // Get the mouse position in world space
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
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
        float minAngle = _isFlipped ? -_upAngle : _downAngle;
        float maxAngle = _isFlipped ? -_downAngle : _upAngle;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _offsetDirection;
        
        // Clamp within the flipped range
        float clampedAngle = Mathf.Clamp(angle, minAngle, maxAngle);

        Quaternion targetRotation = Quaternion.Euler(0, 0, clampedAngle);

        // Smoothly rotate the pivot object
        pivot.rotation = Quaternion.Slerp(pivot.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

    }
}
