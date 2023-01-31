using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform spawnPoint;
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
            Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length - 1)], spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)], spawnPoint.position, spawnPoint.rotation);
        }
    }

    IEnumerator SpawnWave()
    {
        Debug.Log("Enemy Spawned!");

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
        waveIndex++;
    }
}
