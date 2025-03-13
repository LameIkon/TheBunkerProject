using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IMovable, IIdentifiable, IWeaponUser
{
    [SerializeField] private Rigidbody2D _rb;
    private PlayerAnimationController _animationController;
    private bool _isFacingRight = true;

    [SerializeField] private PlayerInput _playerInput;
    public PlayerInput PlayerInput => _playerInput;

    [Header("Movement")]
    [SerializeField] private MovementConfig _movementConfig;
    private float _currentMoveSpeed;
    private bool _isRunning;
    private bool _isCrouching;
    private bool _isClimbing;
    public float _movementX;
    public float _movementY;

    [Header("Ground")]
    [SerializeField] private Transform _groundCheckPos;
    [SerializeField] private Vector2 _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer;


    [Header("Interact With")]
    [SerializeField] private Door _currentDoor; // Used to check which door player interacts with
    [SerializeField] private Elevator _currentElevator; // Used to check which elevator player interacts with
    [SerializeField] private Ladder _currentLadder; // Used to check which ladder player interacts with

    private float _ladderXPosition;
    private float _ladderCenteringSpeed = 5f;
    private bool _isCenteringLadder;

    private EntityActionState _currentActionState = EntityActionState.Idle;
    private WeaponType _currentWeaponType = WeaponType.Unarmed;

    public static string s_PlayerId; // To find the id in the GlobalWeaponHandler and allow us to display ammo UI


    private void OnEnable()
    {
        WeaponSelectionHandler.s_OnWeaponChanged += CheckWeaponType;
        RotationManager.OnFlipped += UpdateMovement;
    }

    private void OnDisable()
    {
        WeaponSelectionHandler.s_OnWeaponChanged -= CheckWeaponType;
        RotationManager.OnFlipped -= UpdateMovement;
    }

    private void Start()
    {
        s_PlayerId = GetUniqueEntityId();
        _animationController = GetComponent<PlayerAnimationController>();
        _currentMoveSpeed = _movementConfig.WalkingSpeed; // Assign movement speed
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    #region Movement
    public void Movement()
    {
        if (_currentLadder != null && _currentLadder._UsingLadder) // Use Ladder
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _movementConfig.ClimbSpeed * _movementY);
            StartCoroutine(LadderCentering());

        }
        else // Walk
        {
            _rb.velocity = new Vector2(_movementX * _currentMoveSpeed, _rb.velocity.y);
        }
    }

    public void Move(float movementX)
    {
        _movementX = movementX;
        Flip();
        Invoke(nameof(UpdateMovement), 0f); // Invoke next frame. Sometimes causes rare animation bug if not implemented
    }

    public void Sprint(bool isSprinting)
    {
        _isRunning = isSprinting;
        UpdateMovement();
    }

    public void Crouch(bool isCrouching)
    {
        _isCrouching = isCrouching;
        UpdateMovement();
    }

    public void Climb(bool isClimbing)
    {
        _isClimbing = isClimbing;
        //HandleLadderInteraction(_isClimbing);
    }



    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        Move(input.x);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        Sprint(context.performed);
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        Crouch(context.performed);
    }

    public void OnClimb(InputAction.CallbackContext context)
    {
        HandleLadderInteraction(context);
        if (context.performed)
            Climb(true);
        else if (context.canceled)
            Climb(false);
    }

    private void UpdateMovement()
    {
        bool isFacingLeft = Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x;
        bool isFacingRight = !isFacingLeft;
        bool isMovingOpposite = (isFacingLeft && _movementX > 0) || (isFacingRight && _movementX < 0);

        if (_currentWeaponType != WeaponType.Unarmed && isMovingOpposite)
        {
            _currentMoveSpeed = _movementConfig.BackwardsMoveSpeed;
            _currentActionState = _movementX != 0 ? EntityActionState.WalkingBackwards : EntityActionState.Idle;
        }
        else if (_isRunning)
        {
            _currentMoveSpeed = _movementConfig.RunningSpeed;
            _currentActionState = _movementX != 0 ? EntityActionState.Running : EntityActionState.Idle;
        }
        else if (_isCrouching)
        {
            _currentMoveSpeed = _movementConfig.CrouchingSpeed;
            _currentActionState = _movementX != 0 ? EntityActionState.CrouchingWalking : EntityActionState.CrouchingIdle;
        }
        else
        {
            _currentMoveSpeed = _movementConfig.WalkingSpeed;
            _currentActionState = _movementX != 0 ? EntityActionState.Walking : EntityActionState.Idle;
        }

        AnimationHandler($"{_currentActionState.ToString()}{_currentWeaponType.ToString()}"); // Combine together to a string
    }



    #endregion
    #region Ladder
    private void HandleLadderInteraction(InputAction.CallbackContext context)
    {
        _movementY = context.ReadValue<Vector2>().y;

        if (_currentLadder != null)
        {
            if (_currentLadder._ExitLadder && context.canceled) // Exit ladder
            {
                ExitLadder();
            }
            else if (_currentLadder._Interact)
            {
                if (context.performed) // Climb ladder
                {
                    EnterLadder();
                }
                else if (context.canceled) // Pause while on ladder
                {
                    PauseLadder();
                }
            }
        }
    }

    private void EnterLadder()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; // Freezes both Z and Y axis
        gameObject.layer = LayerMask.NameToLayer("Ignore Ground"); // Allows going through floors
        _currentLadder.CurrentlyUsingLadder(true);
    }

    private void ExitLadder()
    {
        _rb.gravityScale = 1; // Enable gavity
        _rb.velocity = new Vector2(0, 0); // Stops the player from moving forward
        _currentLadder.CurrentlyUsingLadder(false);
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void PauseLadder()
    {
        _rb.gravityScale = 0; // Disable gavity
        _rb.velocity = new Vector2(0, 0); // Stops the player from moving forward
        _currentLadder.CurrentlyUsingLadder(false);
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



    private void Flip()
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

    private void CheckWeaponType(WeaponType weaponType)
    {
        if (weaponType != WeaponType.Unarmed) // Can only flip if not armed. RotationManager will handle weapons
        {
            RotationManager.CanRotate = true;
        }
        else
        {
            RotationManager.CanRotate = false;
        }
        _currentWeaponType = weaponType;
        AnimationHandler($"{_currentActionState}{weaponType}"); // Combine the 2 strings together. The string name needs to be specific to the named animation
    }

    private void AnimationHandler(string state)
    {
        _animationController.PlayAnimation(state);
    }

    public string GetUniqueEntityId()
    {
        return GetInstanceID().ToString();
    }

    public WeaponType GetEquippedWeapon() 
    {
        return _currentWeaponType;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(_groundCheckPos.position, _groundCheckRadius);
    }
}
