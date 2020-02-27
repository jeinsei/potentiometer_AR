using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class MeshCombiner : MonoBehaviour
{
    public string _name_of_your_merge;
    public static UnityEngine.Object[] selection;
    public GameObject[] currentSelection;
    private GameObject raw;
    public List<GameObject> _saveMergeData = new List<GameObject>();

    public void SelectGOforMerge()
    {
        ArrayUtility.Add(ref currentSelection, Selection.activeGameObject);
    }

    public void SetGOinEmptyGO()
    {
        raw = new GameObject();
        raw.name = _name_of_your_merge;

        raw.AddComponent<MeshRenderer>();
        raw.AddComponent<MeshFilter>();

        for (int i = 0; i < currentSelection.Length; i++)
        {
            currentSelection[i].transform.parent = raw.transform;
        }

        currentSelection = new GameObject[0];

    }

    public void ResetArray()
    {
        currentSelection = new GameObject[0];

    }

    public void CombineMeshes()
    {
        Quaternion oldRot = raw.transform.rotation; // save old rotation of the gameobject
        Vector3 oldPos = raw.transform.position; // save old position of the gameobject

        raw.transform.rotation = Quaternion.identity; // set 0 in rotation
        raw.transform.position = Vector3.zero; // set 0 in position

        MeshFilter[] filters = raw.GetComponentsInChildren<MeshFilter>(); // stock all mesh children in an array

        Debug.Log(name + " is combining " + filters.Length + " meshes!");

        Mesh finalMesh = new Mesh(); // create an empty mesh for stock all meshes

        CombineInstance[] combiners = new CombineInstance[filters.Length]; // create combine Array and "combine" all mesh


        for (int a = 0; a < filters.Length; a++)
        {
            if (filters[a].transform == transform)
                continue;

            combiners[a].subMeshIndex = 0;
            combiners[a].mesh = filters[a].sharedMesh;
            combiners[a].transform = filters[a].transform.localToWorldMatrix; // set the same position transform between Main object and the mesh combine
        }

        finalMesh.CombineMeshes(combiners); // combine all mesh in empty mesh

        raw.GetComponent<MeshFilter>().sharedMesh = finalMesh; // set all mesh in Mesh component

        raw.transform.rotation = oldRot; // pose Gameobject on native rotation
        raw.transform.position = oldPos; // pose Gameobject on native position

        for( int a = 0; a < raw.transform.childCount; a++) // Enable children of the Main Gameobject
        {

            raw.transform.GetChild(a).gameObject.SetActive(false);
        }
    }

    public void SaveOriginalMesh()
    {
    
        Debug.Log("save original gameobject");
        GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect
        (raw, "Assets/" + _name_of_your_merge + "_original.prefab", InteractionMode.AutomatedAction);

        for (int i = 0; i <= _saveMergeData.Count; i++)
        {
            if (_saveMergeData[i].gameObject == null)
            {
                _saveMergeData.RemoveAt(i);
            }
        }
    }

    public void SaveGameobjectPrefab()
    {
        Debug.Log("save prefab");
        GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect
        (raw, "Assets/" + _name_of_your_merge + "_merge.prefab", InteractionMode.AutomatedAction);
        _saveMergeData.Add(prefab);

    }

}
