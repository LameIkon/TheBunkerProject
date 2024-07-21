using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    private bool _isFacingRight = true;


    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    public float _movementX;
    public float _movementY;

    [Header("Ground")]
    [SerializeField] private Transform _groundCheckPos;
    [SerializeField] private Vector2 _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Climbing")]
    [SerializeField] private float _climbSpeed;

    [Header("Interact With")]
    [SerializeField] private Door _currentDoor; // Used to check which door player interacts with
    [SerializeField] private Elevator _currentElevator; // Used to check which elevator player interacts with
    [SerializeField] private Ladder _currentLadder; // Used to check which ladder player interacts with

     private float _ladderXPosition;
     private float _ladderCenteringSpeed = 5f;
     private bool _isCenteringLadder;

    // Update is called once per frame
    void Update()
    {
        Movement();       
    }

    public void Movement()
    {
        if (_currentLadder != null && _currentLadder._UsingLadder) // Use Ladder
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _climbSpeed * _movementY);
            StartCoroutine(LadderCentering());
            
        }
        else // Walk
        {
            _rb.velocity = new Vector2(_movementX * _moveSpeed, _rb.velocity.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        _movementX = context.ReadValue<Vector2>().x;
        Flip();
    }


    #region Ladder
    public void UseLatter(InputAction.CallbackContext context)
    {
        _movementY = context.ReadValue<Vector2>().y;

        if (_currentLadder != null)
        {
            if (_currentLadder._ExitLadder) // Exit ladder
            {
                if (context.canceled)
                {
                    _rb.gravityScale = 1; // Enable gavity
                    _rb.velocity = new Vector2(0, 0); // Stops the player from moving forward
                    _currentLadder.CurrentlyUsingLadder(false);
                    _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    gameObject.layer = LayerMask.NameToLayer("Default");
                }
            }

            else if (_currentLadder._Interact)
            {
                if (context.performed) // Climb ladder
                {
                    _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; // Freezes both Z and Y axis
                    gameObject.layer = LayerMask.NameToLayer("Ignore Ground"); // Allows going through floors
                    _currentLadder.CurrentlyUsingLadder(true);               
                }
                else if (context.canceled) // Pause while on ladder
                {
                    _rb.gravityScale = 0; // Disable gavity
                    _rb.velocity = new Vector2(0,0); // Stops the player from moving forward
                    _currentLadder.CurrentlyUsingLadder(false);
                }
            }       
        }      
    }

    private IEnumerator LadderCentering()
    {
        // Center the player to the ladder
        if (!_isCenteringLadder) // Ensure only once instance
        {
            _isCenteringLadder = true;
            while (Mathf.Abs(transform.position.x - _ladderXPosition) > 0.02f) // Tolerance. Stop centering when getting sufficiently close
            {
                Vector2 targetPosition = new Vector2(_ladderXPosition, transform.position.y);
                transform.position = Vector2.Lerp(transform.position, targetPosition, _ladderCenteringSpeed * Time.deltaTime);
                Debug.Log("Centering...");
                yield return new WaitForFixedUpdate(); // Wait for the next fixed frame
            }
            _isCenteringLadder = false;
        }    
    }



    #endregion
    #region Interacting
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_currentDoor != null && _currentDoor._Interact) // Door
            {
                StartCoroutine(_currentDoor.DoorTransition()); // Call the door script coroutine
            }
            else if (_currentElevator != null && _currentElevator._Interact) // Elevator
            {
                _currentElevator.ShowFloorPanel();
            }
        }
    }
    #endregion
    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door")) // If trigger door
        {
            _currentDoor = collision.GetComponentInChildren<Door>(); // Get the door script
        }
        if (collision.CompareTag("Elevator")) // If trigger elevator
        {
            _currentElevator = collision.GetComponentInParent<Elevator>(); // Get the elevator script
        }
        if (collision.CompareTag("Ladder")) // if trigger ladder
        {
            _currentLadder = collision.GetComponentInParent<Ladder>(); // Get the ladder script
            _ladderXPosition = _currentLadder.transform.position.x; // Store the ladder's x position
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door") && collision.GetComponentInChildren<Door>() == _currentDoor) // Might not need to be this complicated but only deselect if exiting that door trigger
        {
            _currentDoor = null; // Set to null since dont need anymore          
        }
        if (collision.CompareTag("Elevator") && collision.GetComponentInParent<Elevator>() == _currentElevator) // Deselect elevator trigger
        {
            _currentElevator = null; // Set to null since dont need anymore
        }
        if (collision.CompareTag("Ladder") && collision.GetComponentInParent<Ladder>() == _currentLadder)
        {
            _currentLadder = null; // Set to null since dont need anymore
        }
    }
    #endregion

    // For Jumping. Might be useful later so dont delete
    //private bool isGrounded()
    //{
    //    if(Physics2D.OverlapBox(_groundCheckPos.position, _groundCheckRadius, 0, _groundLayer))
    //    {
    //        return true;
    //    }
    //    return false;
    //}

  

    private void Flip()
    {     
        if (_isFacingRight && _movementX < 0f || !_isFacingRight && _movementX > 0f)
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f,180f,0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(_groundCheckPos.position, _groundCheckRadius);
    }
}
