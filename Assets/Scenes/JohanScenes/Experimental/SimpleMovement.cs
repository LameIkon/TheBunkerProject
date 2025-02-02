using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private float SpeedX, SpeedY;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        SpeedX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        SpeedY = Input.GetAxisRaw("Vertical") * movementSpeed;
        rb.velocity = new Vector2 (SpeedX, SpeedY);
    }
}
