using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration; // in unit / second^2
    
    private Rigidbody2D _rb;
    private Coroutine _moveRoutine;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Log(("TEST"));
    }

    void Update()
    {
    }

    public void OnMoveX(InputValue inputValue)
    {
        float valueX = inputValue.Get<float>();
        
        // Stop existing routine if exists
        if (_moveRoutine != null)
        {
            StopCoroutine(_moveRoutine);
        }
        
        if (valueX != 0)
        {
            _moveRoutine = StartCoroutine(Accelerate(valueX));
        }
        else
        {
            _moveRoutine = StartCoroutine(Decelerate());
        }
    }


    IEnumerator Accelerate(float direction)
    {
        float targetSpeed = Mathf.Sign(direction) * maxSpeed;

        while (!Mathf.Approximately(_rb.velocity.x, targetSpeed))
        {
            yield return null;

            float newX = _rb.velocity.x + acceleration * Time.deltaTime * Mathf.Sign(direction);
            newX = Mathf.Clamp(newX, -maxSpeed, maxSpeed);

            _rb.velocity = new Vector2(newX, _rb.velocity.y);
        }

        _rb.velocity = new Vector2(targetSpeed, _rb.velocity.y);
    }

    IEnumerator Decelerate()
    {
        while (Mathf.Abs(_rb.velocity.x) > 0.01f)
        {
            yield return null;

            float newX = _rb.velocity.x - acceleration * Time.deltaTime * Mathf.Sign(_rb.velocity.x);
            _rb.velocity = new Vector2(newX, _rb.velocity.y);
        }

        _rb.velocity = new Vector2(0f, _rb.velocity.y);
    }

}
