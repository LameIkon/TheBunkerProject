using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _bulletSpeed;   
    private Rigidbody2D _rb;

    [SerializeField] private BulletSO _bullet;

    private void Awake()
    {
        _bulletSpeed = _bullet._BulletSpeed;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity =  transform.right * _bulletSpeed;
    }   
}
