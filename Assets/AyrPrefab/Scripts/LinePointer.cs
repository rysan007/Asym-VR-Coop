using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePointer : MonoBehaviour
{
    LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Linecast(transform.position + transform.forward * .5f, transform.forward, out hit))
        {
            if (hit.collider)
            {
                line.SetPosition(1, hit.point);
            }
            else
            {
                line.SetPosition(1, transform.forward);
            }
        }
    }
}
