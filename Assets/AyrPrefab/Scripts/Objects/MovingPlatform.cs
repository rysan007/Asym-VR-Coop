using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using System;

public class MovingPlatform : GenericTransformBlockBehavior
{
    Transform beginPoint;
    Transform endPoint;
    GameObject platformObject;
    Collider platformCollider;
    MeshRenderer plaformMeshRenderer;

    bool moveToEnd = true;
    bool areFeaturesOn = true;

    [SerializeField]
    bool isOn = true;

    [SerializeField]
    bool isExisting = true;

    [SerializeField]
    bool isPlatformMoving = true;

    [SerializeField]
    bool isActivatedByPlayer = false;

    [SerializeField]
    bool isReturningToBaseWOPlayer = false;

    [SerializeField]
    bool isStopAtEndWithPlayer = false;

    [SerializeField]
    bool willWaitAtEnds = false;
    public float endWaitTime = 0;

    [SerializeField]
    bool isJumpingPlatform = false;
    [Range(0,20)]
    public float upPower = 0;
    [Range(-20, 20)]
    public float rightPower = 0;
    [Range(-20, 20)]
    public float fowardPower = 0;

    public bool isTeleporter =  false;
    public float moveSpeed = 1;
    public float rotateSpeed = 1;
    public float teleportTime = 3;
    float teleportTimer;

    void Start()
    {
        beginPoint = transform.Find("Begin");
        endPoint = transform.Find("End");
        beginPoint.GetComponent<MeshRenderer>().enabled = false;
        endPoint.GetComponent<MeshRenderer>().enabled = false;

        platformObject = transform.Find("Platform").gameObject;
        platformObject.transform.position = beginPoint.position;

        platformCollider = platformObject.GetComponent<Collider>();
        plaformMeshRenderer = platformObject.GetComponent<MeshRenderer>();

        if (isExisting)
        {
            TurnOnPlatformExistance(true);
        }
        else
        {
            TurnOnPlatformExistance(false);
        }

        teleportTimer = teleportTime;
    }


    void Update()
    {
        if (isOn)
        {
            if(isExisting == false) //if on, it must exist
            {
                isExisting = true;
                TurnOnPlatformExistance(true);
            }
            DoPlatformFunctions();
        }
        else
        {
            if (!isExisting)
            {
                TurnOnPlatformExistance(false);
            }
        }

    }

    private void TurnOnPlatformExistance(bool onSwitch)
    {
        platformCollider.enabled = onSwitch;
        plaformMeshRenderer.enabled = onSwitch;
    }

    private void DoPlatformFunctions()
    {
        bool isOccupying = IsOccupiedByPlayer();

        if (isPlatformMoving)
        {
            if (isActivatedByPlayer)
            {
                if (isOccupying)
                {
                    if (isStopAtEndWithPlayer)
                    {
                        StayAtEnd();
                    }
                    else
                    {
                        MovePlatform();
                    }
                }
                else if (isReturningToBaseWOPlayer)
                {
                    ReturnToBase();
                }
            }
            else
            {
                MovePlatform();
            }
        }

        if (isJumpingPlatform)
        {
            if (isOccupying)
            {
                LaunchPlayer();
            }
        }
    }

    private void LaunchPlayer()
    {
        Vector3 launchDirection = new Vector3(rightPower, upPower, fowardPower);
        Vector3 platformRelative = platformObject.transform.InverseTransformDirection(launchDirection);
        platformObject.transform.GetChild(0).GetComponent<Rigidbody>().AddForce(-platformRelative.x, platformRelative.y, -platformRelative.z, ForceMode.VelocityChange);
    }

    private void MovePlatform()
    {
        if (isTeleporter)
        {
            if (teleportTimer <= 0)
            {
                BeginEndPositionSwitch();

                if (moveToEnd)
                {
                    platformObject.transform.position = endPoint.position;
                    platformObject.transform.rotation = endPoint.rotation;
                }
                else
                {
                    platformObject.transform.position = beginPoint.position;
                    platformObject.transform.rotation = beginPoint.rotation;
                }

                teleportTimer = teleportTime;
            }
            else
            {
                teleportTimer = teleportTimer - Time.deltaTime;
            }
        }
        else
        {
            BeginEndPositionSwitch();

            if (moveToEnd)
            {
                platformObject.transform.position = Vector3.MoveTowards(platformObject.transform.position, endPoint.position, moveSpeed * Time.deltaTime);
                platformObject.transform.rotation = Quaternion.RotateTowards(platformObject.transform.rotation, endPoint.rotation, rotateSpeed * Time.deltaTime);
            }
            else
            {
                platformObject.transform.position = Vector3.MoveTowards(platformObject.transform.position, beginPoint.position, moveSpeed * Time.deltaTime);
                platformObject.transform.rotation = Quaternion.RotateTowards(platformObject.transform.rotation, beginPoint.rotation, rotateSpeed * Time.deltaTime);
            }
        }
    }

    private void BeginEndPositionSwitch()
    {
        if (platformObject.transform.position == beginPoint.position)
        {
            if (willWaitAtEnds)
            {
                StartCoroutine(WaitToSwitchTo(true));
            }
            else
            {
                moveToEnd = true;
            }
            
        }
        else if (platformObject.transform.position == endPoint.position)
        {
            if (willWaitAtEnds)
            {
                StartCoroutine(WaitToSwitchTo(false));
            }
            else
            {
                moveToEnd = false;
            }
        }
    }

    public IEnumerator WaitToSwitchTo(bool switchTo)
    {
        yield return new WaitForSeconds(endWaitTime);
        moveToEnd = switchTo; 
    }

    public bool IsOccupiedByPlayer() //If it has child or not
    {
        return (platformObject.transform.childCount == 0) ? false : true;
    }

    private void ReturnToBase()
    {
        if (isTeleporter)
        {
            platformObject.transform.position = beginPoint.position;
            platformObject.transform.rotation = beginPoint.rotation;
        }
        else
        {
            platformObject.transform.position = Vector3.MoveTowards(platformObject.transform.position, beginPoint.position, moveSpeed * Time.deltaTime);
            platformObject.transform.rotation = Quaternion.RotateTowards(platformObject.transform.rotation, beginPoint.rotation, rotateSpeed * Time.deltaTime);
        }

    }

    private void StayAtEnd()
    {
        if (isTeleporter)
        {
            platformObject.transform.position = endPoint.position;
            platformObject.transform.rotation = endPoint.rotation;
        }
        else
        {
            platformObject.transform.position = Vector3.MoveTowards(platformObject.transform.position, endPoint.position, moveSpeed * Time.deltaTime);
            platformObject.transform.rotation = Quaternion.RotateTowards(platformObject.transform.rotation, endPoint.rotation, rotateSpeed * Time.deltaTime);
        }
        
    }

    public void StartPlatform()
    {

        networkObject.SendRpc(RPC_START_MOVEMENT, Receivers.AllBuffered);
    }

    public void StopPlatform()
    {
        networkObject.SendRpc(RPC_STOP_MOVEMENT, Receivers.AllBuffered);
    }

    public override void StartMovement(RpcArgs args)
    {
        isPlatformMoving = true;
    }

    public override void StopMovement(RpcArgs args)
    {
        isPlatformMoving = false;
    }
}

