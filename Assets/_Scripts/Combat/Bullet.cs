using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _bulletSpeed;
    private Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity =  transform.right * _bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
