using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Door _door;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            if (_door == null) // check for the door script the first timer intering. Due to performance i made it to get found here instead of Awake
            {
                _door = GetComponentInParent<Door>();
            }
            _door.SetInteract(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Leaving Door
        {
            _door.SetInteract(false);
        }
    }
}
