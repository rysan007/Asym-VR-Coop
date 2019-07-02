using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCameraMovement : MonoBehaviour
{
    public float angleY;
    public float angleX;
    GameObject cam;
    float cameraZoomSpeed = 2f;
    bool leftShoulder = true;
    Vector3 leftShoulderLocation = new Vector3(-.5f, 0, -.5f);
    Vector3 rightShoulderLocation = new Vector3(.5f, 0, -.5f);
    Vector3 leftOrbitLocation = new Vector3(-.2f, .3f, -3);
    Vector3 rightOrbitLocation = new Vector3(.2f, .3f, -3);

    void Start()
    {
        cam = transform.Find("Camera").gameObject;
        StartCoroutine(MoveCameraTo(cam.transform.localPosition, leftOrbitLocation, .3f));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetCameraShoulderLocation();
        }
        if (Input.GetMouseButtonUp(1))
        {
            SetCameraOrbitLocation();
        }

        if (Input.GetMouseButton(1))
        {
            OverShoulderLook();
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                leftShoulder = !leftShoulder;
                SetCameraShoulderLocation();
            }
        }
        else
        {
            OrbitMouseLook();
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                leftShoulder = !leftShoulder;
                SetCameraOrbitLocation();
            }
        }
    }

    private void OverShoulderLook()
    {
        float turnX = Input.GetAxis("Mouse X");
        angleY += turnX * 5;

        float turnY = Input.GetAxis("Mouse Y");
        angleX += turnY * 5;

        if (angleX > 90)
        {
            angleX = 90;
        }
        else if (angleX < -90)
        {
            angleX = -90;
        }

        transform.rotation = Quaternion.Euler(-angleX, angleY, 0);
    }

    private void OrbitMouseLook()
    {
        //Camera movement
        float turnX = Input.GetAxis("Mouse X");
        angleY += turnX * 5;

        float turnY = Input.GetAxis("Mouse Y");
        angleX += turnY * 5;

        if(angleX > 90)
        {
            angleX = 90;
        }
        else if (angleX < -90)
        {
            angleX = -90;
        }

        transform.rotation = Quaternion.Euler(-angleX, angleY, 0);
    }

    private void SetCameraOrbitLocation()
    {
        if (leftShoulder)
        {
            StartCoroutine(MoveCameraTo(cam.transform.localPosition, leftOrbitLocation, .3f));
        }
        else
        {
            StartCoroutine(MoveCameraTo(cam.transform.localPosition, rightOrbitLocation, .3f));
        }
    }

    private void SetCameraShoulderLocation()
    {
        if (leftShoulder)
        {
            StartCoroutine(MoveCameraTo(cam.transform.localPosition, leftShoulderLocation, .3f));
        }
        else
        {
            StartCoroutine(MoveCameraTo(cam.transform.localPosition, rightShoulderLocation, .3f));
        }
        
    }

    private IEnumerator MoveCameraTo(Vector3 startLocation,Vector3 endLocation, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            cam.transform.localPosition = Vector3.Lerp(startLocation, endLocation, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
