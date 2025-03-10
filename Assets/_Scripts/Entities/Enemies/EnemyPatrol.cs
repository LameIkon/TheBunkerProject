using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")] [SerializeField]
    private Transform _leftEdge;

    [SerializeField] private Transform _rightEdge;

    [Space(5f)] [Header("Enemy")] [SerializeField]
    private Transform _enemy;

    [Space(5f)] [Header("Movement Parameters")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _chaseSpeed;

    private Vector3 _initialScale;
    private bool _movingLeft;

    [Space(5f)] [Header("Idle Behaviour")] [SerializeField]
    private float _idleDuration;
    private float _idleTimer;

    [Space(5f)] [Header("Chasing")]
    [SerializeField] private Transform _player; //put player object inside for monster to chase.
    [SerializeField] private int _maxChaseDist;
    [SerializeField] private int _minChaseDist;
    [SerializeField] private bool _isChasing = false;
    private bool _isFacingRight = true;
    private int _movementX;


    // [Header("Enemy Animator")]  This must be uncommented once we have implemented animations in the game
    // [SerializeField] private Animator _anim; This must be uncommented once we have implemented animations in the game

    private void Awake()
    {
        _initialScale = _enemy.localScale;
    }

    private void OnDisable()
    {
        //_anim.SetBool("walkingAnimation", false); This must be uncommented once we have implemented animations in the game
    }

    private void Update()
    {
        ChasePlayer();

        if(!_isChasing) 
        { 
            Patrol();
        }      
    }

    private void Patrol()
    {
        if (_movingLeft)
        {
            if (_enemy.position.x >= _leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (_enemy.position.x <= _rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        //_anim.SetBool("walkingAnimation", false); This must be uncommented once we have implemented animations in the game
        _idleTimer += Time.deltaTime;
        if (_idleTimer > _idleDuration)
        {
            _movingLeft = !_movingLeft;
        }
    }

    private void MoveInDirection(int direction)
    {
        _idleTimer = 0;
        //_anim.SetBool("walkingAnimation", true); This must be uncommented once we have implemented animations in the game
        Flip();
        _movementX = direction;
        Vector3 enemyPos = _enemy.position;         

            enemyPos = new Vector3(enemyPos.x + Time.deltaTime * direction * _moveSpeed, enemyPos.y, enemyPos.z);
            _enemy.position = enemyPos;
    }

    private void ChasePlayer()
    {       
        
        if (Vector3.Distance(transform.position, _player.position) <= _minChaseDist) //checks if player is inside a certain dist to chase.
        {
            MoveTowardsPlayer();
            Flip();           
            _isChasing = true;       
        }

        else if (Vector3.Distance(transform.position, _player.position) >= _maxChaseDist) //if player is outside max "range" for chasing. 
        {
            _isChasing = false;
            
        }
       
    }

    private void MoveTowardsPlayer()
    {
             
        Vector2 enemyPos = _enemy.position; //stores pos before we move
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _chaseSpeed * Time.deltaTime); //moves monster towards player.pos.
        Vector2 enemyPosLast = _enemy.position; //stores pos after we move, to compare and see if we go left or right.

        //used for flipping to check if we are moving left or right when chasing.
        if(enemyPosLast.x < enemyPos.x)
        {
            _movementX = -1;
        }

        else if (enemyPosLast.x > enemyPos.x)
        {
            _movementX = 1;
        }
    }


    private void Flip()
    {
        // _enemy.localScale = new Vector3(Mathf.Abs(_initialScale.x) * direction, _initialScale.y, _initialScale.z);

        if (_isFacingRight && _movementX < 0f || !_isFacingRight && _movementX > 0f)
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}

   
