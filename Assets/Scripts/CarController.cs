using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField] private float forwardAccel = 20f;
    [SerializeField] private float reverseAccel = 10f;
    [SerializeField] private float turnStrength = 50f;
    [SerializeField] private float maxSpeed =  1000f;
    [SerializeField] private Rigidbody rb;

    private float _accelDir = 0f;
    private float _turnDir = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f,_turnDir * turnStrength * Time.deltaTime, 0f));
        transform.position = rb.transform.position;
        
      
    }

    private void FixedUpdate()
    {
        Move(_accelDir, forwardAccel);
        // if (rb.velocity.magnitude < maxSpeed)
        // {
        //     Move(_moveDir, forwardAccel);
        // }
        // else
        // {
        //     var velocity = rb.velocity;
        //     velocity = new Vector3(velocity.x, velocity.y, maxSpeed);
        //     rb.velocity = velocity;
        // }
    }

    private void Move(float accelDir, float speed)
    {
        var dir = transform.forward * accelDir;
        rb.AddForce(dir * (speed * 1000f));
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
       // if (context.started)
        //{
            _accelDir = context.ReadValue<float>();
        //}
    }
    public void OnTurn(InputAction.CallbackContext context)
    {
       // if (!context.started) return;
        var val = context.ReadValue<float>();
        _turnDir = val;
    }


    public void OnBrake(InputAction.CallbackContext context)
    {
    }
}