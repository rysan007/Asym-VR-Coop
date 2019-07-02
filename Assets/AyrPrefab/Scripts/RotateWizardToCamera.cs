using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWizardToCamera : MonoBehaviour
{
    GameObject cameraHolder;
    public GameObject head;

    void Start()
    {
        cameraHolder = transform.parent.Find("CameraHolder").gameObject;
    }

    
    void Update()
    {
        float xRotation = cameraHolder.transform.eulerAngles.x;
        float yRotation = cameraHolder.transform.eulerAngles.y;
        head.transform.eulerAngles = new Vector3(xRotation, yRotation, transform.eulerAngles.z);

        //transform.rotation = cameraHolder.transform.rotation;
    }
}
