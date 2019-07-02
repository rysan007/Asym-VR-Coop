using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHeadRotation : MonoBehaviour
{
    public GameObject head;
    float speed = 1.5f;
    float angleY;
    float maxTurnAngle = 90f;
    WizardCameraMovement wizardCameraMovement;

    private void Start()
    {
        wizardCameraMovement = transform.parent.GetComponentInChildren<WizardCameraMovement>();
    }

    void Update()
    {
        angleY = wizardCameraMovement.angleY;

        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, angleY, 0)) > maxTurnAngle)
        {
            float angle = Vector3.SignedAngle(transform.forward, Quaternion.Euler(0, angleY, 0)*Vector3.forward, Vector3.up);
            if(angle > 0)
            {
                transform.rotation = Quaternion.Euler(0f, angleY - maxTurnAngle, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, angleY + maxTurnAngle, 0f);
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angleY, 0), Time.deltaTime * speed);
        }
    }

}
