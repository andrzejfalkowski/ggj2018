using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public EnemyProperties WeakZombi;
    public PlayerProperties Player;

    [Header("Infection")]
    public float InfectionCooldown = 5;

    [Header("Formations")]
    public float RectFormation_distanceBetweenTroops = 0.5f;
    public float HordeFormation_distanceBetweenTroops = 0.3f;
    public float HordeFormation_speadingFactor = 0.1f;

    [Header("Navigation")]
    public float samplePositionRange = 3;

    [Header("Visual")]
    public float healthBarVisibilityTime = 2.0f;
    public float frontArmAnimLength = 0.1f;
    public float frontArmRestoreIdleLength = 0.5f;

    [Header("Spawners")]
    public int playerTroops = 20;
    [Space]
    public int minEnemiesFromSpawnerPerWave = 10;
    public int maxEnemiesFromSpawnerPerWave = 25;
    public int enemiesIncreamentPerWave = 2;
    [Space]
    public int minSpawnersInWave = 2;
    public int maxSpawnersInWave = 8;
    public int spawnersIncreamentPerWave = 1;
    [Space]
    public float maxEnemiesWavePeriodInS = 30;
    public float minEnemiesWavePeriodInS = 15;
    public int periodDecreamentPerWave = 1;
}