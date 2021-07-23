using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [SerializeField] private float carSpeed = 10f;
    
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
        Move(_moveDir, carSpeed);
    }
    
    private void Move(Vector3 dir, float speed)
    {
        Debug.Log(dir);
        //_rb.velocity = dir * speed;
        _rb.AddRelativeTorque();
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