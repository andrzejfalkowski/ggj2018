using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RectFormation : BaseSquadFormation
{
    private float distanceBetweenTroops = 0.5f;
    private float samplePositionRange = 3;

    private int troopsCount;
    private int numberOfColumns;

    private List<Vector2> Slots;

    public RectFormation(int _troopsCount, int _columns)
    {
        troopsCount = _troopsCount;
        numberOfColumns = _columns;
        Slots = new List<Vector2>();
    }

    public override void CalculatePosition(Vector2 targetPos)
    {
        Slots.Clear();
        int width = Mathf.CeilToInt(troopsCount / numberOfColumns);
        int lastColumnWidth = width * numberOfColumns;
        lastColumnWidth = troopsCount - lastColumnWidth;
        for (int i = 0; i < troopsCount; i++)
        {
            int currentWidth = troopsCount - i > lastColumnWidth ? width : lastColumnWidth;
            float xOffset = ((i % currentWidth) - (currentWidth - 1) * 0.5f) * distanceBetweenTroops;
            int currentColumn = Mathf.FloorToInt(i / width);
            float yOffset = (currentColumn - (numberOfColumns - 1) * 0.5f) * distanceBetweenTroops;
            Vector2 newPos = new Vector2(targetPos.x + xOffset, targetPos.y + yOffset);
            newPos = ValidatePosition(newPos);
            Slots.Add(newPos);
        }
    }

    private Vector2 ValidatePosition(Vector2 newPos)
    {
        NavMeshHit hitInfo;
        if (NavMesh.SamplePosition(newPos, out hitInfo, samplePositionRange, int.MaxValue))
        {
            if(newPos == (Vector2)hitInfo.position)
            {
                return hitInfo.position;
            }
            else
            {
                return newPos + new Vector2(UnityEngine.Random.Range(0, distanceBetweenTroops * 0.5f),
                                            UnityEngine.Random.Range(0, distanceBetweenTroops * 0.5f));
            }
        }
        return newPos;
    }

    public override Vector2 GetPositionOfTroop(int index)
    {
        return index < Slots.Count ? Slots[index] : Vector2.zero;
    }
}
