using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidLadder : LadderHandler
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_CanUseLadder)
        {
            _CanUseLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _CanUseLadder = false;
        }
    }
}
