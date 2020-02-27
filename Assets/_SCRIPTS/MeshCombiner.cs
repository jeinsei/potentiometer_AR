using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.IO;

[ExecuteInEditMode]
public class MeshCombiner : MonoBehaviour
{
    [Header("1 DÉFINISSEZ LE NOM DE VOTRE MERGE")]
    public string _name_of_your_merge;

    [Header("2 SÉLECTIONNEZ LES ÉLÉMENTS A MERGER")]
    public GameObject[] _currentSelection;
    private GameObject _raw;

    [Header("3 LIST DES ÉLÉMENTS MERGÉ SAUVEGARDÉ")]
    public List<GameObject> _saveMergeData = new List<GameObject>();
    public static string _nameMerge;


    // set les gameObjects selectionnés qui seront mergé dans un tableau
    public void SelectGOforMerge()
    {
        Debug.Log("set gameObject in array");
        ArrayUtility.Add(ref _currentSelection, Selection.activeGameObject);
        var _original = Directory.CreateDirectory(Application.dataPath + "/_ORIGINAL");
        var _merge_mesh =Directory.CreateDirectory(Application.dataPath + "/_MERGE_MESH");
    }

    // set les gameObjects du tableau dans un emptyGameObject
    public void SetGOinEmptyGO()
    {
        Debug.Log("set gameObject in emptyGO");
        _raw = new GameObject();
        _raw.name = _name_of_your_merge;

        _raw.AddComponent<MeshRenderer>();
        _raw.AddComponent<MeshFilter>();

        for (int i = 0; i < _currentSelection.Length; i++)
        {
            _currentSelection[i].transform.parent = _raw.transform;
        }

        _currentSelection = new GameObject[0];

    }
   
    // Reset toutes les variables
    public void ResetArray()
    {
        Debug.Log("reset all datas");
        _currentSelection = new GameObject[0];
        _saveMergeData.Clear();
        _name_of_your_merge = "UNTITLED";
    }

    // Combine les différents enfants de L'emptyGameObject
    public void CombineMeshes()
    {
        Quaternion oldRot = _raw.transform.rotation; // save old rotation of the gameobject
        Vector3 oldPos = _raw.transform.position; // save old position of the gameobject

        _raw.transform.rotation = Quaternion.identity; // set 0 in rotation
        _raw.transform.position = Vector3.zero; // set 0 in position

        MeshFilter[] filters = _raw.GetComponentsInChildren<MeshFilter>(); // stock all mesh children in an array

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

        _raw.GetComponent<MeshFilter>().sharedMesh = finalMesh; // set all mesh in Mesh component

        _raw.transform.rotation = oldRot; // pose Gameobject on native rotation
        _raw.transform.position = oldPos; // pose Gameobject on native position

        for( int a = 0; a < _raw.transform.childCount; a++) // Enable children of the Main Gameobject
        {
            _raw.transform.GetChild(a).gameObject.SetActive(false);
        }
    }

    // Sauvegarde le MainGameObject avant la fusion dans le dossier _ORIGINAL
    public void SaveOriginalMesh()
    {
        Debug.Log("save original prefab");
        GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect
        (_raw, "Assets/_ORIGINAL/" + _name_of_your_merge + "_original.prefab", InteractionMode.AutomatedAction);
       
       for (int i = 0; i < _saveMergeData.Count; i++)
        {
            if (_saveMergeData[i].gameObject == null)
            {
                _saveMergeData.RemoveAt(i);
            }

        }
    }

    // Sauvegarde Le MainGameObject après la fusion dans le dossier _MERGE_MESH
    public void SaveGameobjectPrefab()
    {
        Debug.Log("save prefab");
        GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect
        (_raw, "Assets/_MERGE_MESH/" + _name_of_your_merge + "_merge.prefab", InteractionMode.AutomatedAction);
        _saveMergeData.Add(prefab);
        Debug.Log(_nameMerge);
    }

}
