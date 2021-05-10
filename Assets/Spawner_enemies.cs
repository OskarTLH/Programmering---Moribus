using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_enemies : MonoBehaviour
{

    public GameObject[] enemies;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;
    public int enemyCount;

    int randEnemy;

    void Start()
    {
        StartCoroutine(waitspawner());  
    }

    
    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    public void SpawnNewEnemy()
    {
        randEnemy = Random.Range(0, enemies.Length);

        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, Random.Range(-spawnValues.z, spawnValues.z));

        Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
    }



    IEnumerator waitspawner()
    {
        yield return new WaitForSeconds (startWait);


        for (int i = 0; i<enemyCount;i++)
        {
            SpawnNewEnemy();
            yield return new WaitForSeconds(spawnWait);
        }

    }

}

