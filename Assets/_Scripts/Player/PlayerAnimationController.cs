using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Dictionary<string, string> _animationStates; // This will hold all the animations

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        PopulateAnimationStates();
    }

    public void PlayAnimation(string state)
    {
        _animator.Play(_animationStates.ContainsKey(state) ? _animationStates[state] : "IdleUnarmed");
    }

    private void PopulateAnimationStates()
    {
        _animationStates = new Dictionary<string, string> // Fill it
        {
            // Walking
            { "WalkingRifle", "WalkingHoldingRifle" },
            { "WalkingBackwardsRifle", "WalkingBackwardsRifle" },
            { "WalkingUnarmed", "WalkingUnarmed" },

            // Running
            { "RunningUnarmed", "RunningUnarmed" },
            { "RunningRifle", "RunningRifle" },

            // Crouching
            { "CrouchingIdleUnarmed", "CrouchingIdleUnarmed" },
            { "CrouchingWalkingUnarmed", "CrouchingWalkingUnarmed" },

            // Idle
            { "IdleRifle", "IdleHoldingRifle" },
            { "IdleUnarmed", "IdleUnarmed" }
        };
    }
}
