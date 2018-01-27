using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSquadFormation
{
    protected List<Vector2> Slots;

    public abstract void CalculatePosition(Vector2 targetPos);

    public virtual Vector2 GetPositionOfTroop(int index, Vector2 currentPos)
    {
        return index < Slots.Count ? Slots[index] : currentPos;
    }
}
