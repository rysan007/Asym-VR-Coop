using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class MoveSphere : InteractablePosRotBehavior, IOwnership
{
    ThrowInteractable interactable;

    public override void DoAction(RpcArgs args)
    {
        
    }

    public void GetNetworkOwnership()
    {
        networkObject.TakeOwnership();
    }

    private void Start()
    {
        interactable = GetComponent<ThrowInteractable>();
    }
    void Update()
    {
        if(interactable == null)
        {
            print("problem here");
        }
        //if (interactable.IfTakeOwnership() && !networkObject.IsOwner)
        //{
        //    //print("taking ownership ");
        //    networkObject.TakeOwnership();
        //    interactable.GetOwnershipFalse();
        //}
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
