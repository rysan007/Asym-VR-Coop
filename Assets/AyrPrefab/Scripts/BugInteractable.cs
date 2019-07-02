using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class BugInteractable : ActivatableBehavior, IBugInteractable
{
    MeshRenderer mesh;
    public Material dissolveMaterial;
    public float destroyTime = 3;
    public float dissolveAmount;
    private float dissolveMin = -1;
    private float dissolveMax = 1;
    private float dissolveDifference;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        dissolveMaterial = GetComponent<MeshRenderer>().material;
        dissolveAmount = dissolveMin;
        dissolveDifference = dissolveMax - dissolveMin;
    }

    public void BugObjectActivate()
    {
        networkObject.SendRpc(RPC_ACTIVATE_OBJECT, Receivers.AllBuffered);
    }

    public void BugObjectDeactivate()
    {
        networkObject.SendRpc(RPC_DEACTIVATE_OBJECT, Receivers.AllBuffered);
    }

    public IEnumerator DissolveOverTime()
    {
        while (dissolveAmount < dissolveMax)
        {
            dissolveAmount += Time.deltaTime / dissolveDifference;
            dissolveMaterial.SetFloat("Vector1_28ACD76D", dissolveAmount);
            yield return null;
        }
    }

    private IEnumerator TimeoutAndDestroySelf(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }
    }

    public override void activateObject(RpcArgs args)
    {
        StartCoroutine(DissolveOverTime());
        StartCoroutine(TimeoutAndDestroySelf(destroyTime));
    }

    public override void deactivateObject(RpcArgs args)
    {
        
    }
}
