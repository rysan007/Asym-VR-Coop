using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using System;

public class BugControlObject : ActivatableBehavior, IBugInteractable
{
    private GameObject BugCrystal;
    [SerializeField]
    private bool isActivated = false;
    TeleportCrystal teleportCrystal;

    private void Start()
    {
        teleportCrystal = GetComponent<TeleportCrystal>();
    }

    public void BugObjectActivate()
    {
        networkObject.SendRpc(RPC_ACTIVATE_OBJECT, Receivers.AllBuffered);
    }

    public void BugObjectDeactivate()
    {
        networkObject.SendRpc(RPC_DEACTIVATE_OBJECT, Receivers.AllBuffered);
    }

    public override void activateObject(RpcArgs args)
    {
        isActivated = true;
        teleportCrystal.Activate();
        print("Bug crystal activated, using TeleportCrystal script");
    }

    public override void deactivateObject(RpcArgs args)
    {
        isActivated = false;
        print("Bug crystal deactivated, using TeleportCrystal script");
    }

    public Vector3 GetTeleportPoint()
    {
        return transform.Find("TeleportPoint").position;
    }
}

/*
  public GameObject teleportAreaPrefab;
    private GameObject teleportAreaObject;
    public Vector3 teleportAreaSize = new Vector3(10,10,10);
    public float teleportScale = 1;
    public bool isActivated = false;
    public bool isAreaActivator = false;

    private void Start()
    {
        teleportAreaObject = Instantiate(teleportAreaPrefab, transform);
        teleportAreaObject.transform.localScale = teleportAreaSize;
        teleportAreaObject.SetActive(false);
    }

    public void BugObjectActivate()
    {
        networkObject.SendRpc(RPC_ACTIVATE_OBJECT, Receivers.AllBuffered);
    }

    public void BugObjectDeactivate()
    {
        networkObject.SendRpc(RPC_DEACTIVATE_OBJECT, Receivers.AllBuffered);
    }

    public override void activateObject(RpcArgs args)
    {
        if(isAreaActivator)
            teleportAreaObject.SetActive(true);
        isActivated = true;
    }

    public override void deactivateObject(RpcArgs args)
    {
        if(isAreaActivator)
            teleportAreaObject.SetActive(false);
        isActivated = false;
    }

    public Vector3 GetTeleportPoint()
    {
        return transform.Find("TeleportPoint").position;
    }
*/
