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
    //private bool _lastPressedRight = false;
    //private bool _moveRight = false;
    //private bool _moveLeft = false;

    //public KeyCode _rightKey;
    //public KeyCode _leftKey;


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

        //if (_movementX == 0)
        //{
        //    _rb.velocity = new Vector2(0 * _moveSpeed, _rb.velocity.y);
        //}

        ////New 
        //else if (_movementX == 1)
        //{
        //    _rb.velocity = new Vector2(1 * _moveSpeed, _rb.velocity.y);
        //}

        //else if (_movementX == -1)
        //{
        //    _rb.velocity = new Vector2(-1 * _moveSpeed, _rb.velocity.y);
        //}


        /////////////////////////////////
        //if (_moveRight || _moveLeft)
        //    _movementX = 0;

        //if (_moveRight)
        //{
        //    _lastPressedRight = true;
        //}

        //if (_moveLeft)
        //{
        //    _lastPressedRight = false;
        //}

        //if (_moveRight && _moveLeft)
        //{
        //    if (_lastPressedRight)
        //    {
        //        _movementX = 1;
        //    }

        //    else
        //    {
        //        _movementX = -1;
        //    }

        //}

        //else if (_moveRight)
        //{
        //    _movementX = 1;
        //}

        //else if (_moveLeft)
        //{
        //    _movementX = -1;
        //}

    }

    //public void MoveRight(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        _moveRight = true;
    //        _lastPressedRight = true;
    //    }
    //}

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
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(_groundCheckPos.position, _groundCheckRadius);
    }


    //public void OnGUI()
    //{
    //    Event e = Event.current;
    //    if (e.isKey)
    //    {


    //        if (e.keyCode == KeyCode.D || e.keyCode == KeyCode.RightArrow)
    //        {
    //            _rightKey = e.keyCode;
    //        }

    //        if (e.keyCode == KeyCode.A || e.keyCode == KeyCode.LeftArrow)
    //        {
    //            _leftKey = e.keyCode;
    //        }
    //    }        
    //}
}
