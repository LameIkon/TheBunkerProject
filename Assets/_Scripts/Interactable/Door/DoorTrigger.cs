using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Door _door;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            if (_door == null) // check for the door script the first timer intering. Due to performance i made it to get found here instead of Awake
            {
                _door = GetComponentInParent<Door>();
            }
            _door.SetInteract(true);
            //_door._Renderer.material = _door._GreenEmission;
            _door._HighlightEmission.Highlight();

            StopCoroutine(_door._AutomaticDoorCloseCoroutine); // prevent door closing in your face
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Leaving Door
        {
            _door.SetInteract(false);
            //_door._Renderer.material = _door._RedEmission;
            _door._HighlightEmission.ReturnToOriginal();

            if (_door._IsOpen) // Close door after you
            {
                _door._AutomaticDoorCloseCoroutine = StartCoroutine(_door.DoorAutomaticClose());
            }
        }
    }
}
