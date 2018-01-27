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

    [Header("UI")]
    [SerializeField]
    private IngameHUD IngameHUD;
    [SerializeField]
    private SquadDisplay SquadDisplay;
    public Transform ParticlesParent;

    private PlayerSquad playerSquad;

    private float timeCounter;

    private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
            playerSquad = FindObjectOfType<PlayerSquad>();
        }
		else
		{
			GameObject.Destroy(this.gameObject);
		}
        timeCounter = 0;

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
                timeCounter += Time.deltaTime;
                break;
			case EGameState.GAME_OVER:
				break;
		}
	}

    public Vector2 GetPlayerSquadPosition()
    {
        return playerSquad.GetSquadTarget();
    }

    public PlayerTroop GetNearestPlayerTroop(Vector2 origin, float range)
    {
        return playerSquad.GetNearestTroop(origin, range);
    }

    public void ShowInfectedTroop(int troopIndex)
    {
        SquadDisplay.ShowInfectedTroop(troopIndex);
    }

    public void KillTroopWithInfection(int troopIndex)
    {
        playerSquad.KillTroopWithInfection(troopIndex);
    }

    public void InfectRandomTroop()
    {
        playerSquad.InfectRandomTroop();
    }

    public void GameOver()
    {
        SpawningManager.Instance.GameOver();
        IngameHUD.Show(timeCounter);
    }

    private void InitializeGame()
    {
        playerSquad.Init(SettingsService.GameSettings.playerTroops);
        SquadDisplay.Init(SettingsService.GameSettings.playerTroops);
        SupplyManager.Instance.InitializeGame();
        SpawningManager.Instance.InitializeGame();
    }
}
