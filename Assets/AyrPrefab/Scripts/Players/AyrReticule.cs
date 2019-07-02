using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyrReticule : MonoBehaviour, IInteracter
{
    WizardInputs inputs;
    const float grabDistance = 5;
    public GameObject playerHoldLocation;

    public IInteractables m_HighlightedInteractable;
    public IInteractables m_CurrentInteractable = null;


    Camera cam;

    void Awake()
    {
        inputs = GetComponent<WizardInputs>();
        cam = GetComponentInChildren<Camera>();

    }

    void Update()
    {
        CheckCurrentReticuleHighlight();
        CheckForInputs();
    }

    private void CheckForInputs()
    {
        //Pickup/Use
        if (inputs.EKeyPressed())
        {
            if(m_HighlightedInteractable != null)
            {
                m_CurrentInteractable = m_HighlightedInteractable;
                m_CurrentInteractable.UseObject(transform);
            }
        }

        //Drop
        if (inputs.RKeyPressed())
        {
            if (m_CurrentInteractable == null)
                return;

            m_CurrentInteractable.UnuseObject(transform);
            m_CurrentInteractable = null;
        }
    }

    private void CheckCurrentReticuleHighlight()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            if(Vector3.Distance(transform.position, hit.transform.position) < grabDistance)
            {
                if(hit.transform.GetComponent<IInteractables>() != null)
                {
                    m_HighlightedInteractable = hit.transform.GetComponent<IInteractables>();
                }
            }
        }
        else
        {
            m_HighlightedInteractable = null;
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public float GetScale()
    {
        return 1;
    }

    public Vector3 GetHoldLocation()
    {
        return playerHoldLocation.transform.position;
    }

}
