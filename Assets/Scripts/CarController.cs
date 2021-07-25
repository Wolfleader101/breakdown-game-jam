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
    [SerializeField] private float maxSpeed = 1000f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform rayPos;
    [SerializeField] private float rayLength = 1f;
    [SerializeField] private float airDrag = 0.1f;
    [SerializeField] private float downwardsForce = 15f;

    private float _accelDir = 0f;
    private float _turnDir = 0f;

    private float _defaultDrag;
    private bool _isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        rb.transform.parent = null;
        _defaultDrag = rb.drag;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGrounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
                                                  new Vector3(0f, _turnDir * turnStrength * Time.deltaTime * _accelDir,
                                                      0f));
        }

        transform.position = rb.transform.position;
    }

    private void FixedUpdate()
    {
        _isGrounded = false;
        RaycastHit hit;
        if (Physics.Raycast(rayPos.position, -transform.up, out hit, rayLength, groundMask))
        {
            Debug.DrawRay(rayPos.position, -transform.up * rayLength, Color.yellow);
            _isGrounded = true;
        }

        if (_isGrounded)
        {
            rb.drag = _defaultDrag;
            if (_accelDir > 0)
            {
                Move(_accelDir, forwardAccel);
            }
            else if (_accelDir < 0)
            {
                Move(_accelDir, reverseAccel);
            }
        }
        else
        {
            rb.drag = airDrag;
            rb.AddForce(Vector3.down * (downwardsForce * 100));
        }
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