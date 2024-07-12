using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLadder : LadderHandler
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_ExitLadder)
        {
            _ExitLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _ExitLadder = false;
        }
    }
}
