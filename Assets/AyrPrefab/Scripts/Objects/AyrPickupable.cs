using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
public class AyrPickupable : MonoBehaviour, IInteractables
{
    [SerializeField]
    bool takeOwnership = false;

    [SerializeField]
    public Vector3 m_Offset = Vector3.zero;

    protected Rigidbody rb;

    public IInteracter m_ActiveInteracter = null;
    protected bool isVRHand = false;
    protected bool isPCHand = false;

    public bool isVRInteractionAllowed = true;
    public bool isPCInteractionAllowed = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void UseObject(Transform interacter)
    {
        print("Default print UseObject (no override)");
    }

    public virtual void UnuseObject(Transform interacter)
    {
        print("Default print UnuseObject (no override)");
    }

    public virtual void ForceUnuseObject(Transform interacter)
    {
        print("Default print ForceUnuseObject (no override)");
    }

    public Transform GetTransform()
    {
        return transform;
    }

}
