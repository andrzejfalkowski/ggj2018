using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGroup
{
    private HordeFormation formation;
    private List<EnemyCreature> enemies;
    private TargetComponent Target;

    public EnemiesGroup(List<EnemyCreature> _enemies)
    {
        enemies = _enemies;
        Target = new TargetComponent();
        formation = new HordeFormation(enemies.Count);
    }

    public void UpdateGroupState()
    {
        int prevEnemiesCount = enemies.Count;
        for (int i = 0; i < enemies.Count; i++)
        {
            PerformAttack(enemies[i]);
            if (!enemies[i].IsAlive())
            {
                GameObject.Destroy(enemies[i].EnemyGO);
                enemies.RemoveAt(i);
                i--;
            }
        }
        CheckCreaturesState(prevEnemiesCount);
    }

    public void SetTarget(Vector2 target)
    {
        //TODO
        //kiedy żołnierz znajduje się w zasięgu, przeciwnik przestaje podążać do celu grupowego
        //wyznacza własny cel na najbliższego przeciwnika
        Target.Position = target;
        formation.CalculatePosition(target);
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].UpdatePosition(formation.GetPositionOfTroop(i, enemies[i].Position));
        }
    }

    public EnemyCreature GetNearestEnemy(EnemyCreature nearestEnemy, Vector2 origin)
    {
        EnemyCreature returnedEnemy = nearestEnemy;
        float distanceToClosestEnemy = nearestEnemy != null ? Vector3.Distance(nearestEnemy.Position, origin) : float.MaxValue;
        for (int i = 0; i < enemies.Count; i++)
        {
            float distanceToCurrentEnemy = Vector3.Distance(enemies[i].Position, origin);
            if (distanceToCurrentEnemy < distanceToClosestEnemy)
            {
                returnedEnemy = enemies[i];
                distanceToClosestEnemy = distanceToCurrentEnemy;
            }
        }
        return returnedEnemy;
    }

    private void CheckCreaturesState(int prevEnemiesCount)
    {
        if (prevEnemiesCount != enemies.Count)
        {
            formation = new HordeFormation(enemies.Count);
            SetTarget(Target.Position);
        }
    }

    private void PerformAttack(EnemyCreature enemyCreature)
    {
        if (GameManager.Instance != null && enemyCreature.EnemyGO != null)
        {
            PlayerTroop troop = GameManager.Instance.GetNearestPlayerTroop(enemyCreature.EnemyGO.transform.position);
            if (troop != null)
            {
                enemyCreature.PerformAttack(troop.Health, troop.Position);
            }
        }
    }
}
