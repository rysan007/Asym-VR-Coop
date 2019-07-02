using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVRHeadPosition : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject cameraRig;
    public Vector3 myScale;
    public Vector3 myLocation;
    public Quaternion myRotation;
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("VRCamera");
        cameraRig = GameObject.FindGameObjectWithTag("CameraRig");
        if (mainCam)
        {
            myLocation = mainCam.transform.position;
            myRotation = mainCam.transform.rotation;
        }
        if (cameraRig)
        {
            myScale = cameraRig.transform.localScale;
        }    
    }

    void Update()
    {
        if (mainCam == null)
        {
            mainCam = GameObject.FindGameObjectWithTag("VRCamera");
        }
        if(cameraRig == null)
        {
            cameraRig = GameObject.FindGameObjectWithTag("CameraRig");
        }

        if (mainCam)
        {
            myLocation = mainCam.transform.position;
            transform.position = myLocation;

            myRotation = mainCam.transform.rotation;
            transform.rotation = myRotation;
        }
        if (cameraRig)
        {
            myScale = cameraRig.transform.localScale;
            transform.localScale = myScale/33.333f;
        }
    }
}
