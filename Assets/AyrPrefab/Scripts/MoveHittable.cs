using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class MoveHittable : InteractablePosRotBehavior
{
    public override void DoAction(RpcArgs args)
    {

    }

    void Update()
    {
        //if (/*interactable.IfTakeOwnership() && */!networkObject.IsOwner)
        //{
        //    networkObject.TakeOwnership();
        //    //interactable.SetTakeOwnership(false);
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
