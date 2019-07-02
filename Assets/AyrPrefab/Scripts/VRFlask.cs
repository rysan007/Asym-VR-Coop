using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFlask : MonoBehaviour
{
    public GameObject teleportPointObject;
    public bool isHeld = false;
    private float energyAmount = 0;

    Vector3 teleportPoint;
    GameObject lanternLocation;
    MoveVRFlask moveVRFlask;

    void Start()
    {
        teleportPoint = teleportPointObject.transform.localPosition;
        moveVRFlask = GetComponent<MoveVRFlask>();
    }

    public Vector3 GetTeleportPoint()
    {
        return teleportPoint;
    }

    public void SetFlaskEnergy(float energy)
    {
        energyAmount = energy;
    }

    public float GetFlaskEnergy()
    {
        return energyAmount;
    }
}
