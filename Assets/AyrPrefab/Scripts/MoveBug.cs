using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class MoveBug : InteractablePosRotBehavior
{
    public GameObject deathParticle;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isFlying", true);
    }

    private void Update()
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

    public void StopAnimating()
    {
        networkObject.SendRpc(RPC_DO_ACTION, Receivers.All);
    }

    public void DestroyPuppet()
    {
       networkObject.Destroy();
    }

    private void OnDestroy()
    {
        Instantiate(deathParticle, transform).transform.SetParent(null);
    }

    public override void DoAction(RpcArgs args)
    {
        animator.SetBool("isFlying", false);
    }
}
