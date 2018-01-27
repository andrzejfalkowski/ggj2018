using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectFormation : BaseSquadFormation
{
    private float distanceBetweenTroops { get { return SettingsService.GameSettings.RectFormation_distanceBetweenTroops; } }

    private int troopsCount;
    private int numberOfColumns;

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
            newPos = NavigationHelper.ValidatePosition(newPos, distanceBetweenTroops);
            Slots.Add(newPos);
        }
    }
}
