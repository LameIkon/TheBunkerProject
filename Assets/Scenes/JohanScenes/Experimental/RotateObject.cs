using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("References")]
    private Transform parentObject; // what to rotate
    [SerializeField] private Transform pivot; // What to rotate around
    private RotationManager rotationManager; // Rotation Manager

    [Header("Rotation Settings")]
    [SerializeField] private float _downAngle = -40f;   // Minimum rotation angle
    [SerializeField] private float _upAngle = 40f;      // Maximum rotation angle
    [SerializeField] private float _rotationSpeed = 6f; // Adjust in inspector
    [SerializeField] private float _offsetDirection = 0f; // Adjust in inspector. Offsets sprite mouse direction
    [SerializeField] private bool _flipToDefault = false;

    private void Start()
    {
        parentObject = FindTopParent(transform);
        rotationManager = RotationManager.Instance;
    }

    void Update()
    {
        // Call the RotationManager's UpdateRotation method
        rotationManager.UpdateRotation(parentObject, pivot, _upAngle, _downAngle, _offsetDirection, _rotationSpeed, _flipToDefault);
    }

    Transform FindTopParent(Transform currentTransform)
    {
        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
        }
        return currentTransform;
    }
}
