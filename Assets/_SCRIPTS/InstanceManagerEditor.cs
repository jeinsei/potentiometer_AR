using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InstanceManager))]
public class InstanceManagerEditor : Editor
{
    InstanceManager _instanceManager;

    // Creation des buttons de l'interface pour activer désactiver l'instansiation des objets dans l'Editor
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        InstanceManager im = target as InstanceManager;

        var style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.black;
        Color _onButton = GUI.color;
        GUI.color = Color.green;

        // activation du boolean
        if (GUILayout.Button("Enable Instance in Editor",style))
        {
            Debug.Log("vous avez activé le plugin !");
            im.BoolOn();
        }

        GUI.color = _onButton;
        Color _offButton = GUI.color;
        GUI.color = Color.red;

        // desactivation du boolean
        if (GUILayout.Button("Disable Instance in Editor",style))
        {
            Debug.Log("vous avez désactivé le plugin !");
            im.BoolOff();
        }

        GUI.color = _offButton;
    }
}
