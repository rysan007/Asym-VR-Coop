using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class MovePlatform : InteractablePosRotBehavior
{
    public override void DoAction(RpcArgs args)
    {
        
    }

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
