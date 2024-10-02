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

    private void GetDoorsAndLights()
    {
        if (_lights == null)
        {
            _lights = GetComponentsInChildren<Light2D>();
        }

        if (_doors == null)
        {
            _doors = GetComponentsInChildren<Door>();
        }

        if (_lights != null)
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
            //for (int i = 0; i < _lights.Length; i++)
            //{
            //    _lights[i].enabled = true;
            //}
            _insideRoom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //for (int i = 0; i < _lights.Length; i++)
            //{
            //    _lights[i].enabled = false;
            //}
            _insideRoom = false;
        }
    }

    private void DetectLightsSwitch()
    {
        
            if (_doors[0]._IsOpen || _doors[1]._IsOpen)
            {
                _lightsOn = true;
            }

            else if (!_doors[0]._IsOpen && _insideRoom || !_doors[1]._IsOpen && _insideRoom)
            {
                _lightsOn = true;
            }

            else if (!_doors[0]._IsOpen && !_insideRoom || !_doors[1]._IsOpen && !_insideRoom)
            {
                _lightsOn = false;
            }
        
    }

    private void TurnOnLights()
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
