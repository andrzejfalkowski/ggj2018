using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquad : MonoBehaviour
{
    [SerializeField]
    private Transform troopPrefab;

    private List<PlayerTroop> Troops;
    private BaseSquadFormation Formation;
    private SquadTarget Target;

    public void Init(int troopsCount)
    {
        ClearExistingData();
        Target = new SquadTarget();
        int columns = troopsCount / Mathf.FloorToInt(Mathf.Sqrt(troopsCount));
        Formation = new RectFormation(troopsCount, columns);
        Formation.CalculatePosition(Target.Position);
        Troops = new List<PlayerTroop>();
        for (int i = 0; i < troopsCount; i++)
        {
            Transform trans = Instantiate<Transform>(troopPrefab, transform.position, troopPrefab.rotation, transform);
            Troops.Add(new PlayerTroop(trans.gameObject, i, Formation.GetPositionOfTroop(i)));
        }
    }
	
	public void UpgadeTarget(Vector2 target)
    {
        if (Troops != null && Formation != null && Target != null)
        {
            Target.Position = target;
            Formation.CalculatePosition(Target.Position);
            for (int i = 0; i < Troops.Count; i++)
            {
                Troops[i].UpdatePosition(Formation.GetPositionOfTroop(i));
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

    private void ClearExistingData()
    {
        if (Troops != null && Formation != null && Target != null)
        {
            for (int i = 0; i < Troops.Count; i++)
            {
                Destroy(Troops[i].TroopGO);
            }
        }
    }
}
