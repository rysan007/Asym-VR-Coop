using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCrystal : MonoBehaviour
{
    BugControlObject bugControlObject;
    GameObject VRFlask;
    Vector3 recallPoint;

    public bool teleportsFlask = false;
    
    void Start()
    {
        bugControlObject = GetComponent<BugControlObject>();
        VRFlask = GameObject.FindGameObjectWithTag("VRFlask");
        print(VRFlask);
        recallPoint = VRFlask.transform.Find("VRFlask Teleport Point").transform.position;
        print(recallPoint);
    }

    public void Activate()
    {
        if (teleportsFlask)
        {
            print("setting flask location to: " + recallPoint);
            VRFlask.transform.position = recallPoint;
        }
    }

    public void TeleportFlaskToRecallPoint()
    {
        VRFlask.transform.position = recallPoint;
    }
}
