using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

[ExecuteInEditMode]
public class SlidingRail : MonoBehaviour
{
    [SerializeField]
    [Range(0f,1f)]
    private float slidingPosition;

    public bool returnsToStartPoint = false;
    [Range(0f, 1f)]
    public float returnSpeed = 0;

    Transform beginPoint;
    Transform endPoint;
    GameObject slidingObject;
    GameObject grabInteractable;
    //public Mesh slidingObjectMesh;
    SlideInteractable slideInteractable;
    float distanceStartToEnd;

    Transform slider;
    ParticleToTarget particle;


    void Start()
    {
        SetupPlatform();

        if(slidingObject.transform.localScale != Vector3.one)
        {
            Debug.LogError(gameObject + "'s platform is not properly scaled to 1!");
        }
    }

    void Update()
    {
        if (slideInteractable.IsGrabbed())
        {
            Vector3 nearestPoint = FindNearestPointOnLine(beginPoint.position, endPoint.position, grabInteractable.transform.position);
            float distanceBetweenStartAndObject = Vector3.Distance(beginPoint.position, nearestPoint);
            slidingPosition = distanceBetweenStartAndObject / distanceStartToEnd;
            slidingObject.transform.position = Vector3.Lerp(beginPoint.transform.position, endPoint.transform.position, slidingPosition);
            //particle.UpdateParticleTargetLocation(slider.position);
        }
        else
        {
            slidingObject.transform.position = Vector3.Lerp(beginPoint.transform.position, endPoint.transform.position, slidingPosition);
            grabInteractable.transform.position = slidingObject.transform.position;
            //particle.StopParticleEffects();
        }

        if (returnsToStartPoint && !slideInteractable.IsGrabbed())
        {
            if(slidingPosition > 0)
            {
                slidingPosition -= Time.deltaTime * returnSpeed;
            }
        }
    }

    private Vector3 FindNearestPointOnLine(Vector3 beginPoint, Vector3 endPoint, Vector3 grabPoint)
    {
        //Get heading
        Vector3 heading = (endPoint - beginPoint);
        float magnitudeMax = heading.magnitude;
        heading.Normalize();

        //Do projection from the point but clamp it
        Vector3 grabDistance = grabPoint - beginPoint;
        float dotP = Vector3.Dot(grabDistance, heading);
        dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
        return beginPoint + heading * dotP;
    }
    
    private void SetupPlatform()
    {
        beginPoint = transform.Find("Begin");
        endPoint = transform.Find("End");
        beginPoint.GetComponent<MeshRenderer>().enabled = false;
        endPoint.GetComponent<MeshRenderer>().enabled = false;
        slidingObject = transform.Find("SlidingObject").gameObject;
        grabInteractable = transform.Find("InteractableGrab").gameObject;
        grabInteractable.GetComponent<BoxCollider>().size = slidingObject.GetComponent<BoxCollider>().size;

        if (XRDevice.isPresent == false)
        {
            grabInteractable.GetComponent<MeshRenderer>().enabled = false;
        }
        slideInteractable = grabInteractable.GetComponent<SlideInteractable>();
        distanceStartToEnd = Vector3.Distance(beginPoint.position, endPoint.position);
    }

    //public void SetParticle(ParticleToTarget part)
    //{
    //    particle = part;
    //    particle.StartParticleEffects(slider.position);
    //}
}
