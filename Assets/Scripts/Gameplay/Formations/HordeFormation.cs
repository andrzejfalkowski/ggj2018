using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeFormation : BaseSquadFormation
{
    private float distanceBetweenTroops {get { return SettingsService.GameSettings.HordeFormation_distanceBetweenTroops; } }
    private float groupSpreadingFactor { get { return SettingsService.GameSettings.HordeFormation_speadingFactor; } }


    private int troopsCount;

    public HordeFormation(int _troopsCount)
    {
        troopsCount = _troopsCount;
        Slots = new List<Vector2>();
    }

    public override void CalculatePosition(Vector2 targetPos)
    {
        Slots.Clear();
        float hordeRange = distanceBetweenTroops * troopsCount * groupSpreadingFactor;
        for (int i = 0; i < troopsCount; i++)
        {
            float xOffset = Random.Range(0, hordeRange);
            float yOffset = Random.Range(0, hordeRange);
            Vector2 newPos = new Vector2(targetPos.x + xOffset, targetPos.y + yOffset);
            newPos = NavigationHelper.ValidatePosition(newPos, distanceBetweenTroops);
            Slots.Add(newPos);
        }
    }
}
