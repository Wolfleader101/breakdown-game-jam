using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [SerializeField] private float carSpeed = 10f;
    [SerializeField] private float maxSpeed = .05f;

    private Vector3 _moveDir = Vector2.zero;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (_rb.velocity.magnitude < maxSpeed)
        {
            Move(_moveDir, carSpeed);
        }
        else
        {
            var velocity = _rb.velocity;
            velocity = new Vector3(velocity.x, velocity.y, maxSpeed);
            _rb.velocity = velocity;
        }
    }

    private void Move(Vector3 dir, float speed)
    {
        _rb.AddRelativeForce(dir * speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            var val = context.ReadValue<Vector2>();
            _moveDir = new Vector3(val.x, 0, val.y);
        }
    }


    public void OnBrake(InputAction.CallbackContext context)
    {
    }
}