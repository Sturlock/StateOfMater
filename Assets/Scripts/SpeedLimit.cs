using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimit : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public float maxSpeed = 5f;//Replace with your max speed
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (_rigidbody.velocity.magnitude > maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * maxSpeed;
        }
    }
}
