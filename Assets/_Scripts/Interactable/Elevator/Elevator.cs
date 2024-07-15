using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform _elevatorPlatform;
    [SerializeField] private Transform _floor1;
    [SerializeField] private Transform _floor2;
    public GameObject _FloorCanvas;
    [SerializeField] private float _speed = 1.5f; // Elevator speed. Change in inspector
    [SerializeField] private Floor _currentFloor; // Can be used in inspector to decide which floor to land at
    [SerializeField] private bool _isMoving = false; 
    public bool _Interact  { get; private set; }  // Used in TriggerElevator script to check when you can interact

    public enum Floor
    {
        Floor1,
        Floor2
    }

    private void Awake()
    {
         _elevatorPlatform.position = GetFloorPosition(_currentFloor);
    }

    IEnumerator MoveToFloor(Floor targetFloor)
    {
        _isMoving = true;
        Vector2 targetPosition = GetFloorPosition(targetFloor);

        while ((Vector2)_elevatorPlatform.position != targetPosition)
        {
            _elevatorPlatform.position = Vector2.MoveTowards(_elevatorPlatform.position, targetPosition, _speed * Time.deltaTime);
            yield return null;
        }

        _isMoving = false;
    }

     Vector2 GetFloorPosition(Floor floor)
     {
        switch (floor)
        {
            case Floor.Floor1:
                return _floor1.position;
            case Floor.Floor2:
                return _floor2.position;
            default:
                return _floor1.position;
        }
     }

    public void ShowFloorPanel()
    {
        if (_FloorCanvas != null && !_FloorCanvas.activeSelf && !_isMoving)
        {
            _FloorCanvas.SetActive(true);
        }
    }

     public void SetInteract(bool interact)
     {
        _Interact = interact;
     }


    private void OnDrawGizmos()
    {
        if(_elevatorPlatform != null && _floor1 != null && _floor2 != null)
        {
            Gizmos.DrawLine(_elevatorPlatform.position, _floor1.position);
            Gizmos.DrawLine(_elevatorPlatform.position, _floor2.position);
        }
    }

    private void RequestFloor(Floor targetFloor)
    {
        if (!_isMoving)
        {
            _currentFloor = targetFloor; // Set the new floor
            _FloorCanvas.SetActive(false); // Set floor panel to false
            StartCoroutine(MoveToFloor(_currentFloor)); // Begin the elevator movement
        }
    }

    public void Floor1Button() // Button
    {
          RequestFloor(Floor.Floor1);
    }

    public void Floor2Button() // Button
    {
          RequestFloor(Floor.Floor2);
    }
}


