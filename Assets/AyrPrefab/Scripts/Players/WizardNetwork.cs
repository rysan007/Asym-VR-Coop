using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.XR;

public class WizardNetwork : WizardBehavior
{
    GameObject[] ridables;
    public bool isRidingParentObject = false;
    float characterHeight = 1;

    private void Start()
    {
        ridables = GameObject.FindGameObjectsWithTag("Platform");
        RemoveUnnecessaryComponents();
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
                if (isRidingParentObject)
                {
                    transform.position = transform.parent.transform.position + new Vector3(networkObject.localPosition.x, characterHeight, networkObject.localPosition.z);
                    transform.rotation = networkObject.rotation;
                    return;
                }
                else
                {
                    transform.position = networkObject.position;
                    transform.rotation = networkObject.rotation;
                    return;
                }
                
            }
            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;
            networkObject.localPosition = transform.localPosition;
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

    private void RemoveUnnecessaryComponents()
    {
        if (networkObject.IsOwner)
        {
            //foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
            //{
            //    mr.enabled = false;
            //}
        }
        else
        {
            foreach (FixedJoint fg in GetComponentsInChildren<FixedJoint>())
            {
                Destroy(fg);
            }

            foreach (Rigidbody rig in GetComponentsInChildren<Rigidbody>())
            {
                Destroy(rig);
            }

            Destroy(GetComponent<WizardMovement>());
            Destroy(GetComponent<WallGrab>());
            Destroy(GetComponent<WizardInputs>());
            Destroy(GetComponent<ThrowBug>());
            Destroy(GetComponent<AyrReticule>());
            Destroy(GetComponentInChildren<WizardCameraMovement>());
            Destroy(GetComponentInChildren<GetLantern>());
            Destroy(GetComponentInChildren<Camera>());
            
        }
    }

    public void ParentNearestRidable()
    {        
        networkObject.SendRpc(RPC_WIZARD_PARENT, Receivers.All);
    }

    public void UnparentCharacter()
    {
        networkObject.SendRpc(RPC_WIZARD_UNPARENT, Receivers.All);
    }

    public override void WizardParent(RpcArgs args)
    {
        isRidingParentObject = true;
        GameObject nearestRidable = null;
        float nearestDistance = Mathf.Infinity;
        foreach (GameObject ob in ridables)
        {
            float distance = Vector3.Distance(transform.position, ob.transform.position);
            if (distance < nearestDistance)
            {
                nearestRidable = ob;
                nearestDistance = distance;
            }
        }
        if(nearestRidable == null)
        {
            transform.SetParent(null);
        }
        else
        {
            transform.SetParent(nearestRidable.transform);
        }
        
    }

    public override void WizardUnparent(RpcArgs args)
    {
        isRidingParentObject = false;
        transform.SetParent(null);
    }
}
