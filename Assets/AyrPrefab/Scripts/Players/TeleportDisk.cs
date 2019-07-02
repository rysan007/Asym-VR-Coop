using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Generated;

public class TeleportDisk : MonoBehaviour
{
    public GameObject teleportDiskPrefab;
    public GameObject playerHoldLocation;
    private FixedJoint m_Joint = null;
    private TeleportDiskBehavior TDbehavior;
    private GameObject teleportDisk;

    void Start()
    {
        m_Joint = GetComponentInChildren<FixedJoint>();
        //teleportDisk = Instantiate(teleportDiskPrefab);
        TDbehavior = NetworkManager.Instance.InstantiateTeleportDisk();
        teleportDisk = TDbehavior.GetComponent<Transform>().gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            teleportDisk.transform.position = playerHoldLocation.transform.position;
            Rigidbody diskRigidBody = teleportDisk.GetComponent<Rigidbody>();
            diskRigidBody.velocity = Vector3.zero;
            diskRigidBody.AddForce(playerHoldLocation.transform.forward * 10, ForceMode.Impulse);
        }
    }
}
