using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Platform))]
public class PlatformEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Platform myScript = (Platform)target;

        if(GUILayout.Button("Generate Platform"))
        {
            myScript.DestroyExistingTiles();
            myScript.GeneratePlatform();
        }
    }
}
