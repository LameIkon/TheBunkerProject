using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform parentObject; // To know what to flip
    [SerializeField] private Transform _rotationPivot; // What to rotate around
    private RotationManager rotationManager; // Rotation Manager
    [SerializeField, TextArea(1,2)] private string _description; // Write a comment 

    [Header("Rotation Settings")]
    [SerializeField] private float _downAngle = -40f;   // Minimum rotation angle
    [SerializeField] private float _upAngle = 40f;      // Maximum rotation angle
    [SerializeField] private float _rotationSpeed = 6f; // Adjust in inspector
    [SerializeField] private float _offsetDirection = 0f; // Adjust in inspector. Offsets sprite mouse direction
    [SerializeField] private bool _flipToDefault = false; // Experimental code atm

    private void Start()
    {
        rotationManager = RotationManager.Instance;
    }

    void Update()
    {
        // Call the RotationManager's UpdateRotation method
        if (RotationManager.CanRotate)
        {
            rotationManager.UpdateRotation(parentObject, _rotationPivot, _upAngle, _downAngle, _offsetDirection, _rotationSpeed, _flipToDefault);
        }
    }
}
