using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AyrTeleporter : MonoBehaviour
{
    enum PointerLocation { Flask, Terrain, Disk};

    public VRHandInputs inputs;
    NetworkVRPlayer networkVRPlayer;
    public GameObject m_Pointer;
    private const float FLASKMOVEMENTTHRESHOLD = .15f;
    private const float VRFLASKSIZE = .1f;
    private const float FADETIME = 0.5f;
    private bool m_IsTeleporting = false;  //for teleport time delay
    
    [SerializeField]
    private float m_CurrentTPAreaScale = 1f;

    private GameObject VRFlask;
    private VRFlask VRFlaskScript;
    private MoveVRFlask moveVRFlask;
    private Rigidbody flaskRB;

    //private bool isMovingToFlask = false;
    private bool shouldMoveToFlask = false; //Dealing with network delay of parenting flask

    private VRPlayerScale scaler;


    private void Start()
    {
        inputs = GetComponent<VRHandInputs>();
        networkVRPlayer = transform.parent.GetComponent<NetworkVRPlayer>();
        m_Pointer = Instantiate(m_Pointer);
        VRFlask = GameObject.FindGameObjectWithTag("VRFlask");
        VRFlaskScript = VRFlask.GetComponent<VRFlask>();
        moveVRFlask = VRFlask.GetComponent<MoveVRFlask>();
        flaskRB = VRFlask.GetComponent<Rigidbody>();
        scaler = GetComponentInParent<VRPlayerScale>();

        //move to flask at beginning
        MoveInFlask();
    }

    void Update()
    {
        //Move in/out flask if not held and flask not moving
        if (inputs.TouchpadBottomDown())
        {
            if (!VRFlaskScript.isHeld && IsWithinMovingThreshold())
            {
                if (IsInFlask())
                {
                    MoveOutFlask();
                }
                else
                {
                    MoveInFlask();
                }
            }
            else
            {
                print("Can't move out: isHeld? " + VRFlaskScript.isHeld + " is moving? " + flaskRB.velocity);
            }
        }
        
        //After making flask the parent, this deals with network delay of parenting flask
        if (shouldMoveToFlask && transform.parent.transform.parent != null)              
        {
            transform.parent.transform.localPosition = new Vector3(0, -.05f, 0);
            shouldMoveToFlask = false;
        }

        if(flaskRB.velocity != Vector3.zero && !IsInFlask())
        {
            //isMovingToFlask = true;
            m_CurrentTPAreaScale = VRFLASKSIZE;
            shouldMoveToFlask = true;
        }
    }

    private bool IsWithinMovingThreshold()
    {
        if(flaskRB.velocity.x < FLASKMOVEMENTTHRESHOLD && flaskRB.velocity.y < FLASKMOVEMENTTHRESHOLD && flaskRB.velocity.z < FLASKMOVEMENTTHRESHOLD)
        {
            return true;
        }
        return false;
    }

    private void MoveInFlask()
    {
        //isMovingToFlask = true;
        m_CurrentTPAreaScale = .1f;
        shouldMoveToFlask = true;  //dealing with flask parent delay issues
        TeleportTo(SetPointer(PointerLocation.Flask));
        moveVRFlask.AddOccupant();  //tell everyone that VR is in flask
    }

    private void MoveOutFlask()
    {
        float flaskEnergy = VRFlaskScript.GetFlaskEnergy();
        if(flaskEnergy > 0)
        {
            m_CurrentTPAreaScale = flaskEnergy;
        }
        else
        {
            m_CurrentTPAreaScale = 1f;
        }
        TeleportTo(SetPointer(PointerLocation.Flask));
        moveVRFlask.RemoveOccupant();  //tell everyone that VR is out flask
    }

    private void TeleportTo(Vector3 loc)
    {
        // check for valid position and if already teleporting
        if (m_IsTeleporting)
        {
            print("Still teleporting, can't teleport again");
            return;
        }
        //get camera rig and head position
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        //figure out translation
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translateVector = loc - groundPosition;

        //move
        StartCoroutine(MoveRig(cameraRig, translateVector));

    }

    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        m_IsTeleporting = true;                             // Flag
        SteamVR_Fade.Start(Color.black, FADETIME, true);  //Fade to black
        yield return new WaitForSeconds(FADETIME);        //apply translation

        scaler.SetVRScale(m_CurrentTPAreaScale);            //set TP area's scale to VR character
        if (IsInFlask()/* && !isMovingToFlask*/)
        {
            networkVRPlayer.UnsetFlaskParent();
            cameraRig.transform.rotation = Quaternion.identity;
        }
        else/* if (isMovingToFlask)*/
        {
            networkVRPlayer.SetFlaskParent();
            //isMovingToFlask = false;
            shouldMoveToFlask = true;
        }
        //else
        //{
        //    cameraRig.position += translation;
        //}

        //fade to clear
        SteamVR_Fade.Start(Color.clear, FADETIME, true);

        //deflag
        m_IsTeleporting = false;

    }

    private Vector3 SetPointer(PointerLocation loc)
    {
        if (loc == PointerLocation.Flask)
        {
            m_Pointer.transform.position = VRFlask.GetComponent<VRFlask>().GetTeleportPoint();
        }
        return m_Pointer.transform.position;
    }


    private bool IsInFlask() //if teleporter hand's parent (Rig) has a parent (flask), return true
    {       
        if (transform.parent.parent != null)
            return true;
        else
            return false;
    }
}

//enum PointerLocation { Flask, Terrain, Disk };

//public VRHandInputs inputs;
//NetworkVRPlayer networkVRPlayer;
//public GameObject m_Pointer;
//public SteamVR_Action_Boolean m_TeleportAction;
//public SteamVR_Action_Boolean m_TouchpadCenter = null;

//private SteamVR_Behaviour_Pose m_Pose = null;
//private bool m_HasPosition = false;    //if pointer
//private bool m_IsTeleporting = false;  //for teleport time delay
//private float m_FadeTime = 0.5f;
//[SerializeField]
//private float m_CurrentTPAreaScale = 1f;
//[SerializeField]
//private bool teleportEnabled = true;
////private GameObject[] teleportAreas;
////private LineRenderer pointerLine;
//float pointerLineWidth;

//private GameObject VRFlask;
//private VRFlask VRFlaskScript;
//private MoveVRFlask moveVRFlask;
//private Rigidbody flaskRB;

//private bool isMovingToFlask = false;
////private bool isInsideFlask = false;
//private bool shouldMoveToFlask = false; //Dealing with network delay of parenting flask
//private bool moveToFlaskatStart = true;

//private VRPlayerScale scaler;

//int ignoreVRBodyLayermask = ~(1 << 12 & 1 << 2);


//private void Start()
//{
//    inputs = GetComponent<VRHandInputs>();
//    networkVRPlayer = transform.parent.GetComponent<NetworkVRPlayer>();

//    m_TouchpadCenter = SteamVR_Actions.VRCoopGame.Teleport;

//    m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
//    m_Pointer = Instantiate(m_Pointer);
//    //teleportAreas = GameObject.FindGameObjectsWithTag("TeleportArea");

//    VRFlask = GameObject.FindGameObjectWithTag("VRFlask");
//    VRFlaskScript = VRFlask.GetComponent<VRFlask>();
//    moveVRFlask = VRFlask.GetComponent<MoveVRFlask>();
//    flaskRB = VRFlask.GetComponent<Rigidbody>();

//    scaler = GetComponentInParent<VRPlayerScale>();
//    //pointerLine = GetComponent<LineRenderer>();
//    //pointerLineWidth = pointerLine.startWidth;

//}

//in UPDATE
//if (teleportEnabled)
//{
//    //pointer  
//    if (m_TouchpadCenter.GetState(m_Pose.inputSource))
//    {
//        m_HasPosition = UpdatePointer(PointerLocation.Terrain);

//        pointerLine.enabled = true;
//        pointerLine.startWidth = pointerLineWidth * scaler.GetCurrentScale();
//        pointerLine.endWidth = pointerLineWidth * scaler.GetCurrentScale();
//        if (m_HasPosition)
//        {
//            m_Pointer.SetActive(true);
//        } 
//    }
//    else
//    {
//        pointerLine.enabled = false;
//        m_Pointer.SetActive(false);
//    }

//    //teleport
//    if (m_TouchpadCenter.GetStateUp(m_Pose.inputSource))
//    {
//        //print("trying teleport");
//        TryTeleport();
//    }
//}


//If m_HasPosition, start coroutine to move rig to that location
//private void TryTeleport()
//{
//    // check for valid position and if already teleporting
//    if(!m_HasPosition || m_IsTeleporting)
//    {
//        print("no teleport, returning: hasPosition: " + m_HasPosition + " isTeleporting: " + m_IsTeleporting);
//        return;
//    }
//    //get camera rig and head position
//    Transform cameraRig = SteamVR_Render.Top().origin;
//    Vector3 headPosition = SteamVR_Render.Top().head.position;

//    //figure out translation
//    Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
//    Vector3 translateVector = m_Pointer.transform.position - groundPosition;

//    //move
//    StartCoroutine(MoveRig(cameraRig, translateVector));

//}



//public void UpdateTeleportAreas()
//{
//    teleportAreas = GameObject.FindGameObjectsWithTag("TeleportArea");
//}


//private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
//{
//    m_IsTeleporting = true;                             // Flag
//    SteamVR_Fade.Start(Color.black, m_FadeTime, true);  //Fade to black
//    yield return new WaitForSeconds(m_FadeTime);        //apply translation

//    scaler.SetVRScale(m_CurrentTPAreaScale);            //set TP area's scale to VR character

//    if (IsInFlask() && !isMovingToFlask)
//    {
//        networkVRPlayer.UnsetFlaskParent();
//        cameraRig.transform.rotation = Quaternion.identity;
//    }
//    else if (isMovingToFlask)
//    {
//        networkVRPlayer.SetFlaskParent();
//        isMovingToFlask = false;
//        shouldMoveToFlask = true;
//    }
//    else
//    {    
//        cameraRig.position += translation;
//    }

//    //fade to clear
//    SteamVR_Fade.Start(Color.clear, m_FadeTime, true);

//    //deflag
//    m_IsTeleporting = false;

//}

//private bool UpdatePointer(PointerLocation loc)
//{
//    if (loc == PointerLocation.Terrain)
//    {
//        Ray ray = new Ray(transform.position + transform.forward * .5f * scaler.GetCurrentScale(), transform.forward);
//        RaycastHit hit;

//        //Check for valid teleport location
//        if (Physics.Raycast(ray, out hit)) ;//, 100, ignoreVRBodyLayermask))
//        {
//            if (hit.transform)
//            {
//                if (hit.transform.CompareTag("Terrain")) //maybe use layer here
//                {
//                    //Check hit location as valid teleport area
//                    foreach (GameObject ob in teleportAreas)
//                    {
//                        if (ob.GetComponent<Collider>().bounds.Contains(hit.point))
//                        {
//                            //get teleport area scale
//                            m_CurrentTPAreaScale = ob.GetComponent<TeleportAreaVRScaler>().GetTeleportAreaScale();

//                            //move pointer to valid position
//                            m_Pointer.transform.position = hit.point;
//                            return true;
//                        }
//                    }
//                }
//                //else if (hit.transform.CompareTag("BugInteractable")) //maybe use layer here
//                //{
//                //    if (hit.transform.GetComponent<BugControlObject>().isActivated)
//                //    {
//                //        //get teleport area scale
//                //        m_CurrentTPAreaScale = hit.transform.GetComponent<BugControlObject>().teleportScale; //Size for VRFlask

//                //        //move pointer to crystal teleport point
//                //        m_Pointer.transform.position = hit.transform.GetComponent<BugControlObject>().GetTeleportPoint();
//                //        return true;
//                //    }
//                //}
//                else if (hit.transform.CompareTag("TeleportDisk")) //maybe use layer here
//                {
//                    m_Pointer.transform.position = hit.point;
//                    return true;
//                }
//            }
//        }
//    }
//    else if(loc == PointerLocation.Flask)
//    {
//        m_Pointer.transform.position = VRFlask.GetComponent<VRFlask>().GetTeleportPoint();
//        return true;
//    }
//    //if not a hit
//    return false;
//}

//public void EnableTeleport()
//{
//    teleportEnabled = true;
//}

//public void DisableTeleport()
//{
//    teleportEnabled = false;
//}

