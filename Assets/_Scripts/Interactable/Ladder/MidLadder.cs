using UnityEngine;

public class MidLadder : MonoBehaviour
{
    [SerializeField] private LadderHandler _ladderHandler;

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            _ladderHandler.SetInteract(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _ladderHandler.SetInteract(false);
        }
    }
}
