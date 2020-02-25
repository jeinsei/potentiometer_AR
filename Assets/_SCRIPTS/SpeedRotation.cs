using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedRotation : MonoBehaviour
{
    [Header("rotation")]
    public GameObject _target;
    public GameObject _settings;
    public float _speedRotation;
    [Header("Scaling")]
    public Transform _scaling1;
    public Transform _scaling2;
    public float _distanceScaling;
    [Header("On_Off")]
    public Renderer _renderGO;
    public float _speed;
    public Vector3 _initPosition;
    [Header("Select Color")]
    public GameObject _selectObject;
    public Material[] _setMaterial;
    [Header("Select GameObject")]
    public GameObject _selectGO;
    public GameObject _targetGO;
    [Header("Slider selection")]
    public Slider _sliderColor;
    public Slider _sliderTarget;
    public Slider _sliderRotation;



    void Start()
    {
        _target.GetComponent<Renderer>().material = _setMaterial[4];
        _initPosition = _target.transform.position;
      
    }

    void Update()
    {
       // RotationTarget();
       // DistanceScaling();
       // VisibleObject();
        ChangeColor();
        ChangeTarget();

    }


    public void RotationTarget()
    {
        float _targetRotation = _settings.gameObject.transform.rotation.y;
        _speedRotation = _targetRotation * 10;
        GameObject.FindWithTag("target").transform.Rotate(0, _speedRotation, 0);
    }

    public void DistanceScaling()
    {
        float _setDistanceScaling = Vector3.Distance(_scaling1.position, _scaling2.transform.position);
        _distanceScaling = _setDistanceScaling / 3;
        _target.transform.localScale = new Vector3(_distanceScaling, _distanceScaling, _distanceScaling);

    }

    public void VisibleObject()
    {
        float move = _speed * Time.deltaTime; // calculate distance to move
        if (_renderGO.isVisible)
        {
            _target.transform.position = Vector3.MoveTowards(_target.transform.position, _renderGO.transform.position, move);
        }
        else
        {
            _target.transform.position = Vector3.MoveTowards(_target.transform.position, _initPosition, move);
        }

    }

    public void ChangeColor()
    {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(_selectObject.transform.position, _selectObject.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(_selectObject.transform.position, _selectObject.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
           
             var targetRenderer = _targetGO.GetComponent<Renderer>();

            if (hit.collider.tag == "vert")
            {
                print("vert");
                _targetGO.GetComponent<Renderer>().material = _setMaterial[0];
            }
            if (hit.collider.tag == "bleu")
            {
                print("bleu");
                _targetGO.GetComponent<Renderer>().material = _setMaterial[1];
            }
            if (hit.collider.tag == "jaune")
            {
                print("jaune");
                _targetGO.GetComponent<Renderer>().material = _setMaterial[2];
            }
            if (hit.collider.tag == "violet")
            {
                print("violet");
                _targetGO.GetComponent<Renderer>().material = _setMaterial[3];
            }
           
        }
        else
        {
            _targetGO.GetComponent<Renderer>().material = _setMaterial[4];
            Debug.DrawRay(_selectObject.transform.position, _selectObject.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    public void ChangeTarget()
    {
        RaycastHit hit;
       
        if (Physics.Raycast(_selectGO.transform.position, _selectGO.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(_selectGO.transform.position, _selectGO.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");


            if (hit.collider.tag == "set0")
            {
                _targetGO = GameObject.FindWithTag("set0");
               
            }
            if (hit.collider.tag == "set1")
            {
                _targetGO = GameObject.FindWithTag("set1");
               
            }
            if (hit.collider.tag == "set2")
            {
                _targetGO = GameObject.FindWithTag("set2");
             
            }
            if (hit.collider.tag == "set3")
            {
                _targetGO = GameObject.FindWithTag("set3");

            }

        }
        else
        {
            Debug.DrawRay(_selectObject.transform.position, _selectObject.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }


    public void SliderColor()
    {
        _selectObject.transform.rotation = Quaternion.Euler(0, _sliderColor.value * 360, 0);
    }
   
    public void SliderTarget()
    {
        _target.transform.rotation = Quaternion.Euler(0, _sliderTarget.value * 360, 0);
    }

    public void SliderRotation()
    {
        _settings.transform.rotation = Quaternion.Euler(0, _sliderRotation.value * 360, 0);
    }
}

