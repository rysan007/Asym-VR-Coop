using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AyrHand : MonoBehaviour, IInteracter
{
    public VRHandInputs inputs;
    GameObject platformGrabParticle;
    ParticleToTarget particleToTarget;

    public SteamVR_Action_Vibration m_TouchFeedback = null;
    //private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    IInteractables m_CurrentInteractable = null;
    public List<IInteractables> m_ContactInteractables = new List<IInteractables>();

    private VRPlayerScale scale;
    private Collider handCollider;

    void Awake()
    {
        inputs = GetComponent<VRHandInputs>();
        platformGrabParticle = transform.Find("ParticleToTarget").gameObject;
        particleToTarget = platformGrabParticle.GetComponent<ParticleToTarget>();

        m_Joint = GetComponent<FixedJoint>();
        scale = GetComponentInParent<VRPlayerScale>();

        handCollider = GetComponent<Collider>();
        handCollider.isTrigger = true;

        //Physics.IgnoreLayerCollision(12,11);
    }
    
    void Update()
    {
        CheckForInputs();
    }

    private void CheckForInputs()
    {
        if (inputs.TriggerDown())
        {
            if (m_ContactInteractables.Count != 0)
            {
                m_CurrentInteractable = GetNearestInteractable();
                UseInteractable();
            }
        }

        if (inputs.TriggerUp())
        {
            DropInteractable(); 
        }

        if (inputs.GripDown())
        {
            handCollider.isTrigger = false;
        }

        if (inputs.GripUp())
        {
            handCollider.isTrigger = true;
        }
    }

    //Pick up or use object
    private void UseInteractable()
    {
        if (m_CurrentInteractable != null)
        {
            m_CurrentInteractable.UseObject(transform);
        } 
    }

    private void DropInteractable()
    {
        if (m_CurrentInteractable == null)
            return;

        m_CurrentInteractable.UnuseObject(transform);
    }

    //Get list of all IInteractables in range and return the closest one
    private IInteractables GetNearestInteractable()
    {
        IInteractables nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (IInteractables interactable in m_ContactInteractables)
        {
            distance = (interactable.GetTransform().position - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }
        return nearest;
    }

    private void TouchFeedback(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        m_TouchFeedback.Execute(0, duration, frequency, amplitude, source);
    }

    //Gather all triggered IInteractables
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<IInteractables>() != null)
        {
            m_ContactInteractables.Add(col.gameObject.GetComponent<IInteractables>());
            print("trigger enter: " + col);
            TouchFeedback(1, 150, 75, inputs.ControllerPose().inputSource);
        }
    }

    //Remove untriggered IInteractables
    private void OnTriggerExit(Collider col)
    {
        
        if (col.gameObject.GetComponent<IInteractables>() != null)
        {
            print("trigger exit: " + col);
            m_ContactInteractables.Remove(col.gameObject.GetComponent<IInteractables>());
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public float GetScale()
    {
        return scale.GetCurrentScale();
    }

    public Vector3 GetHoldLocation()
    {
        return GetComponent<Collider>().transform.position + GetComponent<Collider>().transform.forward * .3f * scale.GetCurrentScale();
    }
}
