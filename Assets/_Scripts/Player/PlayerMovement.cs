using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    private bool _isFacingRight = true;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    public float _movementX;
    

    [Header("Jumping")]
    [SerializeField] private float _jumpPower;

    [Header("Jumping")]
    [SerializeField] private Transform _groundCheckPos;
    [SerializeField] private Vector2 _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer;


    // Update is called once per frame
    void Update()
    {
        Movement();       
    }

    public void Movement()
    {        
        _rb.velocity = new Vector2(_movementX * _moveSpeed, _rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _movementX = context.ReadValue<Vector2>().x;
        Flip();
    }

    public void Jumping(InputAction.CallbackContext context)
    {
        if (isGrounded())
        {
            if (context.performed)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
            }
        }       
    }

    private bool isGrounded()
    {
        if(Physics2D.OverlapBox(_groundCheckPos.position, _groundCheckRadius, 0, _groundLayer))
        {
            return true;
        }
        return false;
    }
  

    private void Flip()
    {     
        if (_isFacingRight && _movementX < 0f || !_isFacingRight && _movementX > 0f)
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f, 180f, 0f);

            //Vector3 localScale = transform.localScale;
            //localScale.x *= -1f;
            //transform.localScale = localScale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(_groundCheckPos.position, _groundCheckRadius);
    }

}
