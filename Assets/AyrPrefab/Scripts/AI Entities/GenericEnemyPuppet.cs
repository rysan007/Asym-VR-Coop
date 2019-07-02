using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyPuppet : MonoBehaviour
{
    public GameObject enemy;
    public int enemyNumber = 0;
    GameObject[] enemies;
    public bool isDead = false;
    private GenericEnemyNetcode enemyNetcode;
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;

    void Start()
    {
        enemyNetcode = enemy.GetComponent<GenericEnemyNetcode>();
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        transform.position = enemy.transform.position;
        transform.rotation = enemy.transform.rotation;

        //If the net enemy status has changed, kill or revive puppet
        if (enemyNetcode.isDead != isDead)
        {
            if (enemyNetcode.isDead)
            {
                KillPuppet();
            }
            else
            {
                RevivePuppet();
            }
        }

    }

    public void KillPuppet() 
    {
        isDead = true;
        boxCollider.enabled = false;
        meshRenderer.enabled = false;
        enemyNetcode.KillEnemyNet();
    }

    public void RevivePuppet()
    {
        isDead = false;
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
        enemyNetcode.ReviveEnemyNet();
    }
}
