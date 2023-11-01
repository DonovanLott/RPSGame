using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Place this on any gameObject to give it basic 4-directional movement
 * Made for testing purposes
 */
public class TestPlayer : MonoBehaviour
{
    public float speed;

    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
        
    }
}
