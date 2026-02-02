using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelTime; // in seconds
    
    private Rigidbody2D _rb;
    private int _horizontalInputScale;
    private Coroutine _moveRoutine;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Log(("TEST"));
        //_rb.velocity = new Vector2(5, 0);
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
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
    }


    IEnumerator Accelerate(float direction)
    {
        float startTime = Time.time;
        while (Mathf.Abs(_rb.velocity.x) < maxSpeed)
        {
            float timeInterpolate = (Time.time -  startTime) / accelTime;
            float velX = Mathf.Lerp(0, maxSpeed, timeInterpolate);
            _rb.velocity = new Vector2(direction * velX, _rb.velocity.y);
            yield return null;
        }
    }
    
}
