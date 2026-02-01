using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float maxHorizontalSpeed;
    
    private Rigidbody2D _rb;
    private int _horizontalInputScale;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontalInputScale = 0;

        if (Input.GetKey(KeyCode.A))
        {
            _horizontalInputScale -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _horizontalInputScale += 1;
        }
    }

    void FixedUpdate()
    {
        if (_horizontalInputScale != 0)
        {
            // Accelerate (horizontally)   
            float force = _rb.mass * acceleration;
            _rb.AddForce(Vector2.right * (_horizontalInputScale * force), ForceMode2D.Force);
        }
        else
        {
            // Decelerate (horizontally)
            float oppDirection = _rb.velocity.x > 0 ? -1 : 1;
            _rb.AddForce(Vector2.right * (oppDirection * acceleration), ForceMode2D.Force);
        }
        
        // Clamp horizontal speed
        _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed), _rb.velocity.y);
    }
}
