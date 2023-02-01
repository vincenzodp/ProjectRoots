using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    //public Transform spawnPoint;
    //public Transform spawnPoint2;
    public Transform[] enemyPrefab;

    [SerializeField]
    public float countdown = 2f;

    [SerializeField]
    public float cooldown = 5f;

    [SerializeField]
    private int waveIndex = 0;


    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = cooldown;
            return;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }

    void SpawnEnemy()
    {
        if (waveIndex < 3)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length - 1)], spawnPoints[i].position, spawnPoints[i].rotation);
            }
            //Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length - 1)], spawnPoints[0].position, spawnPoints[0].rotation);
            //Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length - 1)], spawnPoints[1].position, spawnPoints[1].rotation);
            //Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)], spawnPoint.position, spawnPoint.rotation);           
            //Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)], spawnPoint2.position, spawnPoint2.rotation);
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)], spawnPoints[i].position, spawnPoints[i].rotation);
            }
            //Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length - 1)], spawnPoints[0].position, spawnPoints[0].rotation);
            //Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length - 1)], spawnPoints[1].position, spawnPoints[1].rotation);
            //Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)], spawnPoint.position, spawnPoint.rotation);
            //Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)], spawnPoint2.position, spawnPoint2.rotation);
        }
    }

    IEnumerator SpawnWave()
    {
        //Debug.Log("Enemy Spawned!");

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
        waveIndex++;
    }
}
