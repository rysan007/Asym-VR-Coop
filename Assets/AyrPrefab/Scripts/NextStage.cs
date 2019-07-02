using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    GameObject VRPlayer;
    GameObject PCPlayer;
    MeshRenderer m_Mesh;
    void Start()
    {
        VRPlayer = GameObject.FindGameObjectWithTag("CameraRig");
        PCPlayer = GameObject.FindGameObjectWithTag("FirstPersonPC");
        m_Mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (m_Mesh.bounds.Contains(VRPlayer.transform.position) && m_Mesh.bounds.Contains(PCPlayer.transform.position))
        {
            if (NetworkManager.Instance.Networker.IsServer)
                SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
}
