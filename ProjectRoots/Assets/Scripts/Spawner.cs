using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] Transform leftSpawnPoint, rightSpawnPoint;

    float spawnTimer = 0f;
    float difficultyTimer = 0f;

    float intervalBetweenSpawns = 5f;
    float intervalBetweenDifficultyIncreases = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DifficultyTimerTick();
        SpawnEnemyTimerTick();

        TimewarpDevControls();

        
    }

    private void DifficultyTimerTick()
    {
        if (difficultyTimer <= 0)
        {
            difficultyTimer = intervalBetweenDifficultyIncreases;
            IncreaseDifficulty();
        }
        else
        {
            difficultyTimer -= Time.deltaTime;
        }
    }

    private void SpawnEnemyTimerTick()
    {
        if (spawnTimer <= 0)
        {
            spawnTimer = intervalBetweenSpawns;
            SpawnEnemy();
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    void IncreaseDifficulty()
    {
        if(intervalBetweenSpawns > .6f)
        {
            intervalBetweenSpawns -= .2f;
        }
    }

    void SpawnEnemy()
    {
        // since the tree does not shoot decency demands drastic dispositions ---------->
        Destroy(Instantiate(enemyPrefab, leftSpawnPoint.position, Quaternion.identity), 15f);
        Destroy(Instantiate(enemyPrefab, rightSpawnPoint.position, Quaternion.identity), 15f);
    }

    void TimewarpDevControls()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 5;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1;
        }
    }
}
