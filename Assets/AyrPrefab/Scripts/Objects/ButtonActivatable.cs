using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivatable : AyrActivatable
{

    private GenericButtonController m_Button;
    private NetworkGenericButton netButton;

    bool buttonReady = true;


    public float buttonResetTime = 2;
    public bool isTeleportingObject = false;
    public GameObject objectToTeleport;
    public bool teleportVRPlayer = false;
    public bool teleportPCPlayer = false;
    public GameObject teleportLocationObject;
    public Vector3 teleportLocation;

    public bool isActivatingEffect = false;
    public GameObject effectToActivate;
    public GameObject effectLocationObject;
    public Vector3 effectLocation;
    public Quaternion effectDirection;

    public bool isEnablingObject = false;
    public GameObject[] objectsToEnable;

    bool isStart = true;
    public bool testActivate = false;

    //private void Awake()
    //{
    //    if (isEnablingObject)
    //    {
    //        DisableObjects();
    //    }
    //}

    //private void DisableObjects()
    //{
    //    foreach (GameObject ob in objectsToEnable)
    //    {
    //        MeshRenderer[] allMeshes = ob.GetComponentsInChildren<MeshRenderer>();
    //        foreach (MeshRenderer child in allMeshes)
    //        {
    //            child.enabled = false;
    //        }
    //        Collider[] allColliders = ob.GetComponentsInChildren<Collider>();
    //        foreach (Collider child in allColliders)
    //        {
    //            child.enabled = false;
    //        }
    //    }
    //}

    private void Start()
    {
        m_Button = GetComponent<GenericButtonController>();
        netButton = GetComponent<NetworkGenericButton>();
        if (isTeleportingObject)
        {
            teleportLocation = teleportLocationObject.GetComponent<MeshRenderer>().bounds.center;
        }

        if (isActivatingEffect)
        {
            effectLocation = effectLocationObject.transform.position;
        }
    }

    //private void Update()
    //{
    //    if (testActivate)
    //    {
    //        testActivate = false;
    //        Action();
            
    //    }
    //}

    public override void Action()
    {
        if (buttonReady)
        {
            ButtonAction();
            netButton.PressButton();
            m_Button.AnimateButtonPressed();
            Invoke("UnpressButton", buttonResetTime);
            buttonReady = false;
        }   
    }

    private void ButtonAction()
    {
        if (isTeleportingObject)
        {
            if (teleportVRPlayer)
            {
                objectToTeleport = GameObject.FindGameObjectWithTag("CameraRig");
            }
            else if(teleportPCPlayer)
            {
                objectToTeleport = GameObject.FindGameObjectWithTag("FirstPersonPC");
            }

            if (objectToTeleport)
            {
                //Set VR player scale if teleport area
                if (teleportVRPlayer && teleportLocationObject.CompareTag("TeleportArea"))
                {
                    objectToTeleport.GetComponentInParent<VRPlayerScale>().SetVRScale(teleportLocationObject.GetComponent<TeleportAreaVRScaler>().GetTeleportAreaScale());
                    objectToTeleport.transform.position = teleportLocation;
                }
                objectToTeleport.transform.position = teleportLocation; 
            }
            else
            {
                print("Object to teleport not found!");
            }

        }
        if (isActivatingEffect)
        {
            Instantiate(effectToActivate, effectLocation, effectDirection);
        }
        if (isEnablingObject)
        {
            foreach (GameObject ob in objectsToEnable)
            {
                MeshRenderer[] allMeshes = ob.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer child in allMeshes)
                {
                    child.enabled = true;
                }
                Collider[] allColliders = ob.GetComponentsInChildren<Collider>();
                foreach (Collider child in allColliders)
                {
                    child.enabled = true;
                }
            }        
        }
        
    }

    private void UnpressButton()
    {
        m_Button.AnimateButtonUnpressed();
        buttonReady = true;
    }

}