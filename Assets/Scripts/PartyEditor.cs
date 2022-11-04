using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Party))]
public class PartyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Party myScript = (Party)target;
        if (GUILayout.Button("Load Data"))
        {
            myScript.LoadData();
        }
        if (GUILayout.Button("Update Data"))
        {
            myScript.UpdateData();
        }
    }
}