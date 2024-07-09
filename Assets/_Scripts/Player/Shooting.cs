using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private GameObject _bulletPrefab;

    

    private void Start()
    {

    }


    public void Shot (InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Fire();
        }
    }

    public void Fire()
    {
        Instantiate(_bulletPrefab, _shootingPoint.position, transform.rotation);
    }
}
