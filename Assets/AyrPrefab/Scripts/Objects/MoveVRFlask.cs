using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;
using UnityEngine.XR;

public class MoveVRFlask : VRFlaskBehavior
{
    [SerializeField]
    bool hasOccupant = false;
    GameObject currentParent;

    private void Start()
    {
        if (XRDevice.isPresent == false && !networkObject.IsOwner)
        {
            networkObject.TakeOwnership();
        }
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

    public void SetFlaskParent(GameObject parent)
    {
        currentParent = parent;
        networkObject.SendRpc(RPC_DO_ACTION, Receivers.AllBuffered);   
    }

    public bool HasOccupant()
    {
        return hasOccupant;
    }

    public void AddOccupant()
    {
        print("add occupant");
        networkObject.SendRpc(RPC_ADD_OCCUPANT, Receivers.AllBuffered);
    }

    public void RemoveOccupant()
    {
        print("remove occupant");
        networkObject.SendRpc(RPC_REMOVE_OCCUPANT, Receivers.AllBuffered);

    }

    public override void DoAction(RpcArgs args)
    {
        if (currentParent == null)
        {
            print("flask parent: null");
            transform.SetParent(null);
        }
        else
        {
            print("flask parent: " + currentParent);
            transform.SetParent(currentParent.transform);
        }
    }

    public override void AddOccupant(RpcArgs args)
    {
        print("add occupant RPC");
        hasOccupant = true;
    }

    public override void RemoveOccupant(RpcArgs args)
    {
        print("remove occupant RPC");
        hasOccupant = false;
    }
} 
