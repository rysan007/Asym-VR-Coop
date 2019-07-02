using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;

public class MoveVRHead : MoveVRHeadBehavior
{
    public GetVRHeadPosition headPuppet;

    private void Start()
    {
        headPuppet = GetComponent<GetVRHeadPosition>();
        //TODO: owner no longer works if not instantiating
        if (networkObject.IsOwner)
        {
            foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
            {
                mr.enabled = false;
            }
        }
    }
    void Update()
    {
        try
        {
            //Destroy object if owner disconnects
            if (NetworkManager.Instance.IsServer && XRDevice.isPresent == false)
            {
                networkObject.Owner.disconnected += DestroyOnDisconnect;
            }

            if (!networkObject.IsOwner)
            {
                transform.position = networkObject.position;
                transform.rotation = networkObject.rotation;
                transform.localScale = new Vector3(networkObject.scale, networkObject.scale, networkObject.scale);
                return;
            }
            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;
            networkObject.scale = transform.localScale.x;
        }
        catch
        {
            Debug.Log("Error in MoveHead");
        }
        
    }

    private void DestroyOnDisconnect(NetWorker sender)
    {
        print("destroying disconnected VR player head!");
        networkObject.Destroy();
    }
}
