using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInteractable : AyrPickupable
{
    private void Start()
    {
        isVRInteractionAllowed = true;
        isPCInteractionAllowed = false;
    }

    private void Update()
    {
        if (m_ActiveInteracter != null)
        {
            transform.position = m_ActiveInteracter.GetHoldLocation();
            transform.rotation = m_ActiveInteracter.GetTransform().rotation;
        }
    }

    public override void UseObject(Transform interacter)
    {
        m_ActiveInteracter = interacter.GetComponentInChildren<IInteracter>();

        if (m_ActiveInteracter.GetType() == typeof(AyrHand))
        {
            print("TriggerDownAction Slide interactable override, Getting ownership");
            transform.parent.GetComponentInChildren<IOwnership>().GetNetworkOwnership();
        }
        else
        {
            print("PC can't use");
            m_ActiveInteracter = null;
        }
        
    }

    public override void UnuseObject(Transform interacter)
    {
        print("TriggerUpAction Slide interactable override");
        m_ActiveInteracter = null;
    }

    public override void ForceUnuseObject(Transform interacter)
    {
        print("GripDownAction Slide interactable override");
    }

    public bool IsGrabbed()
    {
        bool grabbed;
        if(m_ActiveInteracter == null)
        {
            grabbed = false;
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            grabbed = true;
            SetScale();
        }
        return grabbed;
    }

    private void SetScale()
    {
        float currentScale = m_ActiveInteracter.GetScale();
        Vector3 playerScale = new Vector3(currentScale, currentScale, currentScale);
        transform.localScale = playerScale / 10f;
    }

}