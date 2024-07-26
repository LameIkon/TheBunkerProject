using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")] [SerializeField]
    private Transform _leftEdge;

    [SerializeField] private Transform _rightEdge;

    [Space(5f)] [Header("Enemy")] [SerializeField]
    private Transform _enemy;

    [Space(5f)] [Header("Movement Parameters")] [SerializeField]
    private float _speed;

    private Vector3 _initialScale;
    private bool _movingLeft;

    [Space(5f)] [Header("Idle Behaviour")] [SerializeField]
    private float _idleDuration;

    private float _idleTimer;

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
        Flip(direction);

        Vector3 enemyPos = _enemy.position;
        enemyPos = new Vector3(enemyPos.x + Time.deltaTime * direction * _speed, enemyPos.y, enemyPos.z);
        _enemy.position = enemyPos;
    }

    private void Flip(int direction)
    {
        _enemy.localScale = new Vector3(Mathf.Abs(_initialScale.x) * direction, _initialScale.y, _initialScale.z);
    }
}