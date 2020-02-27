using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class InstanceManager : MonoBehaviour
{
    public GameObject _selectInstanceGO;

    void OnGUI()
    {
        Event e = new Event();

        while (Event.PopEvent(e))
        {
            if (e.rawType == EventType.MouseDown)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(e.mousePosition.x, Screen.height - e.mousePosition.y, 0));

                if (Physics.Raycast(ray, out hit))
                {
                    if(_selectInstanceGO == null)
                    {
                        Debug.Log("selectionner un gameObject");
                    }
                    else
                    {
                        Instantiate(_selectInstanceGO, hit.point, Quaternion.identity);
                        Debug.Log(hit.point);
                    }
                  
                 }
            }
        }
    }
}
