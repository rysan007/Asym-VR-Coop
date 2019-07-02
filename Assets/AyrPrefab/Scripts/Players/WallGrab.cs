using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrab : MonoBehaviour
{
    WizardInputs inputs;
    Rigidbody rb;
    bool isWallGrabbing = false;
    GameObject wallRunParticle;
    ParticleToTarget particleToTarget;
    Collision currentCollisionTarget;
    Coroutine currentCoroutine;
    float wallStickPower = 10;
    float wallJumpPower = 5;

    Vector3 contactNormal;

    private void Start()
    {
        inputs = GetComponent<WizardInputs>();
        wallRunParticle = transform.Find("ParticleToTarget").gameObject;
        particleToTarget = wallRunParticle.GetComponent<ParticleToTarget>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("RunningWall"))
        {
            if (inputs.shiftKey)
            {
                currentCollisionTarget = collision;
                particleToTarget.StartParticleEffects(collision.contacts[0].point);
                isWallGrabbing = true;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                if(currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                }
                currentCoroutine = StartCoroutine(HoldWallForTime(3));
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("RunningWall"))
        {
            if (inputs.shiftKey && isWallGrabbing == true)
            {
                if (inputs.SpacePressed())
                {
                    StopWallRunning();
                    rb.AddForce(ContactNormal() * wallJumpPower + new Vector3(0, 4, 0), ForceMode.VelocityChange);
                }
                else
                {
                    contactNormal = collision.contacts[0].normal;
                    rb.MovePosition(rb.position - ContactNormal() * wallStickPower * Time.deltaTime);
                    particleToTarget.UpdateParticleTargetLocation(collision.contacts[0].point);
                }
            }
            else
            {
                StopWallRunning();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("RunningWall"))
        {
            StopWallRunning();
        }
    }

    IEnumerator HoldWallForTime(float timeToHold)
    {
        yield return new WaitForSeconds(timeToHold);
        StopWallRunning();
    }


    public bool IsWallGrabbing()
    {
        return isWallGrabbing;
    }

    public Vector3 ContactNormal()
    {
        return contactNormal;
    }

    private void StopWallRunning()
    {
        particleToTarget.StopParticleEffects();
        rb.useGravity = true;
        isWallGrabbing = false;
    }

}
