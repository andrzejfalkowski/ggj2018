using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectFormation : BaseSquadFormation
{
    private int troopsCount;
    private int numberOfColumns;

    public RectFormation(int _troopsCount, int _columns)
    {
        troopsCount = _troopsCount;
        numberOfColumns = _columns;
    }

    public override void CalculatePosition(Vector2 targetPos)
    {
        throw new NotImplementedException();
    }

    public override Vector2 GetPositionOfTroop(int index)
    {
        throw new NotImplementedException();
    }
}
