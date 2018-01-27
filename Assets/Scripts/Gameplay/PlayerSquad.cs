using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquad : MonoBehaviour
{
    [SerializeField]
    private Transform troopPrefab;

    private List<PlayerTroop> Troops;
    private BaseSquadFormation Formation;
    private TargetComponent Target;

    private void Update()
    {
        if (Troops != null)
        {
            int prevTroopsCount = Troops.Count;
            for (int i = 0; i < Troops.Count; i++)
            {
                PerformAttack(Troops[i]);
                UpdateInfectionState(Troops[i]);
                if(!Troops[i].IsAlive())
                {
                    Destroy(Troops[i].GameObject);
                    Troops.RemoveAt(i);
                    i--;
                }
            }
            CheckTroopsState(prevTroopsCount);
        }
    }

    public void Init(int troopsCount)
    {
        ClearExistingData();
        Target = new TargetComponent();
        CreateFormation(troopsCount);
        InstantiateTroops(troopsCount);
    }
	
	public void UpdateTarget(Vector2 target)
    {
        if (Troops != null && Formation != null && Target != null)
        {
            Target.Position = target;
            Formation.CalculatePosition(Target.Position);
            for (int i = 0; i < Troops.Count; i++)
            {
                Troops[i].UpdatePosition(Formation.GetPositionOfTroop(i, Troops[i].Position));
            }
        }
	}

    public Vector2 GetSquadTarget()
    {
        if (Target != null)
        {
            return Target.Position;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public PlayerTroop GetNearestTroop(Vector2 origin, float range)
    {
        PlayerTroop nearestTroop = null;
        float distanceToClosest = float.MaxValue;
        for (int i = 0; i < Troops.Count; i++)
        {
            float distanceToCurrent = Vector3.Distance(Troops[i].Position, origin);
            if (distanceToCurrent < distanceToClosest &&
                distanceToCurrent < range)
            {
                nearestTroop = Troops[i];
                distanceToClosest = distanceToCurrent;
            }
        }
        return nearestTroop;
    }

    public void KillTroopWithInfection(int troopIndex)
    {
        if (Troops != null)
        {
            PlayerTroop troopToKill = Troops.Find((t) => t.Index.Equals(troopIndex));
            if(troopToKill != null)
            {
                troopToKill.IsDead = true;
                troopToKill.Health.CurrentHealth = 0;
            }
        }
    }

    public void InfectRandomTroop()
    {
        if (Troops != null)
        {
            PlayerTroop troopToInfect = Troops.Find((t) => !t.IsInfected && !t.IsDead);
            if(troopToInfect != null)
            {
                troopToInfect.IsInfected = true;
                GameManager.Instance.ShowInfectedTroop(troopToInfect.Index);
                troopToInfect.ChangeColorToInfected();
            }
        }
    }

    public void DoubleDamage()
    {
        if (Troops != null)
        {
            for (int i = 0; i < Troops.Count; i++)
            {
                if (!Troops[i].IsDead)
                {
                    Troops[i].Attack.DoubleDamage();
                }
            }
        }
    }

    public void CureAll()
    {
        if (Troops != null)
        {
            for (int i = 0; i < Troops.Count; i++)
            {
                if (!Troops[i].IsDead && Troops[i].IsInfected)
                {
                    Troops[i].Health.Cure();
                    Troops[i].IsInfected = false;
                    GameManager.Instance.HideInfectedTroop(Troops[i].Index);
                    Troops[i].ChangeColorToNormal();
                }
            }
        }
    }

    private void ClearExistingData()
    {
        if (Troops != null)
        {
            for (int i = 0; i < Troops.Count; i++)
            {
                Destroy(Troops[i].GameObject);
            }
        }
    }

    private void PerformAttack(PlayerTroop playerTroop)
    {
        if (EnemiesManager.Instance != null && playerTroop.Attack.IsAttackPossible())
        {
            EnemyCreature enemy = EnemiesManager.Instance.GetNearestEnemy(playerTroop.GameObject.transform.position,
                                                                          playerTroop.Attack.Range);
            if(enemy != null)
            {
                playerTroop.PerformAttack(enemy.Health, enemy.Position);
            }
        }
    }

    private void InstantiateTroops(int troopsCount)
    {
        Troops = new List<PlayerTroop>();
        for (int i = 0; i < troopsCount; i++)
        {
            Transform trans = Instantiate<Transform>(troopPrefab, transform.position, troopPrefab.rotation, transform);
            Troops.Add(new PlayerTroop(trans.gameObject, i, Formation.GetPositionOfTroop(i, transform.position),
                                       SettingsService.GameSettings.Player));
        }
    }

    private void CreateFormation(int troopsCount)
    {
        if(troopsCount > 0)
        {
            int columns = troopsCount / Mathf.FloorToInt(Mathf.Sqrt(troopsCount));
            Formation = new RectFormation(troopsCount, columns);
            Formation.CalculatePosition(Target.Position);
        }
    }

    private void CheckTroopsState(int prevTroopsCount)
    {
        if(prevTroopsCount != Troops.Count)
        {
            if(Troops.Count > 0)
            {
                CreateFormation(Troops.Count);
                UpdateTarget(Target.Position);
            }
            else
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    private void UpdateInfectionState(PlayerTroop playerTroop)
    {
        if(GameManager.Instance != null && playerTroop.ShouldBeInfected() &&
           !playerTroop.IsInfected)
        {
            playerTroop.IsInfected = true;
            GameManager.Instance.ShowInfectedTroop(playerTroop.Index);
            playerTroop.ChangeColorToInfected();
        }
    }
}
