using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnArea : MonoBehaviour
{
    Vector3 returnLocation;

    void Start()
    {
        returnLocation = transform.parent.Find("ReturnLocation").position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("collision with " + other.transform.tag);
        if (other.transform.CompareTag("FirstPersonPC"))
        {
            other.transform.position = returnLocation;
        }
    }
}
