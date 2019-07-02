using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float jumpPower = 8;

    GameObject cameraObject;
    Rigidbody rb;
    CapsuleCollider col;
    WallGrab wallGrab;
    WizardInputs inputs;

    int layerMask;

    void Start()
    {
        inputs = GetComponent<WizardInputs>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        wallGrab = GetComponent<WallGrab>();
        cameraObject = transform.Find("CameraHolder").gameObject;

        //All layers but layer 8 (PCCharacter)
        layerMask = 1 << 8;
        layerMask = ~layerMask;
    }

    void Update()
    {
        WSADMovement();

        JumpMovement();
    }

    private void WSADMovement()
    {
        float moveVert = inputs.forwardBackMovement;
        float moveHorz = inputs.leftRightMovement;

        moveVert *= moveSpeed;
        Vector3 direction = cameraObject.transform.forward;
        Vector3 newDirection = new Vector3(direction.x, 0, direction.z);
        newDirection.Normalize();
        rb.MovePosition(rb.position + newDirection * moveVert * Time.deltaTime);

        moveHorz *= moveSpeed;
        rb.MovePosition(rb.position + (cameraObject.transform.right * moveHorz) * Time.deltaTime);
    }

    private void JumpMovement()
    {
        if (IsGrounded()) 
        {
            if (inputs.SpacePressed())
            {
                rb.AddForce(transform.up * jumpPower, ForceMode.VelocityChange);
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.SphereCast(col.bounds.center, col.radius * .9f, Vector3.down, out hit, .5f, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
