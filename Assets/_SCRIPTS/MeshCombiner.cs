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
    public GameObject _raw;

    [Header("3 LIST DES ÉLÉMENTS MERGÉ SAUVEGARDÉ")]
    public List<GameObject> _saveMergeData = new List<GameObject>();
    public static string _nameMerge;


    // set les gameObjects selectionnés qui seront mergé dans un tableau
    public void SelectGOforMerge()
    {
        Debug.Log("set gameObject in array");
        ArrayUtility.Add(ref _currentSelection, Selection.activeGameObject);

        if(!Directory.Exists(Application.dataPath + "_OIGINAL") && !Directory.Exists(Application.dataPath + "_MERGE_MESH"))
        {
            Debug.Log("nous crééons vos dossiers!");
            var _original = Directory.CreateDirectory(Application.dataPath + "/_ORIGINAL");
            var _merge_mesh =Directory.CreateDirectory(Application.dataPath + "/_MERGE_MESH");
        }
        else
        {
            Debug.Log("vos dossiers sont déjà créés !");
        }
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

        MeshFilter[] meshFilters = _raw.GetComponentsInChildren<MeshFilter>();

        Mesh finalMesh = new Mesh(); // create an empty mesh for stock all meshe
       
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
       
        Debug.Log(name + " is combining " + meshFilters.Length + " meshes!");

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        finalMesh.CombineMeshes(combine); // combine all mesh in empty mesh

        _raw.GetComponent<MeshFilter>().sharedMesh = finalMesh;

        _raw.transform.rotation = oldRot;
        _raw.transform.position = oldPos;

        _raw.transform.gameObject.SetActive(true);

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
