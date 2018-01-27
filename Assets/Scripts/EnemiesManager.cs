using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance = null;

    [SerializeField]
    private float updateRateInSeconds;

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
        timer += Time.deltaTime;
        if(timer > updateRateInSeconds)
        {
            timer = 0;
            Vector2 playerPos = GameManager.Instance.GetPlayerSquadPosition();
            for(int i = 0; i < EnemiesGroupList.Count; i++)
            {
                EnemiesGroupList[i].SetTarget(playerPos);
            }
        }
    }

    public void AddEnemyGroup(EnemiesGroup enemyGroup)
    {
        EnemiesGroupList.Add(enemyGroup);
    }
}
