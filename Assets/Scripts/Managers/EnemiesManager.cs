using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance = null;
    
    public float currentNumberOfEnemiesOnTheMap;

    private List<EnemiesGroup> EnemiesGroupList;

    private float timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
        timer = 0;
        EnemiesGroupList = new List<EnemiesGroup>();
    }

    private void Update()
    {
        NavigateEnemies();
        UpdateEnemiesState();
    }

    public void AddEnemyGroup(EnemiesGroup enemyGroup)
    {
        EnemiesGroupList.Add(enemyGroup);
        currentNumberOfEnemiesOnTheMap += enemyGroup.GetEnemiesCount();
    }

    public EnemyCreature GetNearestEnemy(Vector2 origin, float range)
    {
        EnemyCreature enemy = null;
        for (int i = 0; i < EnemiesGroupList.Count; i++)
        {
            enemy = EnemiesGroupList[i].GetNearestEnemy(enemy, origin, range);
        }
        return enemy;
    }

    private void NavigateEnemies()
    {
        timer += Time.deltaTime;
        if (timer > SettingsService.GameSettings.updateRateInSeconds)
        {
            timer = 0;
            Vector2 playerPos = GameManager.Instance.GetPlayerSquadPosition();
            for (int i = 0; i < EnemiesGroupList.Count; i++)
            {
                EnemiesGroupList[i].SetTarget(playerPos);
            }
        }
    }

    private void UpdateEnemiesState()
    {
        currentNumberOfEnemiesOnTheMap = 0;
        for (int i = 0; i < EnemiesGroupList.Count; i++)
        {
            EnemiesGroupList[i].UpdateGroupState();
            currentNumberOfEnemiesOnTheMap += EnemiesGroupList[i].GetEnemiesCount();
            if (EnemiesGroupList[i].GetEnemiesCount() == 0)
            {
                EnemiesGroupList.RemoveAt(i);
                i--;
            }
        }
    }
}
