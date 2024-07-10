using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _bulletSpeed;   
    private Rigidbody2D _rb;

    [SerializeField] private BulletSO _bullet;
    

    private int _DELETETESTDAMAGE = 25;

    private void Awake()
    {
        _bulletSpeed = _bullet._BulletSpeed;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity =  transform.right * _bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            print("Enemy hit");
            enemy.TakeDamage(_DELETETESTDAMAGE);
            Destroy(gameObject);
        }
    }
}
