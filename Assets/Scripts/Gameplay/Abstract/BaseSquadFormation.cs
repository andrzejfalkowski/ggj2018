using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSquadFormation
{
    public abstract void CalculatePosition(Vector2 targetPos);
    public abstract Vector2 GetPositionOfTroop(int index);
}
