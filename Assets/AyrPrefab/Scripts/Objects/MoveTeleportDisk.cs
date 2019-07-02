using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class MoveTeleportDisk : TeleportDiskBehavior
{
    void Update()
    {
        try
        {
            if (!networkObject.IsOwner)
            {
                transform.position = networkObject.position;
                transform.rotation = networkObject.rotation;
                return;
            }
            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;
        }
        catch
        {

        }

    }
}
