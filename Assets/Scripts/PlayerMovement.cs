using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float maxHorizontalSpeed;
    
    private Rigidbody2D rb;
    private int horizontalInputScale;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInputScale = 0;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInputScale -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInputScale += 1;
        }
    }

    void FixedUpdate()
    {
        float force = rb.mass * acceleration;
        rb.AddForce(Vector2.right * (horizontalInputScale * force), ForceMode2D.Force);
        
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed), rb.velocity.y);
    }
}
