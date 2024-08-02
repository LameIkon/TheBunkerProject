using UnityEngine;

public class LadderTrigger : MonoBehaviour
{
    [SerializeField] private Ladder _ladderHandler;

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            _ladderHandler.SetInteract(true);
            _ladderHandler._ShowOutlineMaterial.ChangeToHighlightMaterial();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _ladderHandler.SetInteract(false);
            _ladderHandler._ShowOutlineMaterial.ChangeToOriginalMaterial();
        }
    }
}
