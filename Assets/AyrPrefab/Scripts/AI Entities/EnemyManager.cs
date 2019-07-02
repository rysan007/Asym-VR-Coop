using UnityEngine;
using UnityEngine.XR;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    GameObject genericEnemyPrefab;
    public GameObject genericEnemyPuppetPrefab;

    bool searchForEnemies = true;
    public GameObject[] enemiesClient;
    public List<GameObject> enemiesClientList = new List<GameObject>();

    int enemyAmount = 5;

    void Start()
    {
        if (NetworkManager.Instance.IsServer)
        {
            searchForEnemies = false;
            for (int i = 0; i <= enemyAmount - 1; i++)
            {

                Vector3 randomPosition = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
                GenericEnemyNetcode en = NetworkManager.Instance.InstantiateGenericEnemy(i, randomPosition) as GenericEnemyNetcode;
                en.enemyNumber = i;
                GameObject puppet = Instantiate(genericEnemyPuppetPrefab);
                puppet.GetComponent<GenericEnemyPuppet>().enemy = en.gameObject;
            }
        }
    }

    void Update()
    {
        if (searchForEnemies)
        {
            CreateEnemyPuppets();
        }
    }

    private void CreateEnemyPuppets()
    {
        if (!NetworkManager.Instance.IsServer)
        {
            enemiesClient = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject en in enemiesClient)
            {
                GameObject puppet = Instantiate(genericEnemyPuppetPrefab);
                puppet.GetComponent<GenericEnemyPuppet>().enemy = en;
            }
        }
        searchForEnemies = false;  
    }
}
