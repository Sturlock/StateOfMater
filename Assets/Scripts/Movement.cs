using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MatterState
{
    Solid, Liquid, Gas
}
public class Movement : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public LayerMask maskSolid;
    public LayerMask maskLiquid;
    public LayerMask maskGas;
    public MatterState state = MatterState.Solid;

    public Vector2 controlInput;
    public float jumpForce;
    public bool swim;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        if (!swim)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (state == MatterState.Gas && Input.GetKeyDown(KeyCode.Q))
            {
                state = MatterState.Solid;
            }
            else
            state++;   
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case MatterState.Solid:
                _rigidbody.AddForce(controlInput, ForceMode.VelocityChange);
                gameObject.layer = LayerMask.NameToLayer("Solid");
                controlInput.y = 0f;
                break;
            case MatterState.Liquid:
                _rigidbody.AddForce(controlInput, ForceMode.VelocityChange);
                gameObject.layer = LayerMask.NameToLayer("Liquid");
                break;
            case (MatterState.Gas):
                _rigidbody.AddForce(controlInput, ForceMode.VelocityChange);
                gameObject.layer = LayerMask.NameToLayer("Gas");
                break;
            default:
                _rigidbody.AddForce(controlInput, ForceMode.VelocityChange);
                gameObject.layer = maskSolid;
                break;
        }
    }

    private void GetInput()
    {
        switch (state)
        {
            case MatterState.Solid:
                controlInput.x = Input.GetAxisRaw("Horizontal");
                
                Jump();
                break;
                case MatterState.Liquid:
                if (swim)
                    controlInput = new Vector2(Input.GetAxisRaw("Horizontal"),
                            Input.GetAxisRaw("Vertical"));
                else
                controlInput = new Vector2(Input.GetAxisRaw("Horizontal"), -.1f);
                break;
                case (MatterState.Gas):
                controlInput = new Vector2(Input.GetAxisRaw("Horizontal"),
                                    Input.GetAxisRaw("Vertical") + .2f);
                break;
            default:
                controlInput.x = Input.GetAxisRaw("Horizontal");
                Jump();
                break;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controlInput.y = jumpForce;
            Debug.Log("[Movement] Jump");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == maskLiquid && state == MatterState.Liquid)
        {
            swim = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == maskLiquid && state == MatterState.Liquid)
        {
            swim = false;
        }
    }
}
