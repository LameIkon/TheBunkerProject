using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Gun properties")]
    [SerializeField] private bool _canFire;
    [SerializeField] private float _timeBetweenFiring;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _accuracy = 5f;

    [Header("References")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletTransform;
    private Camera _camera;


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
        // Instantiate the bullet at the gun's position
        GameObject bullet = Instantiate(_bulletPrefab, _bulletTransform.position, Quaternion.identity);

        Vector3 direction = -_bulletTransform.up; 

        // Randomize the angle offset for the bullet direction
        float randomAngleOffset = UnityEngine.Random.Range(-_accuracy, _accuracy);
        direction = Quaternion.Euler(0, 0, randomAngleOffset) * direction;

        // Get the Rigidbody2D and set velocity
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * _bulletSpeed;

        float bulletRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, bulletRotation);

        Destroy(bullet, 3f);
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(_timeBetweenFiring);
        _canFire = true;
        Debug.Log("can fire again");
    }
}
