using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class NetworkGenericButton : GenericButtonBlockBehavior
{
    ButtonActivatable button;
    GenericButtonController animator;
    bool isPressed = false;

    void Start()
    {
        button = GetComponent<ButtonActivatable>();
        //isPressed = GetComponent<GenericButtonController>();
    }

    void Update()
    {
        if (isPressed)
        {
            networkObject.SendRpc(RPC_ACTIVATE_EFFECT, Receivers.All);
            isPressed = false;
        }
    }

    public void PressButton()
    {
        isPressed = true;
    }

    public override void activateEffect(RpcArgs args)
    {
        button.Action();
    }

}
