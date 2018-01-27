using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance = null;

	public enum EGameState
	{
		GAME_STARTING,
		GAME_IN_PROGRESS,
		GAME_OVER
	}

	public EGameState CurrentGameState = EGameState.GAME_STARTING;

    [SerializeField]
    private int playerTroops;
    [SerializeField]
    private int minEnemiesPerWave;
    [SerializeField]
    private int maxEnemiesPerWave;
    [SerializeField]
    private float enemiesWavePeriod;

    private PlayerSquad playerSquad;
    private List<EnemySpawn> enemySpawns;

    private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
            FindReferences();
		}
		else
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	private void Update()
	{
		switch(CurrentGameState)
		{
			case EGameState.GAME_STARTING:
                InitializeGame();
                CurrentGameState = EGameState.GAME_IN_PROGRESS;
                break;
			case EGameState.GAME_IN_PROGRESS:
				break;
			case EGameState.GAME_OVER:
				break;
		}
	}

    public Vector2 GetPlayerSquadPosition()
    {
        return playerSquad.GetSquadTarget();
    }

    private void FindReferences()
    {
        playerSquad = FindObjectOfType<PlayerSquad>();
        enemySpawns = new List<EnemySpawn>(FindObjectsOfType<EnemySpawn>());
    }

    private void InitializeGame()
    {
        playerSquad.Init(playerTroops);
        for(int i = 0; i < enemySpawns.Count; i++)
        {
            enemySpawns[i].Init(Random.Range(minEnemiesPerWave, maxEnemiesPerWave),
                                enemiesWavePeriod, Random.Range(0, enemiesWavePeriod));
        }
    }
}
