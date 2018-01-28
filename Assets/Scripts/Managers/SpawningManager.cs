using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawningManager : MonoBehaviour
{
    public static SpawningManager Instance = null;

    [SerializeField]
    private Transform SpawnersParent;

    private List<EnemySpawn> enemySpawns;

    private int enemiesInNextWave;
    private int spawnersInNextWave;
    private float periodBetweenNextWaves;

    private float timeCounter;
    private bool isInitialized;

    private GameSettings gameSettings { get { return SettingsService.GameSettings; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            enemySpawns = new List<EnemySpawn>(SpawnersParent.GetComponentsInChildren<EnemySpawn>());
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
        timeCounter = 0;
    }

    private void Update()
    {
        if(isInitialized)
        {
            timeCounter += Time.deltaTime;
            if(timeCounter > periodBetweenNextWaves &&
               EnemiesManager.Instance.currentNumberOfEnemiesOnTheMap < gameSettings.maxNumberOfEnemies)
            {
                SpawnEnemies();
            }
        }
    }

    public void InitializeGame()
    {
        isInitialized = true;
        enemiesInNextWave = gameSettings.minEnemiesFromSpawnerPerWave;
        spawnersInNextWave = gameSettings.minSpawnersInWave;
        periodBetweenNextWaves = gameSettings.maxEnemiesWavePeriodInS;
        SpawnEnemies();
    }

    public void GameOver()
    {
        isInitialized = false;
        for (int i = 0; i < enemySpawns.Count; i++)
        {
            Destroy(enemySpawns[i].gameObject);
        }
    }

    private void SpawnEnemies()
    {
        List<EnemySpawn> randomSpawners = GetRandomSpawners();
        SpawnEnemiesInSpawners(randomSpawners);
        UpdateSpawningData();
    }

    private List<EnemySpawn> GetRandomSpawners()
    {
        List<EnemySpawn> randomSpawners = new List<EnemySpawn>();
        while (randomSpawners.Count < enemySpawns.Count &&
              randomSpawners.Count < spawnersInNextWave)
        {
            var spawner = enemySpawns[UnityEngine.Random.Range(0, enemySpawns.Count)];
            if (!randomSpawners.Contains(spawner))
            {
                randomSpawners.Add(spawner);
            }
        }
        return randomSpawners;
    }

    private void SpawnEnemiesInSpawners(List<EnemySpawn> randomSpawners)
    {
        for (int i = 0; i < randomSpawners.Count; i++)
        {
            if(EnemiesManager.Instance.currentNumberOfEnemiesOnTheMap < SettingsService.GameSettings.maxNumberOfEnemies)
            {
                randomSpawners[i].SpawnEnemies(enemiesInNextWave);
            }
        }
    }

    private void UpdateSpawningData()
    {
        timeCounter = 0;
        //
        int updatedEnemiesInWave = enemiesInNextWave + gameSettings.enemiesIncreamentPerWave;
        enemiesInNextWave = Mathf.Clamp(updatedEnemiesInWave, gameSettings.minEnemiesFromSpawnerPerWave,
                                        gameSettings.maxEnemiesFromSpawnerPerWave);
        //
        int updatedSpawnersInWave = spawnersInNextWave + gameSettings.spawnersIncreamentPerWave;
        spawnersInNextWave = Mathf.Clamp(updatedSpawnersInWave, gameSettings.minSpawnersInWave,
                                         gameSettings.maxSpawnersInWave);
        //
        float updatedPeriodBetweenWaves = periodBetweenNextWaves - gameSettings.periodDecreamentPerWave;
        periodBetweenNextWaves = Mathf.Clamp(updatedPeriodBetweenWaves, gameSettings.minEnemiesWavePeriodInS,
                                             gameSettings.maxEnemiesWavePeriodInS);
    }
}