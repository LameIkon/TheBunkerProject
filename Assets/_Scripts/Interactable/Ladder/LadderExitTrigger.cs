using UnityEngine;

public class LadderExitTrigger : MonoBehaviour
{
    [SerializeField] private Ladder _ladderHandler;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_ladderHandler._UsingLadder) // If already using ladder
            {
                _ladderHandler.SetExit(true);
                
            }
            else if (!_ladderHandler._UsingLadder) // If at ladder but havent used it yet
            {
                _ladderHandler.SetExit(false);
                _ladderHandler._HighlightEmission.Highlight();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Leaving ladder
        {
            _ladderHandler.SetExit(false);
            _ladderHandler._HighlightEmission.ReturnToOriginal();
        }
    }
}
