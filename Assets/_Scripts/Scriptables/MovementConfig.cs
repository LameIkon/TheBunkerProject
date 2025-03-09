using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementConfig", menuName = "Configs/MovementConfig")]
public class MovementConfig : ScriptableObject
{
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float _runningSpeed;
    [SerializeField] private float _crouchingSpeed;
    [SerializeField] private float _backwardsMoveSpeed;
    [SerializeField] private float _climbSpeed;

    // Getters
    public float WalkingSpeed => _walkingSpeed;
    public float RunningSpeed => _runningSpeed;
    public float CrouchingSpeed => _crouchingSpeed;
    public float BackwardsMoveSpeed => _backwardsMoveSpeed;
    public float ClimbSpeed => _climbSpeed;
}
