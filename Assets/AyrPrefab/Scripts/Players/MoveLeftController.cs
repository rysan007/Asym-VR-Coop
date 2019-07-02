using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;

public class MoveLeftController : MoveLeftControllerBehavior
{
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
            //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            //Debug.DrawRay(transform.position, forward, Color.red);
            networkObject.scale = transform.localScale.x;
        }
        catch
        {

        }
        
    }

    private void DestroyOnDisconnect(NetWorker sender)
    {
        print("destroying disconnected VR player left controller!");
        networkObject.Destroy();
    }

}
