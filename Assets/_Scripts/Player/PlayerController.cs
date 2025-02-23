using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    private bool _isFacingRight = true;

    [SerializeField] private PlayerInput _playerInput;
    public PlayerInput PlayerInput => _playerInput;

    [Header("Movement")]
    [SerializeField] private float _currentMoveSpeed;
    [SerializeField] private float _normalMoveSpeed;
    [SerializeField] private float _backwardsMoveSpeed;
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

    [Header("State")]
    public string actionState = "Idle"; // What type of movement player is doing - changes depending what player do of movement
    public string weaponType = "Unarmed"; // What weapon player is holding - changes depending what player is holding


    private void OnEnable()
    {
        CurrentWeapon.OnWeaponChanged += UpdateWeaponType;
    }

    private void OnDisable()
    {
        CurrentWeapon.OnWeaponChanged -= UpdateWeaponType;
    }

    private void Start()
    {
        _currentMoveSpeed = _normalMoveSpeed; // Assign movement speed
    }


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
            _rb.velocity = new Vector2(_movementX * _currentMoveSpeed, _rb.velocity.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        _movementX = context.ReadValue<Vector2>().x;


        // Get the mouse position and determine which direction the player is facing
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool isFacingLeft = mousePos.x < transform.position.x; 
        bool isFacingRight = mousePos.x > transform.position.x;

        // Check if the player is moving in the opposite direction
        bool isMovingOpposite = (isFacingLeft && _movementX > 0) || (isFacingRight && _movementX < 0);

        if (weaponType != "Unarmed") // Can only flip if not armed. RotationManager will handle weapons
        { 
            RotationManager.CanRotate = true;
        }
        else
        {
            Flip(context);
            RotationManager.CanRotate = false;
        }  

        if (context.performed || context.canceled)
        {
            if (weaponType != "Unarmed" && isMovingOpposite) // Figure out if should go backwards depending on where you are aiming
            {
                Debug.Log("going backwards");
                _currentMoveSpeed = _backwardsMoveSpeed;
                actionState = context.performed ? "WalkingBackwards" : "Idle";
            }
            else
            {
                _currentMoveSpeed = _normalMoveSpeed;
                actionState = context.performed ? "Walking" : "Idle";
            }

            AnimationHandler($"{actionState}{weaponType}");
        }
        

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
                    gameObject.layer = LayerMask.NameToLayer("Player");
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
                _currentElevator.ShowFloorPanel(); // Call the elevator script
            }
        }
    }
    #endregion
    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door")) // If trigger door
        {
            _currentDoor = collision.GetComponentInParent<Door>(); // Get the door script
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

  

    private void Flip(CallbackContext context)
    {
        if (_movementX < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (_movementX > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void UpdateWeaponType()
    {

        if (CurrentWeapon._currentWeapon != null)
        {
            if (weaponType != "Unarmed") // Can only flip if not armed. RotationManager will handle weapons
            {
                RotationManager.CanRotate = true;
            }
            else
            {
                RotationManager.CanRotate = false;
            }

            weaponType = CurrentWeapon._currentWeapon._weapon._WeaponCategory.ToString(); // Convert enum to string

            if ("Knife" == weaponType) // REMOVE LATER. only to show animations work. reason is we dont have a unarmed state at the moment
            {
                weaponType = "Unarmed";
            }

            AnimationHandler($"{actionState}{weaponType}");
        }
    }

    private void AnimationHandler(string state)
    {
        switch (state)
        {
            case "WalkingRifle":
                _animator.Play("WalkingHoldingRifle");
                break;
            case "WalkingBackwardsRifle":
                _animator.Play("WalkingBackwardsRifle");
                break;
            case "WalkingUnarmed":
                _animator.Play("WalkingUnarmed");
                break;
            case "IdleRifle":
                _animator.Play("IdleHoldingRifle");
                break;
            case "IdleUnarmed":
                _animator.Play("IdleUnarmed");
                break;
            default:
                _animator.Play("IdleUnarmed");
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(_groundCheckPos.position, _groundCheckRadius);
    }
}
