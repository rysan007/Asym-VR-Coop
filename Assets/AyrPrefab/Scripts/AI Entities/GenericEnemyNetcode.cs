using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;

public class GenericEnemyNetcode : GenericEnemyBehavior
{
    public int enemyNumber = 0;
    GameObject[] enemies;
    public bool isDead =  false;
    private BoxCollider m_Collider;
    private Rigidbody m_RigidBody;

    private float minDistance = 3;
    
    //Players
    public GameObject VRPlayer;
    public GameObject NonVRPlayer;

    void Start()
    {
        //Set initial enemy values
        m_Collider = GetComponent<BoxCollider>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_Collider.enabled = true;
        m_RigidBody.isKinematic = false;

        //Get Players
        NonVRPlayer = GameObject.FindGameObjectWithTag("PlayerCube");
        VRPlayer = GameObject.FindGameObjectWithTag("VRCamera");
    }

    void Update()
    {
        if (NonVRPlayer == null)
        {
            NonVRPlayer = GameObject.FindGameObjectWithTag("PlayerCube");
        }
        if (VRPlayer == null)
        {
            VRPlayer = GameObject.FindGameObjectWithTag("VRCamera");
        }

        if (!isDead)
        {   
            EnemyBehavior();

            if (NetworkManager.Instance.IsServer)
            {
                networkObject.position = transform.position;
                networkObject.rotation = transform.rotation;
            }
            else
            {
                transform.position = networkObject.position;
                transform.rotation = networkObject.rotation;
            }
        }
    }

    //Enemy actions
    private void EnemyBehavior()
    {
        if(NonVRPlayer && VRPlayer)
        {
            float nonVRDistance = Vector3.Distance(transform.position, NonVRPlayer.transform.position);
            float VRDistance = Vector3.Distance(transform.position, VRPlayer.transform.position);

            if (nonVRDistance < VRDistance)
            {
                if (nonVRDistance > minDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, NonVRPlayer.transform.position, 1 * Time.deltaTime);
                }
            }
            else
            {
                if (VRDistance > minDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, VRPlayer.transform.position, 1 * Time.deltaTime);
                }
            }
        }
        else if (NonVRPlayer)
        {
            float nonVRDistance = Vector3.Distance(transform.position, NonVRPlayer.transform.position);
            if (nonVRDistance > minDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, NonVRPlayer.transform.position, 1 * Time.deltaTime);
            }
        }
        else if (VRPlayer)
        {
            float VRDistance = Vector3.Distance(transform.position, VRPlayer.transform.position);
            if (VRDistance > minDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, VRPlayer.transform.position, 1 * Time.deltaTime);
            }
        }
        
        //transform.Translate(Vector3.forward * Time.deltaTime);
        //transform.Rotate(Vector3.up * Time.deltaTime);
    }

    public void KillEnemyNet()
    {
        networkObject.SendRpc(RPC_KILL, Receivers.AllBuffered);
    }

    public void ReviveEnemyNet()
    {
        networkObject.SendRpc(RPC_REVIVE, Receivers.AllBuffered);
    }

    public override void Kill(RpcArgs args)
    {
        isDead = true;
        m_Collider.enabled = false;
        m_RigidBody.isKinematic = true;
    }

    public override void Revive(RpcArgs args)
    {
        isDead = false;
        m_Collider.enabled = true;
        m_RigidBody.isKinematic = false;
    }
}
