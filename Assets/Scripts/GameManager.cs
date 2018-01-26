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

	public void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			GameObject.Destroy(this.gameObject);
		}

	}

	void Update()
	{
		switch(CurrentGameState)
		{
			case EGameState.GAME_STARTING:
				break;
			case EGameState.GAME_IN_PROGRESS:
				break;
			case EGameState.GAME_OVER:
				break;
		}
	}
}
