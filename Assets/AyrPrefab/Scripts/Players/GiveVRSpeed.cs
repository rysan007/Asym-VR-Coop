using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using UnityEngine;

public class GiveVRSpeed : MonoBehaviour
{
    SteamVR_Behaviour_Pose trackedObj;

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_Behaviour_Pose>();
    }

    public Vector3 GetVRSpeed()
    {
        return trackedObj.GetVelocity();
    }
}
