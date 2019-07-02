using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.SceneManagement;

public class LoadNextStage : MonoBehaviour
{
    public int loadStageNumber = 0;

    public void StartNextStage()
    {
        if (NetworkManager.Instance.Networker.IsServer)
            SceneManager.LoadScene(loadStageNumber, LoadSceneMode.Single);
    }
}
