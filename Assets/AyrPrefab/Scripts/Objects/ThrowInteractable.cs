using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowInteractable : AyrPickupable
{
    Vector3[] frameVelocity = new Vector3[20];
    int frameVelocityIndex = 0;

    Vector3 velocity;
    Vector3 previousPosition;

    Vector3 angularVelocity;
    Quaternion previousRotation;

    Vector3 currentVelocity = Vector3.zero;

    private void Start()
    {
        isVRInteractionAllowed = true;
        isPCInteractionAllowed = true;
    }

    private void Update()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        currentVelocity = GetAverageVelocity(transform.position, previousPosition, out previousPosition);
        angularVelocity = GetAverageRotation(transform.rotation, previousRotation, out previousRotation);

        if (m_ActiveInteracter != null)
        {
            transform.position = m_ActiveInteracter.GetHoldLocation();
            transform.rotation = m_ActiveInteracter.GetTransform().rotation;
            rb.useGravity = false;
        }
    }

    public override void UseObject(Transform interacter)
    {
        //print("TriggerDownAction Throw Override default");
        m_ActiveInteracter = interacter.GetComponentInChildren<IInteracter>();
        GetComponent<IOwnership>().GetNetworkOwnership();

        if (m_ActiveInteracter.GetType() == typeof(AyrHand))
        {
            isVRHand = true;
            isPCHand = false;
        }
        else if (m_ActiveInteracter.GetType() == typeof(AyrReticule))
        {
            isVRHand = false;
            isPCHand = true;
        }
    }

    public override void UnuseObject(Transform interacter)
    {
        //print("TriggerUpAction Throw Override default");
        if (m_ActiveInteracter != null)
        {
            float currentScale = m_ActiveInteracter.GetScale();
            m_ActiveInteracter = null;
            rb.useGravity = true;
            rb.velocity = currentVelocity;
            print("throw velocity: " + rb.velocity);
            rb.angularVelocity = angularVelocity;
        }
    }

    public override void ForceUnuseObject(Transform interacter)
    {
        print("GripDownAction Throw Override default");
    }


    private Vector3 GetAverageVelocity(Vector3 currentPos, Vector3 previousPos, out Vector3 previousPosition)
    {
        velocity = (currentPos - previousPos) / Time.deltaTime;
        previousPosition = currentPos;
        frameVelocity[frameVelocityIndex] = velocity;
        frameVelocityIndex = (frameVelocityIndex >= frameVelocity.Length-1) ? frameVelocityIndex = 0 : frameVelocityIndex++;
        return velocity;//GetMeanVector(frameVelocity);
    }

    private Vector3 GetAverageRotation(Quaternion currentRot, Quaternion previousRot, out Quaternion previousRotation)
    {
        Quaternion deltaRotation = currentRot * Quaternion.Inverse(previousRot);
        previousRotation = currentRot;
        float angle = 0.0f;
        Vector3 axis = Vector3.zero;
        deltaRotation.ToAngleAxis(out angle, out axis);
        angle *= Mathf.Deg2Rad;
        return axis * angle * (1.0f / Time.deltaTime);
    }

    private Vector3 GetMeanVector(Vector3[] positions)
    {
        if (positions.Length == 0)
            return Vector3.zero;
        float x = 0f;
        float y = 0f;
        float z = 0f;
        foreach (Vector3 pos in positions)
        {
            x += pos.x;
            y += pos.y;
            z += pos.z;
        }
        return new Vector3(x / positions.Length, y / positions.Length, z / positions.Length);
    }

}
