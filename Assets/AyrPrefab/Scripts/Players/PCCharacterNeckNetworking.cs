using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;

public class PCCharacterNeckNetworking : FirstPersonPCBehavior
{
    void Update()
    {
        if (!networkObject.IsOwner)
        {
            transform.rotation = networkObject.rotation;
            return;
        }

        networkObject.rotation = transform.rotation;

        //Destroy this object if client owner has disconnected
        if (networkObject.Owner == null && NetworkManager.Instance.IsServer)
        {
            networkObject.Destroy();
        }
    }
}
