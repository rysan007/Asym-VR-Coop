using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraXInput : MonoBehaviour {
    public float degrees = 2f;
    float raiseAmount = 3f;
    float smooth = 20.0f;

    void Update () {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse Y") > 0)
            {
                if(degrees >= 0)
                    degrees = degrees - raiseAmount;
            }
            else if (Input.GetAxis("Mouse Y") < 0)
            {
                if(degrees <= 50)
                    degrees = degrees + raiseAmount;
            }
            Quaternion target = Quaternion.Euler(degrees, 0, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);
        }
    }
}
