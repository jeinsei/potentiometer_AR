using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshCombiner))]
public class MeshCombineEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MeshCombiner mc = target as MeshCombiner;

        EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Set GameObject for merge"))
            {
                mc.SelectGOforMerge();
            }

            if (GUILayout.Button("Reset Selection"))
            {
                mc.ResetArray();
            }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Set Selection in GameObject "))
            {
                mc.SetGOinEmptyGO();
            }

            if (GUILayout.Button("Save Selection"))
            {
                mc.SaveOriginalMesh();
            }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Merge GameObject Selection"))
        {
            mc.CombineMeshes();
        }

        if (GUILayout.Button("Save Mergging"))
        {
            mc.SaveGameobjectPrefab();
        }

        GUILayout.EndHorizontal();

    }
}
