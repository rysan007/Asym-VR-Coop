using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashObject : MonoBehaviour
{
    //Collider m_collider;
    public GameObject smashEffect;

    void Start()
    {
        //m_collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("RightVRController") || collision.gameObject.CompareTag("LeftVRController"))
        {
            Vector3 hitSpeed = collision.gameObject.GetComponent<GiveVRSpeed>().GetVRSpeed(); 
            if (hitSpeed.magnitude > 4)
            {
                print("magnitude hit: " + hitSpeed.magnitude + " explode at: " + transform.position);
                Instantiate(smashEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
                
        }

    }
}
