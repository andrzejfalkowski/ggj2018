using UnityEngine;
using UnityEngine.AI;

public static class NavigationHelper
{
    private static float samplePositionRange = 3;

    public static Vector2 ValidatePosition(Vector2 newPos, float distanceBetweenTroops)
    {
        NavMeshHit hitInfo;
        if (NavMesh.SamplePosition(newPos, out hitInfo, samplePositionRange, int.MaxValue))
        {
            if (newPos == (Vector2)hitInfo.position)
            {
                return hitInfo.position;
            }
            else
            {
                return newPos + new Vector2(Random.Range(0, distanceBetweenTroops * 0.5f),
                                            Random.Range(0, distanceBetweenTroops * 0.5f));
            }
        }
        return newPos;
    }

}