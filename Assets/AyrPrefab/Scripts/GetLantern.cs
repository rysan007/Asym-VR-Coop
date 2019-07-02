using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLantern : MonoBehaviour
{
    bool holdLantern = false;
    GameObject gameLantern;
    Rigidbody lanternRB;
    Collider lanternCollider;
    VRFlask VRFlaskScript;
    MoveVRFlask moveVRFlask;
    public GameObject flaskBaseLocation;

    public GameObject cameraHolder;

    float throwForce = 5;

    void Start()
    {
        flaskBaseLocation = GameObject.Find("FlaskBaseLocation");
        gameLantern = GameObject.FindGameObjectWithTag("VRFlask");
        VRFlaskScript = gameLantern.GetComponent<VRFlask>();
        moveVRFlask = gameLantern.GetComponent<MoveVRFlask>();

        lanternRB = gameLantern.GetComponent<Rigidbody>();
        lanternCollider = gameLantern.GetComponent<BoxCollider>();
        lanternCollider.enabled = true;
        lanternCollider.isTrigger = true;
        cameraHolder = transform.parent.parent.parent.Find("CameraHolder").gameObject;
        if (holdLantern)
        {
            lanternRB.useGravity = false;
            lanternRB.isKinematic = true;
            lanternRB.velocity = Vector3.zero;
        }
        else
        {
            lanternRB.useGravity = true;
            lanternRB.isKinematic = false;
            lanternRB.velocity = Vector3.zero;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!holdLantern)
            {
                if (Vector3.Distance(transform.position, gameLantern.transform.position) < 2)
                {
                    if (moveVRFlask.HasOccupant())
                    {
                        GrabLantern();
                    }  
                    else
                    {
                        print("can't pick up lantern, no occupant");
                    }
                }           
            }
            else
            {
                print("toss lantern");
                TossLantern();
            }
        }

        if (holdLantern)
        {
            gameLantern.transform.position = transform.position;
        }

        //return if goes too far
        if(Vector3.Distance(gameLantern.transform.position,transform.position) > 100)
        {
            DropLantern();
            gameLantern.transform.position = flaskBaseLocation.transform.position;
            //GrabLantern();
        }

    }

    private void TossLantern()
    {
        holdLantern = false;
        VRFlaskScript.isHeld = false;
        gameLantern.transform.position = gameLantern.transform.position + cameraHolder.transform.forward * 1.5f;
        lanternCollider.isTrigger = false;
        lanternRB.useGravity = true;
        lanternRB.isKinematic = false;
        lanternRB.constraints = RigidbodyConstraints.None;
        lanternRB.velocity = Vector3.zero;
        lanternRB.AddForce(cameraHolder.transform.forward * throwForce, ForceMode.Impulse);
    }

    private void DropLantern()
    {
        holdLantern = false;
        VRFlaskScript.isHeld = false;
        lanternCollider.isTrigger = false;
        lanternRB.useGravity = true;
        lanternRB.isKinematic = false;
        lanternRB.constraints = RigidbodyConstraints.None;
        lanternRB.velocity = Vector3.zero;
    }

    private void GrabLantern()
    {
        holdLantern = true;
        VRFlaskScript.isHeld = true;
        lanternCollider.isTrigger = true;
        gameLantern.transform.rotation = Quaternion.identity;
        lanternRB.constraints = RigidbodyConstraints.FreezeRotation;
        lanternRB.useGravity = false;
        lanternRB.isKinematic = true;
        lanternRB.velocity = Vector3.zero;
    }
}
