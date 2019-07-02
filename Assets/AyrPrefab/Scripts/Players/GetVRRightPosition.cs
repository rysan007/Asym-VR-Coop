using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVRRightPosition : MonoBehaviour
{
    GameObject rightController;
    Vector3 myLocation;
    Quaternion myRotation;
    public GameObject cameraRig;
    public Vector3 myScale;
    void Start()
    {
        rightController = GameObject.FindGameObjectWithTag("RightVRController");
        cameraRig = GameObject.FindGameObjectWithTag("CameraRig");
        if (rightController != null)
        {
            myLocation = rightController.transform.position;
            myRotation = rightController.transform.rotation;
            myScale = cameraRig.transform.localScale;
        }
        
    }

    void Update()
    {
        if (cameraRig == null)
        {
            cameraRig = GameObject.FindGameObjectWithTag("CameraRig");
        }
        if (rightController == null)
        {
            rightController = GameObject.FindGameObjectWithTag("RightVRController");
        }
        else
        {
            myLocation = rightController.transform.position;
            transform.position = myLocation;
            myRotation = rightController.transform.rotation;
            transform.rotation = myRotation;
            myScale = cameraRig.transform.localScale;
            transform.localScale = myScale/80;
        }
    }
}
