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
            collision.transform.SetParent(null); // Set player free from elevator
            _elevator.SetInteract(false);
            _elevator._FloorCanvas.SetActive(false);
        }       
    }
}
