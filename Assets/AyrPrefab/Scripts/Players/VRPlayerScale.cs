using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerScale : MonoBehaviour
{
    //GameObject parentCameraRig;
    GameObject VRCamera;

    float minimumSize = .05f;
    float maximumSize = 20f;
    float sizeChangeSpeed = 1f;

    [Range(.1f, 50)]
    public float VRPlayerCurrentScale = 1f;
    Vector3 currentScale = new Vector3(1, 1, 1);
    

    void Start()
    {
        //parentCameraRig = GameObject.FindGameObjectWithTag("CameraRig");
        VRCamera = GameObject.FindGameObjectWithTag("VRCamera");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(VRPlayerCurrentScale < maximumSize)
                VRPlayerCurrentScale += sizeChangeSpeed;
            ScaleVRPlayer();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (VRPlayerCurrentScale > minimumSize)
                VRPlayerCurrentScale -= sizeChangeSpeed;
            if (VRPlayerCurrentScale < minimumSize)
                VRPlayerCurrentScale = minimumSize;
            ScaleVRPlayer();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine("LerpToVRScale");
            StartCoroutine(LerpToVRScale(20));
        }
        
        //If there is a change in scale, apply
        if(currentScale.x != VRPlayerCurrentScale)
        {
            ScaleVRPlayer();
        }
    }

    private void ScaleVRPlayer()
    {
        currentScale = new Vector3(VRPlayerCurrentScale, VRPlayerCurrentScale, VRPlayerCurrentScale);
        Vector3 beforeScaleLocation = VRCamera.transform.position;
        transform.localScale = currentScale;
        Vector3 afterScaleLocation = VRCamera.transform.position;
        Vector3 difference = afterScaleLocation - beforeScaleLocation;
        transform.position = transform.position - new Vector3(difference.x, 0, difference.z);
    }

    public float GetCurrentScale()
    {
        return VRPlayerCurrentScale;
    }

    public void SetVRScale(float scale)
    {
        VRPlayerCurrentScale = scale;
        ScaleVRPlayer(); //This is called to prevent Update delay when manually teleporting character
    }

    private IEnumerator LerpToVRScale(float scale)
    {
        while (VRPlayerCurrentScale != scale)
        {
            VRPlayerCurrentScale = Mathf.Lerp(VRPlayerCurrentScale, scale, sizeChangeSpeed);
            yield return new WaitForSeconds(.1f);
        }               
    }
}
