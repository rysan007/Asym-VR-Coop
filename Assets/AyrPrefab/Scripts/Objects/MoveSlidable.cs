using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;
using UnityEngine.XR;

public class MoveSlidable : InteractableSliderBehavior, IOwnership
{
    SlideInteractable interactable;

    public void GetNetworkOwnership()
    {
        networkObject.TakeOwnership();
    }

    private void Start()
    {
        interactable = transform.parent.gameObject.GetComponentInChildren<SlideInteractable>();
        if(interactable == null)
        {
            print("Error in MoveSlidable");
        }
    }

    void Update()
    {
        ////Always give VR ownership
        //if (XRDevice.isPresent && !networkObject.IsOwner)
        //{
        //    GetNetworkOwnership();
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
