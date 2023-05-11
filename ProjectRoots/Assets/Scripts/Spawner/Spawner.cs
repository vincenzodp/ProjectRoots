using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject baseEnemyPrefab;
    [SerializeField] GameObject heavyEnemyPrefab;

    [SerializeField] SpawnPoint leftSpawnPoint, rightSpawnPoint;

    [SerializeField, Tooltip("Enable this to experience a well balanced scaling difficulty gameplay")] 
    bool useExperimentalSuperbalancedExperience = false;

    [SerializeField] int[] enemies;
    int enemiesIndex = 0;

    float spawnTimer = 0f;
    float difficultyTimer = 0f;

    float intervalBetweenSpawns = 5f;
    float intervalBetweenDifficultyIncreases = 30f;

    int difficultyLevel = 0;
    int lapsesInJudjment = 0;


    // Update is called once per frame
    void Update()
    {
        DifficultyTimerTick();
        SpawnEnemyTimerTick();
        TimewarpDevControls();
    }

    // this timer tries to increase difficulty if allowed by at least one continuos and severe lapse in judgement.
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

    // this will spawn enemies on both lanes every time the timer reaches 0
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

    // increasing difficulty only changes spawn frequency, when using experimental features it will increase only when the full list of enemies
    // has elapsed, and shall keep increasing every "lap" of the list until it gets to 6 after which it resets (which is not desirable but for now this is how it works)
    void IncreaseDifficulty()
    {
        // this is here in case somebody is not at all daring and must suffer through the previous system else their sanity gets drained.
        // please consider that the following variable is false by default to check whether commit notes are being considered at all.
        if(!useExperimentalSuperbalancedExperience)
        {
            difficultyLevel++;
            //print(difficultyLevel);
            ApplyDifficultyChanges();
            return;
        }

        switch (lapsesInJudjment)
        {
            case 0: 
                difficultyLevel = 1;
                break;
            case 1:
                difficultyLevel = 2;
                break;
            case 2: 
                difficultyLevel = 3;
                break;
            case 3:
                difficultyLevel = 4;
                break;
            case 4: 
                difficultyLevel = 5;
                break;
            case 5:
                difficultyLevel = 6;
                break;
            default:
                difficultyLevel = 1;
                lapsesInJudjment = 0;
                break;
        }

        print(difficultyLevel);

        ApplyDifficultyChanges();
    }

    // with experimental on, this is directly responsible for changing the difficulty, it might seem redundant and that is because
    // it is, currently only spawn speed is influenced so clearly this is the case, but it remains for future implementations.
    void ApplyDifficultyChanges()
    {
        switch (difficultyLevel)
        {
            case 1:
                intervalBetweenSpawns = 5f; 
                break;
            case 2:
                intervalBetweenSpawns = 4f;
                break;
            case 3:
                intervalBetweenSpawns = 3f; 
                break;
            case 4:
                intervalBetweenSpawns = 2f;
                break;
            case 5:
                intervalBetweenSpawns = 1f;
                break;
            default: 
                intervalBetweenSpawns = .5f;
                break;
        }
    }

    // this goes through the enemy array and spawns the current enemy denoted by the enemiesIndex, in both lanes. it is designed robustly and 
    // cannot go out of bounds or lose track of spawns. when the index is reset after iterating thorugh every element, a severe and continuos
    // laps in judgement occurs, resulting in the array being reset but the spawns now being faster as per difficulty level selected.
    // currently it was also unavoidable to have to destroy enemies after they reach the tree and hit once because the turrets are not functioning
    // it does raise a valid point, whether the enemies should die after one hit in any case, since receiving a hit is very problematic in any case.
    void SpawnEnemy()
    {
        if(enemiesIndex >= enemies.Length)
        {
            enemiesIndex = 0;
            lapsesInJudjment++;
            difficultyTimer = 0;
        }

        if(enemies[enemiesIndex] == 0)
        {
            //Destroy(Instantiate(baseEnemyPrefab, leftSpawnPoint.position, Quaternion.identity), 14f);
            //Destroy(Instantiate(baseEnemyPrefab, rightSpawnPoint.position, Quaternion.identity), 14f);
            //Instantiate(baseEnemyPrefab, leftSpawnPoint.position, Quaternion.identity);
            //Instantiate(baseEnemyPrefab, rightSpawnPoint.position, Quaternion.identity);
            leftSpawnPoint.SpawnTarget(baseEnemyPrefab);
            rightSpawnPoint.SpawnTarget(baseEnemyPrefab);

        }
        else
        {
            //Destroy(Instantiate(heavyEnemyPrefab, leftSpawnPoint.position, Quaternion.identity), 23f);
            //Destroy(Instantiate(heavyEnemyPrefab, rightSpawnPoint.position, Quaternion.identity), 23f);
            //Instantiate(heavyEnemyPrefab, leftSpawnPoint.position, Quaternion.identity);
            //Instantiate(heavyEnemyPrefab, rightSpawnPoint.position, Quaternion.identity);
            leftSpawnPoint.SpawnTarget(heavyEnemyPrefab);
            rightSpawnPoint.SpawnTarget(heavyEnemyPrefab);
        }

        enemiesIndex++;
    }

    // this is my gift to you, 
    // oh tester
    // may your sanity be restored,
    // i bequeath to you this cheat.
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


    /*
        Some useful considerations to keep in mind when using this spawner system.
        The list of enemies is contained in a simple int array, where every 0
        is considered a baseEnemy, and every non zero number is considered a HeavyEnemy.
        for convention we shall use 1 for heavy enemies and 0 for base enemies.

        it is worth noting that the system automatically increases the difficulty level,
        which right now consists only of the interval between spawns, but it must be noted 
        that this difficulty level can be adjusted at any time by further additions to this script
        or by using external elements to adjust difficulty, like perhaps buying a powerup that
        lowers difficulty to slow down the relentless spawning of enemies.

        Being that the sequence of enemies is already known based on the contents of the array, 
        difficulty can be adjusted throughout the experience to allow faster spawns of lesser
        enemies, and then be toned down for tougher enemies later on.

        this leaves abundant freedom to mix and match spawn frequency with specific enemies
        allowing for great variety while keeping things simple and working solidly.

        an example of this has already been implemented and to try it simply activate the
        related variable from the inspector (useExperimentalSuperbalancedExperience = true). 
        
        If you have any quandaries please contact dev:Tayumaru on discord.
    */
}
