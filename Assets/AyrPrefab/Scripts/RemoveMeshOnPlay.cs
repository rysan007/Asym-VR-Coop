using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveMeshOnPlay : MonoBehaviour
{
    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
