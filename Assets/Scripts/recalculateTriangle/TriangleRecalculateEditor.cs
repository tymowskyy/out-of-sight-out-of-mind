using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TriangleRecalculate))]
public class TriangleRecalculateEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TriangleRecalculate script = (TriangleRecalculate)target;
        if(GUILayout.Button("Recalculate"))
        {
            script.Recalculate();
        }
    }
}
