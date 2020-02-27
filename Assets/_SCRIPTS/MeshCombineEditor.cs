using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshCombiner))]
public class MeshCombineEditor : Editor
{
    // Creation des buttons de l'interface pour la gestion du workflow de merge mesh dans l'Editor
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MeshCombiner mc = target as MeshCombiner;

        EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Set GameObject for merge"))
            {
                // set les gameObjects selectionnés qui seront mergé dans un tableau
                mc.SelectGOforMerge();
            }

            if (GUILayout.Button("Reset Selection"))
            {
                // Reset toutes les variables
                mc.ResetArray();
            }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Set Selection in GameObject "))
            {
                // set les gameObjects du tableau dans un emptyGameObject
                mc.SetGOinEmptyGO();
            }

            if (GUILayout.Button("Save Selection"))
            {
                // Sauvegarde le MainGameObject avant la fusion
                mc.SaveOriginalMesh();
            }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Merge GameObject Selection"))
            {
                // Sauvegarde le MainGameObject avant la fusion
                mc.CombineMeshes();
            }

            if (GUILayout.Button("Save Mergging"))
            {
                // Sauvegarde Le MainGameObject après la fusion
                mc.SaveGameobjectPrefab();
            }

            GUILayout.EndHorizontal();

    }
}
