using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightRoom : MonoBehaviour
{
    private Light2D [] _lights;
    private Door[] _doors;
    private bool _lightsOn = false;
    private bool _insideRoom = false;

    private void Awake()
    {
      GetDoorsAndLights();
    }

    private void Update()
    {
        DetectLightsSwitch();
        TurnOnLights();
    }

    private void GetDoorsAndLights() //gets doors and lights components in children and stores them in seperate arrays.
    {
        if (_lights == null)
        {
            _lights = GetComponentsInChildren<Light2D>();
        }

        if (_doors == null)
        {
            _doors = GetComponentsInChildren<Door>();
        }

        if (_lights != null) //turns off lights from start.
        {
            for (int i = 0; i < _lights.Length; i++)
            {
                _lights[i].enabled = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _insideRoom = true; //checks if player is inside room
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _insideRoom = false; //checks if player leaves room
        }
    }

    private void DetectLightsSwitch()
    {
        
            if (_doors[0]._IsOpen || _doors[1]._IsOpen) //if one of the doors are opened lights are on.
            {
                _lightsOn = true;
            }

            else if (!_doors[0]._IsOpen && _insideRoom || !_doors[1]._IsOpen && _insideRoom) //if doors are closed but player is inside room, lights is on.
            {
                _lightsOn = true;
            }

            else if (!_doors[0]._IsOpen && !_insideRoom || !_doors[1]._IsOpen && !_insideRoom) //if doors are closed and player is not inside room, lights is off.
            {
                _lightsOn = false;
            }
        
    }

    private void TurnOnLights() //turns lights off/on depending on the bool _lightsOn.
    {
        if(_lightsOn)
        {
            for (int i = 0; i < _lights.Length; i++)
            {
                _lights[i].enabled = true;
            }
        }

        else if (!_lightsOn)
        {
            for (int i = 0; i < _lights.Length; i++)
            {
                _lights[i].enabled = false;
            }
        }
    }

}
