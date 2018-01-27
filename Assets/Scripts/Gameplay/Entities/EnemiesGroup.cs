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
        //TODO when enemy sees the solidier, should go directly to him instead going to the group target
        Target.Position = target;
        formation.CalculatePosition(target);
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].UpdatePosition(formation.GetPositionOfTroop(i, enemies[i].Position));
        }
    }

    public EnemyCreature GetNearestEnemy(EnemyCreature nearestEnemy, Vector2 origin, float range)
    {
        EnemyCreature returnedEnemy = nearestEnemy;
        float distanceToClosestEnemy = nearestEnemy != null ? Vector3.Distance(nearestEnemy.Position, origin) : float.MaxValue;
        for (int i = 0; i < enemies.Count; i++)
        {
            float distanceToCurrentEnemy = Vector3.Distance(enemies[i].Position, origin);
            if (distanceToCurrentEnemy < distanceToClosestEnemy && 
                distanceToCurrentEnemy < range)
            {
                RaycastHit hitInfo;
                Vector3 origin3D = new Vector3(origin.x, origin.y, -0.25f);
                Vector3 enemyPos3D = new Vector3(enemies[i].Position.x, enemies[i].Position.y, -0.25f);
                Ray ray = new Ray(origin3D, Vector3.Normalize(enemyPos3D - origin3D));
                int maskLayer = 1 << LayerMask.NameToLayer("Blockers");
                if (!Physics.Raycast(ray, out hitInfo, distanceToCurrentEnemy, maskLayer))
                {
                    returnedEnemy = enemies[i];
                    distanceToClosestEnemy = distanceToCurrentEnemy;
                }
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
        if (GameManager.Instance != null && enemyCreature.EnemyGO != null &&
            enemyCreature.Attack.IsAttackPossible())
        {
            PlayerTroop troop = GameManager.Instance.GetNearestPlayerTroop(enemyCreature.EnemyGO.transform.position,
                                                                           enemyCreature.Attack.Range);
            if (troop != null)
            {
                enemyCreature.PerformAttack(troop.Health, troop.Position);
            }
        }
    }
}
