using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.UI;
using System.Collections;

public class FirstPersonPC : MonoBehaviour
{
    float speed = 5.0f;
    float rotateSpeed = 5.0f;
    Rigidbody rb;
    Camera cam;
    float angleX;
    [SerializeField]
    float angleY;
    bool jump;
    bool fastRun;
    float moveVert;
    float moveHorz;
    float turnX;
    float turnY;
    bool mouseDown;

    [SerializeField]
    bool isGrounded;

    bool canJumpFromSlope = true;
    public CapsuleCollider capCol;
    public LayerMask groundLayers;

    void Start()
    {
        //Get Components
        rb = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        cam = GetComponentInChildren<Camera>();
    }
    
    private void Update()
    {
        Movement();
        MouseLook();
    }

    //Network movement update
    private void FixedUpdate()
    {
        PhysicsMovement();
    }

    private void MouseLook()
    {
        if (mouseDown)
        {
            //look left/right
            transform.localRotation = Quaternion.Euler(0, angleY, 0);

            //look up/down
            cam.transform.localRotation = Quaternion.Euler(angleX, 0, 0);
        }
    }

    private void PhysicsMovement()
    {
        if (fastRun)
        {
            moveVert *= speed * 3;
            rb.MovePosition(rb.position + (transform.forward * moveVert) * Time.deltaTime);
            fastRun = false;
        }
        else
        {
            moveVert *= speed;
            rb.MovePosition(rb.position + (transform.forward * moveVert) * Time.deltaTime);
        }

        moveHorz *= speed;
        rb.MovePosition(rb.position + (transform.right * moveHorz) * Time.deltaTime);


        if (IsGrounded())// && canJumpFromSlope)
        {
            isGrounded = true;//does nothing
            if (jump)
            {
                rb.AddForce(transform.up * 10, ForceMode.VelocityChange);
            }
            jump = false;
        }
        else
        {
            isGrounded = false;//does nothing
        }
    }

    private void Movement()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                jump = true;
            }
        }

        //Run fast
        if (Input.GetKey(KeyCode.LeftShift))
        {
            fastRun = true;
        }

        //Run WASD
        moveVert = Input.GetAxis("Vertical");
        moveHorz = Input.GetAxis("Horizontal");

        //MouseLook
        if (Input.GetMouseButton(1))
        {
            //look left/right
            turnX = Input.GetAxis("Mouse X");
            angleY += turnX * 5;

            //look up/down
            turnY = Input.GetAxis("Mouse Y");
            turnY *= -1;
            angleX += turnY * 5;
            angleX = Mathf.Clamp(angleX, -85, 85);
            mouseDown = true;
        }
        else
        {
            mouseDown = false;
        }
    }

    private bool IsGrounded()
    {
        //All layers but layer 8 (PCCharacter)
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.SphereCast(GetComponent<CapsuleCollider>().bounds.center, GetComponent<CapsuleCollider>().radius * .9f, Vector3.down, out hit, .1f, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetAngleY(float newYAngle)
    {
        angleY = newYAngle;
    }

    public float GetAngleY()
    {
        return angleY;
    }

}