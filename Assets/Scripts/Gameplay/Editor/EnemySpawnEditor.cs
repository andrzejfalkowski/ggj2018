using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySpawn))]
public class EnemySpawnEditor : Editor
{
    [SerializeField]
    private string numberOfTroopsString;
    [SerializeField]
    private string wavePeriodString;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EnemySpawn myTarget = (EnemySpawn)target;

        GUILayout.Label("Number of enemies:");
        numberOfTroopsString = GUILayout.TextField(numberOfTroopsString);
        GUILayout.Label("Wave period:");
        wavePeriodString = GUILayout.TextField(wavePeriodString);
        if (GUILayout.Button("Initialize"))
        {
            int amountOfEnemies = 0, wavePeriod = 0;
            if (int.TryParse(numberOfTroopsString, out amountOfEnemies) &&
                int.TryParse(wavePeriodString, out wavePeriod))
            {
                myTarget.Init(amountOfEnemies, wavePeriod, 0);
            }
        }
    }
}
