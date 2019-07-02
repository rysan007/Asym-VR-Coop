using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.XR;

public class AyrActivatable : MonoBehaviour, IInteractables
{
    public Vector3 m_Offset = Vector3.zero;
    public bool takeOwnership = false;

    [HideInInspector]
    public AyrHand m_ActiveHand = null;

    [HideInInspector]
    public AyrReticule m_ActiveReticule = null;

    public virtual void Action()
    {
        print("Action() here");
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void UseObject(Transform interacter)
    {
        Action();
    }

    public void UnuseObject(Transform interacter)
    {
        throw new System.NotImplementedException();
    }

    public void ForceUnuseObject(Transform interacter)
    {
        throw new System.NotImplementedException();
    }
}
