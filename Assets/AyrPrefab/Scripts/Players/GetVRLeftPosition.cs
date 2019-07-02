using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVRLeftPosition : MonoBehaviour
{
    GameObject leftController;
    Vector3 myLocation;
    Quaternion myRotation;
    public GameObject cameraRig;
    public Vector3 myScale;
    void Start()
    {
        leftController = GameObject.FindGameObjectWithTag("LeftVRController");
        cameraRig = GameObject.FindGameObjectWithTag("CameraRig");
        if (leftController != null)
        {
            myLocation = leftController.transform.position;
            myRotation = leftController.transform.rotation;
            myScale = cameraRig.transform.localScale;
        }
        
    }

    void Update()
    {
        if (cameraRig == null)
        {
            cameraRig = GameObject.FindGameObjectWithTag("CameraRig");
        }
        if (leftController == null)
        {
            leftController = GameObject.FindGameObjectWithTag("LeftVRController");
        }
        else
        {
            myLocation = leftController.transform.position;
            transform.position = myLocation;
            myRotation = leftController.transform.rotation;
            transform.rotation = myRotation;
            myScale = cameraRig.transform.localScale;
            transform.localScale = myScale/80;
        }
        
    }
}
