using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;
using System;


public class NetworkVRPlayer : VRPlayerBehavior
{
    GameObject VRFlask;
    Transform VRFlaskTeleportPoint;
    GameObject VRHead;
    GameObject VRLeftHand;
    GameObject VRRightHand;
    Camera VRCamera;
    float characterHeight = .05f;

    public bool isRidingParentObject = false;

    private void Awake()
    {
        
    }

    void Start()
    {
        VRFlask = GameObject.FindGameObjectWithTag("VRFlask");
        VRFlaskTeleportPoint = VRFlask.transform.Find("VRFlask Teleport Point").transform;
        VRHead = GameObject.FindGameObjectWithTag("VRCamera");
        VRLeftHand = GameObject.FindGameObjectWithTag("LeftVRController");
        VRRightHand = GameObject.FindGameObjectWithTag("RightVRController");

        if (XRDevice.isPresent == false)
        {
            //Remove unnecessary non VR stuff here
            GetComponent<SteamVR_PlayArea>().enabled = false;
            GetComponent<VRPlayerScale>().enabled = false;

            GetComponentInChildren<SteamVR_Behaviour_Pose>().enabled = false;
            GetComponentInChildren<AyrHand>().enabled = false;
            GetComponentInChildren<AyrTeleporter>().enabled = false;
            GetComponentInChildren<GiveVRSpeed>().enabled = false;
            GetComponentInChildren<SteamVR_Fade>().enabled = false;
            GetComponentInChildren<SteamVR_Camera>().enabled = false;
            VRCamera = GetComponentInChildren<Camera>();
            VRCamera.enabled = false;
        }
        else
        {
            //transform.Find("HeadModel").gameObject.SetActive(false);
        }
    }

    void Update()
    {
        //networkObject.Networker.playerDisconnected += DestroyObject;
        

        if (!networkObject.IsOwner && XRDevice.isPresent == false)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                VRCamera.enabled = !VRCamera.enabled;
            }

            if (isRidingParentObject)
            {
                float scale = networkObject.scale;
                //transform.position = transform.parent.transform.position + new Vector3(networkObject.localPosition.x, characterHeight, networkObject.localPosition.z);
                VRHead.transform.position = transform.parent.transform.position + new Vector3(networkObject.headLocalPosition.x * scale, networkObject.headLocalPosition.y * scale, networkObject.headLocalPosition.z * scale);
                VRLeftHand.transform.position = transform.parent.transform.position + new Vector3(networkObject.leftHandLocalPosition.x * scale, networkObject.headLocalPosition.y * scale, networkObject.leftHandLocalPosition.z * scale);
                VRRightHand.transform.position = transform.parent.transform.position + new Vector3(networkObject.rightHandLocalPosition.x * scale, networkObject.headLocalPosition.y * scale, networkObject.rightHandLocalPosition.z * scale);

                VRHead.transform.rotation = networkObject.headRotation;
                VRHead.transform.localScale = new Vector3(scale, scale, scale);
                VRLeftHand.transform.rotation = networkObject.leftHandRotation;
                VRLeftHand.transform.localScale = new Vector3(scale, scale, scale);
                VRRightHand.transform.rotation = networkObject.rightHandRotation;
                VRRightHand.transform.localScale = new Vector3(scale, scale, scale);

                return;
            }
            else
            {
                VRHead.transform.position = networkObject.headPosition;
                VRLeftHand.transform.position = networkObject.leftHandPosition;
                VRRightHand.transform.position = networkObject.rightHandPosition;

                VRHead.transform.rotation = networkObject.headRotation;
                VRHead.transform.localScale = new Vector3(networkObject.scale, networkObject.scale, networkObject.scale);
                VRLeftHand.transform.rotation = networkObject.leftHandRotation;
                VRLeftHand.transform.localScale = new Vector3(networkObject.scale, networkObject.scale, networkObject.scale);
                VRRightHand.transform.rotation = networkObject.rightHandRotation;
                VRRightHand.transform.localScale = new Vector3(networkObject.scale, networkObject.scale, networkObject.scale);

                return;
            }
        }


        networkObject.headLocalPosition = VRHead.transform.localPosition;
        networkObject.leftHandLocalPosition = VRLeftHand.transform.localPosition;
        networkObject.rightHandLocalPosition = VRRightHand.transform.localPosition;


        networkObject.headPosition = VRHead.transform.position;
        networkObject.headRotation = VRHead.transform.rotation;
        
        networkObject.leftHandPosition = VRLeftHand.transform.position;
        networkObject.leftHandRotation = VRLeftHand.transform.rotation;

        networkObject.rightHandPosition = VRRightHand.transform.position;
        networkObject.rightHandRotation = VRRightHand.transform.rotation;

        networkObject.scale = transform.localScale.x;

    }

    public void SetFlaskParent()
    {
        networkObject.SendRpc(RPC_FLASK_SET_PARENT, Receivers.All);
    }

    public void UnsetFlaskParent()
    {
       networkObject.SendRpc(RPC_FLASK_UNSET_PARENT, Receivers.All);
    }


    public override void FlaskSetParent(RpcArgs args)
    {
        isRidingParentObject = true;
        transform.SetParent(VRFlaskTeleportPoint);
    }

    public override void FlaskUnsetParent(RpcArgs args)
    {
        isRidingParentObject = false;
        transform.SetParent(null);
    }

    void OnPlayerDisconnected(NetworkingPlayer player)
    {
        networkObject.Destroy();
    }
}
