using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCState : MonoBehaviour
{
    bool hasParent;
    GameObject parentObject;

    void Update()
    {
        if(transform.parent != null)
        {
            parentObject = transform.parent.gameObject;
        }
        else
        {
            parentObject = null;
        }
    }

    public GameObject GetParentObject()
    {
        return parentObject;
    }

}
