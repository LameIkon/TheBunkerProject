using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float _damage;
    [SerializeField] private float _attackCoolDowm;
    [SerializeField] private float _range;
    [Space(5f)]
    
    [Header("Collider Parameters")]
    [SerializeField] private float _colliderDistance;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;
    [Space(5f)]
    
    [Header("Player Layer")]
    [SerializeField] private LayerMask _playerLayer;
    
    private Health _playerHealth;
    private Animator _anim;
    private EnemyPatrol _enemyPatrol;
    float _coolDownTimer = Mathf.Infinity;

    private void Awake()
    {
        // _anim = GetComponent<Animator>(); This must be uncommented once we have implemented animations in the game
        _enemyPatrol = GetComponent<EnemyPatrol>();
    }
    
    private void Update()
    {
        _coolDownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (_coolDownTimer >= _attackCoolDowm)
            {
                _coolDownTimer = 0;
                // _anim.SetTrigger(attackAnimation); This must be uncommented once we have implemented animations in the game
            }
        }

        if (_enemyPatrol != null)
        {
            _enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        var bounds = _capsuleCollider.bounds;
        var trans = transform;
        RaycastHit2D hit = Physics2D.BoxCast(bounds.center + trans.right * (_range * trans.localScale.x * _colliderDistance), 
            new Vector3(bounds.size.x * _range, bounds.size.y, bounds.size.z), 0, Vector2.left, 0, _playerLayer);

        if (hit.collider != null)
        {
            _playerHealth = hit.transform.GetComponent<Health>();
        }
        
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var bounds = _capsuleCollider.bounds;
        var trans = transform;
        Gizmos.DrawWireCube(bounds.center + trans.right * _range * trans.localScale.x * _colliderDistance, 
            new Vector3(bounds.size.x * _range, bounds.size.y, bounds.size.z));
    }

    private void DamagePlayer() // Needs to be set on the attack animation
    {
        if (PlayerInSight())
        {
            _playerHealth.TakeDamage(_damage);
        }
    }
    
}