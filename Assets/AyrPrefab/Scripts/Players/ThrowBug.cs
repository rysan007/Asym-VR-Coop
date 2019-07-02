using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBug : MonoBehaviour
{
    public GameObject bugPrefab;
    public GameObject bugPuppetPrefab;

    GameObject currentBug;
    GameObject currentBugPuppet;
    public GameObject holdPosition;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(currentBug == null)
            {
                CreateNewBug();
            } 
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Destroy(currentBug);
        }
    }

    private void CreateNewBug()
    {
        currentBug = Instantiate(bugPrefab, holdPosition.transform);
        currentBugPuppet = NetworkManager.Instance.InstantiateInteractablePosRot(3, holdPosition.transform.position, holdPosition.transform.rotation).gameObject;
        currentBug.GetComponent<BugActions>().SetPuppetReference(currentBugPuppet);

        currentBug.GetComponent<Rigidbody>().AddForce(holdPosition.transform.forward * 5, ForceMode.VelocityChange);
        currentBug.transform.SetParent(null);
    }
}
