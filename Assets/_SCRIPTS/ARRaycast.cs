using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vuforia;

public class ARRaycast : MonoBehaviour
{
    [Header("RAYCAST ELEMENTS")]
    public ImageTargetBehaviour _imageTarget_1;
    public ImageTargetBehaviour _imageTarget_2;

    public GameObject[] _target;
    public Material[] _setMaterial;
    public GameObject _setGOMaterial;
    public GameObject _mainTargetMaterial;
    public GameObject _maintTargetColor;
   

    void Update()
    {
        if (_mainTargetMaterial.GetComponent<Renderer>().isVisible || _maintTargetColor.GetComponent<Renderer>().isVisible)
        {
            SetTargetPosition();
            ChangeTargetRaycast();
        }

        else
        {
            Debug.Log("présenter une carte !");
            
        }

    }

    // fonction de gestion du positionnement des materials sur le marqueur 1
    private void SetTargetPosition()
    {
            _target[0].transform.position = new Vector3(_imageTarget_1.transform.position.x + 0.5f, _imageTarget_1.transform.position.y + 1f, _imageTarget_1.transform.position.z);
            _target[1].transform.position = new Vector3(_imageTarget_1.transform.position.x + 0.5f, _imageTarget_1.transform.position.y - 1f, _imageTarget_1.transform.position.z);
            _target[2].transform.position = new Vector3(_imageTarget_1.transform.position.x - 0.5f, _imageTarget_1.transform.position.y - 1f, _imageTarget_1.transform.position.z);
            _target[3].transform.position = new Vector3(_imageTarget_1.transform.position.x - 0.5f, _imageTarget_1.transform.position.y + 1f, _imageTarget_1.transform.position.z);
           
            _target[4].transform.position = new Vector3(_imageTarget_2.transform.position.x + 0.5f, _imageTarget_2.transform.position.y + 1f, _imageTarget_2.transform.position.z);
            _target[5].transform.position = new Vector3(_imageTarget_2.transform.position.x + 0.5f, _imageTarget_2.transform.position.y - 1f, _imageTarget_2.transform.position.z);
            _target[6].transform.position = new Vector3(_imageTarget_2.transform.position.x - 0.5f, _imageTarget_2.transform.position.y - 1f, _imageTarget_2.transform.position.z);
            _target[7].transform.position = new Vector3(_imageTarget_2.transform.position.x - 0.5f, _imageTarget_2.transform.position.y + 1f, _imageTarget_2.transform.position.z);

    }

    // fonction permettant de set les éléments via raycast
    private void ChangeTargetRaycast()
    {
        // condition permettant via un raycast de changer le material
        if (Physics.Raycast(_mainTargetMaterial.transform.position, _mainTargetMaterial.transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
        {
            Debug.DrawRay(_mainTargetMaterial.transform.position, _mainTargetMaterial.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit Material");


            if (hit.collider.tag == "set0")
            {
                Debug.Log("set0");
                _setGOMaterial = GameObject.FindWithTag("set0");
                _mainTargetMaterial.GetComponent<Animator>().Play("animation_scale");

            }
            if (hit.collider.tag == "set1")
            {
                Debug.Log("set1");
                _setGOMaterial = GameObject.FindWithTag("set1");
                _mainTargetMaterial.GetComponent<Animator>().Play("animation_slider");

            }
            if (hit.collider.tag == "set2")
            {
                Debug.Log("set2");
                _setGOMaterial = GameObject.FindWithTag("set2");
                _mainTargetMaterial.GetComponent<Animator>().Play("animation_mesh");

            }
            if (hit.collider.tag == "set3")
            {
                Debug.Log("set3");
                _setGOMaterial = GameObject.FindWithTag("set3");
                _mainTargetMaterial.GetComponent<Animator>().Play("animation_color");

            }

        }
        // condition permettant via un raycast de changer la couleur
        if (Physics.Raycast(_maintTargetColor.transform.position, _maintTargetColor.transform.TransformDirection(Vector3.forward), out RaycastHit hit2, Mathf.Infinity))
        {
            Debug.DrawRay(_maintTargetColor.transform.position, _maintTargetColor.transform.TransformDirection(Vector3.forward) * hit2.distance, Color.yellow);
            Debug.Log("Did Hit Color");

            if (hit2.collider.tag == "set4")
            {
                Debug.Log("set4");
                _setGOMaterial.GetComponent<Renderer>().material = _setMaterial[0];

            }
            if (hit2.collider.tag == "set5")
            {
                Debug.Log("set5");
                _setGOMaterial.GetComponent<Renderer>().material = _setMaterial[1];


            }
            if (hit2.collider.tag == "set6")
            {
                Debug.Log("set6");
                _setGOMaterial.GetComponent<Renderer>().material = _setMaterial[2];

            }
            if (hit2.collider.tag == "set7")
            {
                Debug.Log("set7");
                _setGOMaterial.GetComponent<Renderer>().material = _setMaterial[3];

            }
        }

        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }

}