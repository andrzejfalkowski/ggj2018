using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerSquad))]
public class PlayerSquadEditor : Editor
{
    [SerializeField]
    private string numberOfTroopsString;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlayerSquad myTarget = (PlayerSquad)target;

        GUILayout.Label("Number of troops:");
        numberOfTroopsString = GUILayout.TextField(numberOfTroopsString);
        if (GUILayout.Button("Initialize"))
        {
            int result = 0;
            if(int.TryParse(numberOfTroopsString, out result))
            {
                myTarget.Init(result);
            }
        }
    }
}
