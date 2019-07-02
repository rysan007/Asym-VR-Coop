using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBugPosition : MonoBehaviour
{
    GameObject realBug;

    void Start()
    {
        realBug = GameObject.FindGameObjectWithTag("Bug");
    }

    void Update()
    {
        if (realBug == null)
        {
            realBug = GameObject.FindGameObjectWithTag("Bug");
        }
        else
        {
            transform.position = realBug.transform.position;
            transform.rotation = realBug.transform.rotation;
        }
    }

}
