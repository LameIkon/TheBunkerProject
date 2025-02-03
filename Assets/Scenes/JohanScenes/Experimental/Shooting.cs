using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MousePosition
{
    [Header("Gun properties")]
    [SerializeField] private bool _canFire;
    [SerializeField] private float _timeBetweenFiring;

    [Header("Bullet properties")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletTransform;
    [SerializeField] private float _bulletSpeed;

    private void Start()
    {
        _canFire = true;
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Input.GetMouseButton(0) && _canFire) 
        { 
            _canFire = false;
            BulletProperties();
           

            StartCoroutine(FireRate());
        }
    }

    private void BulletProperties()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _bulletTransform.position, Quaternion.identity);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - _bulletTransform.position;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction.x, direction.y).normalized * _bulletSpeed;


        float bulletRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the bullet
        bullet.transform.rotation = Quaternion.Euler(0, 0, bulletRotation + 90); // Adjust by 90 degrees if necessary

    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(_timeBetweenFiring);
        _canFire = true;
        Debug.Log("can fire again");
    }
}
