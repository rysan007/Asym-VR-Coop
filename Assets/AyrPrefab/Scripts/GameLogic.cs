using UnityEngine;
using UnityEngine.XR;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{
    //[SerializeField]
    //private GameObject VRplayer;
    //[SerializeField]
    //private GameObject PCPlayer;
    public bool lockCursor = false;
    [SerializeField]
    private GameObject PCReticle;
    [SerializeField]
    private GameObject PCUI;

    GameObject VRStart;
    GameObject NonVRStart;

    private void Start()
    {
        SetStartingLocations();
        CreatePlayers();

        //Server spawns  (separate object)
        //if (NetworkManager.Instance.IsServer)
        //{
        //    List<Vector3> basicInteractableLocations = new List<Vector3>();
        //    basicInteractableLocations.Add(NonVRStart.transform.position + new Vector3(3f, 0, -2f));
        //    basicInteractableLocations.Add(NonVRStart.transform.position + new Vector3(3f, 0, 2f));
        //    basicInteractableLocations.Add(NonVRStart.transform.position + new Vector3(-2f, 0, 4f));

        //    int i = 0;
        //    foreach (Vector3 ob in basicInteractableLocations)
        //    {
        //        NetworkManager.Instance.InstantiateInteractablePosRot(i, ob);
        //        i++;
        //    }
        //}
    }

    private void SetStartingLocations()
    {
        VRStart = GameObject.Find("VRStart");
        NonVRStart = GameObject.Find("NonVRStart");
        if (!VRStart || !NonVRStart)
        {
            Debug.Log("ERROR: Player start locations not found.");
            Application.Quit();
        }
    }

    private void CreatePlayers()
    {
        //try
        //{
            //TODO: need more confirmation that player is VR enabled, this still triggers if VR device is on, but trying to play PC
            if (XRDevice.isPresent == true)
            {
                //Find and destroy duplicate


                NetworkManager.Instance.InstantiateVRPlayer(0,VRStart.transform.position, Quaternion.identity, true);
            }
            else
            {
                //Find and destroy duplicate


                if (lockCursor)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                
                Instantiate(PCReticle);
                Instantiate(PCUI);

                NetworkManager.Instance.InstantiateWizard(0, NonVRStart.transform.position, Quaternion.identity,true);
            }
   // }
        //catch
        //{
        //    Debug.Log("Error initial spawn");
        //}
    }

}