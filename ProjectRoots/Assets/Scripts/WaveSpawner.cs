using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    //public Transform[] enemyPrefab;


    [SerializeField] private float timeToStart = 1f;

    [SerializeField] private float _intervalBetweenWaves = 10f;

    [SerializeField] private float _intervalBetweenWavesDecrementValue = 2.5f;

    //[SerializeField]
    //public float countdown = 2f;

    //[SerializeField]
    //public float cooldown = 5f;

    //[SerializeField]
    //private int waveIndex = 0;

    private float _elapsedTime;

    private bool _startSpawning = false;


    [Serializable]
    private struct WavesPerLevel
    {
        public int levelIndex;
        public List<Wave> waves;
    }

    [SerializeField] private List<WavesPerLevel> _wavesPerLevel;


    [Serializable]
    private class Wave 
    {
        [Serializable]
        public struct EnemyPresence
        {
            public GameObject enemyPrefab;
            public int minNumberInWave;
            public int maxNumberInWave;
        }

        public List<EnemyPresence> enemiesInWave;

        public float intervalBetweenEnemies = 1f;

        private List<GameObject> enemiesQueue = new List<GameObject>();

        private float elapsedTime = 0;

        private Transform spawnPoint;

        private bool waveCompleted = false;

        protected Wave() { }
        public Wave(Wave wave)
        {
            enemiesInWave = new List<EnemyPresence>(wave.enemiesInWave);
            intervalBetweenEnemies = wave.intervalBetweenEnemies;
        }

        public List<GameObject> CreateEnemiesQueue()
        {
            List<GameObject> allEnemies = new List<GameObject>();
            foreach(EnemyPresence enemyPresence in enemiesInWave)
            {
                int res = UnityEngine.Random.Range(enemyPresence.minNumberInWave, maxExclusive: enemyPresence.maxNumberInWave + 1);
                for (int i = 0; i < res; i++)
                    allEnemies.Add(enemyPresence.enemyPrefab);
            }

            //List<GameObject> enemiesQueue = new List<GameObject>();
            for(int i = 0; i < allEnemies.Count; i++)
            {
                int res = UnityEngine.Random.Range(0, allEnemies.Count);
                enemiesQueue.Add(allEnemies[res]);
                allEnemies.RemoveAt(res);
            }

            return enemiesQueue;
        }

        public void StartSpawnAt(Transform at)
        {
            spawnPoint = at;
            SpawnNextEnemy();
        }

        private void SpawnNextEnemy()
        {
            if(enemiesQueue.Count == 0)
            {
                waveCompleted = true;
                return;
            }

            Instantiate(enemiesQueue.First(), spawnPoint.position, spawnPoint.rotation);
            enemiesQueue.RemoveAt(0);
        }

        public void UpdateWave(float deltaTime)
        {
            if (waveCompleted) return;

            if(elapsedTime > intervalBetweenEnemies)
            {
                SpawnNextEnemy();
                elapsedTime = 0;
            }
            else
            {
                elapsedTime += deltaTime;
            }
        }

        public bool Completed()
        {
            return waveCompleted;
        }
    }

    private Wave _currentLeftWave;

    private Wave _currentRightWave;

    private void Awake()
    {
        GameManager.Instance.onRootUnlocked += OnNewRootUnlocked;
    }

    private void OnNewRootUnlocked(TreeRootNode treeRootNodeUnlocked)
    {
        if(treeRootNodeUnlocked.earningType == TreeRootNode.EarningType.FixedValue)
        {
            NewWaveLevel();
        }
    }

    private void NewWaveLevel()
    {
        _wavesPerLevel.RemoveAt(0);
    }


    // Update is called once per frame
    void Update()
    {
        if (_startSpawning) 
        {
            _currentLeftWave.UpdateWave(Time.deltaTime);
            _currentRightWave.UpdateWave(Time.deltaTime);

            if (_currentLeftWave.Completed())
            {
                SpawnNewLeftWave();
            }

            if (_currentRightWave.Completed())
            {
                SpawnNewRightWave();
            }

            return;
        }
        
        if (_elapsedTime > timeToStart)
        {
            SpawnNewLeftWave();
            SpawnNewRightWave();
            _startSpawning = true;
        }
        else
        {
            _elapsedTime += Time.deltaTime;
        }
    }

    void SpawnNewLeftWave()
    {
        int ranLeft = UnityEngine.Random.Range(0, _wavesPerLevel.First().waves.Count);

        _currentLeftWave = new Wave(_wavesPerLevel.First().waves[ranLeft]);
        _currentLeftWave.CreateEnemiesQueue();
        _currentLeftWave.StartSpawnAt(at: spawnPoints[0]);
    }


    void SpawnNewRightWave()
    {
        int ranRight = UnityEngine.Random.Range(0, _wavesPerLevel.First().waves.Count);

        _currentRightWave = new Wave(_wavesPerLevel.First().waves[ranRight]);
        _currentRightWave.CreateEnemiesQueue();
        _currentRightWave.StartSpawnAt(at: spawnPoints[1]);
    }
}
