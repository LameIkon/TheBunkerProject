using UnityEngine;

public class BotLadder : MonoBehaviour
{
    [SerializeField] private LadderHandler _ladderHandler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _ladderHandler._UsingLadder)
        {
             _ladderHandler.SetExit(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _ladderHandler.SetExit(false);
        }
    }
}
