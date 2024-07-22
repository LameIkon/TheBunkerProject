using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerElevator : MonoBehaviour
{
    [SerializeField] private Elevator _elevator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //_highlightScript.TriggerExit(gameObject);
            collision.transform.SetParent(this.transform); // Set player has child of elevator
            _elevator.SetInteract(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //_highlightScript.TriggerExit(gameObject);
            try
            {
            collision.transform.SetParent(null); // Set player free from elevator
            }
            catch (Exception ex)
            {
                Debug.LogError("Ignore this error. Failed to set parent to null: " + ex.Message);    
            }
            _elevator.SetInteract(false);
            _elevator._FloorCanvas.SetActive(false);
        }       
    }
}
