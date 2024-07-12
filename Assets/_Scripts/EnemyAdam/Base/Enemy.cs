using UnityEngine;

public class Enemy : MonoBehaviour, IDamageableTemp, IEnemyMoveable
{
    [field: SerializeField] public float _MaxHealth { get; set; } = 100f;
    public float _CurrentHealth { get; set; }
    public Rigidbody2D _RB { get; set; }
    public bool _IsFacingRight { get; set; }

    #region State Machine Variables

    public EnemyStateMachine _StateMachine { get; set; }
    public EnemyIdleState _IdleState { get; set; }
    public EnemyChaseState _ChaseState { get; set; }
    public EnemyAttackState _AttackState { get; set; }

    #endregion

    #region Idle Variables

    

    #endregion

    private void Awake()
    {
        _StateMachine = new EnemyStateMachine();
        _IdleState = new EnemyIdleState(this, _StateMachine);
        _ChaseState = new EnemyChaseState(this, _StateMachine);
        _AttackState = new EnemyAttackState(this, _StateMachine);
    }

    private void Start()
    {
        _CurrentHealth = _MaxHealth;
        _RB = GetComponent<Rigidbody2D>();
        _StateMachine.Initialize(_IdleState);
    }

    private void Update()
    {
        _StateMachine._CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        _StateMachine._CurrentEnemyState.PhysicsUpdate();
    }

    #region Health / Die Functions

    public void Damage(float damageAmount)
    {
        _CurrentHealth -= damageAmount;
        {
            if (_CurrentHealth <= 0f)
            {
                Die();
            }
        }
    }

    public void Die()
    {
    }

    #endregion


    #region Movement Functions

    public void MoveEnemy(Vector2 velocity)
    {
        _RB.velocity = velocity;
        CheckForLeftOrRightFacing(velocity);
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        // TODO: Enemy movement code
    }

    #endregion

    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        _StateMachine._CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        /*
         * WE CAN ADD MORE IF NECESSARY!
         */
        
        EnemyDamaged,
        PlayFootstepSound
    }

    #endregion
}