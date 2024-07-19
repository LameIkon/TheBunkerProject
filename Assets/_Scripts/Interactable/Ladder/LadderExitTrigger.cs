using UnityEngine;

public class LadderExitTrigger : MonoBehaviour
{
    [SerializeField] private Ladder _ladderHandler;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_ladderHandler._UsingLadder)
            {
                _ladderHandler.SetExit(true);
            }
            else if (!_ladderHandler._UsingLadder)
            {
                _ladderHandler.SetExit(false);
            }
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
