using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquad : MonoBehaviour
{
    private List<PlayerTroop> Troops;
    private BaseSquadFormation Formation;
    private SquadTarget Target;

    public void Init(int troopsCount, GameObject troopPrefab)
    {
        Target = new SquadTarget();
        int columns = troopsCount / Mathf.FloorToInt(Mathf.Sqrt(troopsCount));
        Formation = new RectFormation(troopsCount, columns);
        Formation.CalculatePosition(Target.Position);
        Troops = new List<PlayerTroop>();
        for (int i = 0; i < troopsCount; i++)
        {
            GameObject go = Instantiate<GameObject>(troopPrefab);
            go.transform.SetParent(transform);
            Troops.Add(new PlayerTroop(go, i, Formation.GetPositionOfTroop(i)));
        }
    }
	
	private void Update ()
    {
        for(int i = 0; i < Troops.Count; i++)
        {
            Troops[i].UpdatePosition(Formation.GetPositionOfTroop(i));
        }
	}
}
