using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardParentPlatform : MonoBehaviour
{
    WizardNetwork wizardNetwork;

    private void Awake()
    {
        
    }

    private void Start()
    {
        wizardNetwork = GetComponent<WizardNetwork>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            print("Collision with platform " +  collision.gameObject);
            if (IsCollisionOnPlatform(collision)) //If collision player is on top of collided platform
            {
                print("setting wizard parent with " + collision.gameObject);
                //Set parent
                wizardNetwork.ParentNearestRidable();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            //Unmatch Mouse turn Rotation
            //PCFirstPersonControls firstPersonPC = collision.gameObject.GetComponent<PCFirstPersonControls>();
            //Quaternion newRotation = Quaternion.Euler(0, firstPersonPC.GetAngleY(), 0) * transform.rotation;
            //firstPersonPC.SetAngleY(newRotation.eulerAngles.y);

            //Unset parent
            wizardNetwork.UnparentCharacter();
        }
    }

    private bool IsCollisionOnPlatform(Collision collision)
    {
        return true;


        ////All layers but layer 8 (PCCharacter) so it will not hit self
        //int layerMask = 1 << 8;
        //layerMask = ~layerMask;

        //RaycastHit hit;
        //if (Physics.SphereCast(collision.gameObject.GetComponent<CapsuleCollider>().bounds.center, collision.gameObject.GetComponent<CapsuleCollider>().radius * .9f, Vector3.down, out hit, .1f, layerMask))
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

    }
}
