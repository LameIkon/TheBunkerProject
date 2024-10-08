using UnityEngine;

public class TopLadder : MonoBehaviour
{
    [SerializeField] private Ladder _ladder;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_ladder._UsingLadder)
            {
                _ladder.SetExit(true);
            }
            else if (!_ladder._UsingLadder)
            {
                _ladder.SetExit(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _ladder.SetExit(false);
        }
    }
}
