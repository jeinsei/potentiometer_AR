using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class InstanceManager : MonoBehaviour
{
    public GameObject _selectInstanceGO;
    bool _activateScript;

    // gestion du click souris en mode Editor pour instantié les différents objets
    void OnGUI()
    {
        Event e = new Event();

        while (Event.PopEvent(e))
        {
            if(_activateScript == true) 
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

    // activation du boolean
    public void BoolOn()
    {
        _activateScript = true;
    }

    // desactivation du boolean
    public void BoolOff()
    {
        _activateScript = false;
    }
}
