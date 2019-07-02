using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class MoveCapsule : InteractablePosRotBehavior, IOwnership
{
    ThrowInteractable interactable;

    public override void DoAction(RpcArgs args)
    {
        
    }

    public void GetNetworkOwnership()
    {
        networkObject.TakeOwnership();
        //if (interactable.IfTakeOwnership() && !networkObject.IsOwner)
        //{
        //    networkObject.TakeOwnership();
        //    interactable.GetOwnershipFalse();
        //}
    }

    private void Start()
    {
       interactable  = GetComponent<ThrowInteractable>();
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
