using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARInteractions : MonoBehaviour
{
    [Header("INTERACTIONS ELEMENTS")]
    public GameObject _imageTarget_1;
    public GameObject _imageTarget_2;
    public GameObject anchorPosition1;
    public GameObject anchorPosition2;
    public GameObject anchorPosition3;
    public GameObject _target;

    private float _targetRotation;
    private float _distanceScaling;
    private float _speedRotation;
    private float _move;
    private float _speed;
    private bool _activeAnimation;
   

    void Start()
    {
        _activeAnimation = true;
        _speed = 1;
        _move = _speed * Time.deltaTime;
    }

    void Update()
    {
        if (_target.GetComponent<Renderer>().isVisible && _activeAnimation == true)
        {
            RotationTarget();
        }
    
        if (anchorPosition3.GetComponent<Renderer>().isVisible)
        {
            DistanceScalingTaget();
        }

        if (anchorPosition1.GetComponent<Renderer>().isVisible)
        {
            MoveTarget();
            _activeAnimation = false;
        }

        else
        {
            _target.transform.position = Vector3.MoveTowards(_target.transform.position, anchorPosition2.transform.position, _move);
            _activeAnimation = true;
        }
    }

    // fonction activant la rotation du Gameobject
    private void RotationTarget()
    {
        Debug.Log("je rotate !");
        _targetRotation = _imageTarget_1.gameObject.transform.rotation.y;
        _speedRotation = _targetRotation * 10;
        GameObject.FindWithTag("target").transform.Rotate(0, _speedRotation, 0);
    }

    // fonction activant la gestion du scaling par rapport à la distance des marqueurs
    private void DistanceScalingTaget()
    {
        Debug.Log("je scale !");
        float _setDistanceScaling = Vector3.Distance(_imageTarget_1.transform.position, _imageTarget_2.transform.position);
        _distanceScaling = _setDistanceScaling / 10;
        _target.transform.localScale = new Vector3(_distanceScaling, _distanceScaling, _distanceScaling);

    }

    // fonction permettant de "migrer" le Gameobject d'un marqueur à un autre
    private void MoveTarget()
    {
        Debug.Log("je migre !");
        _target.transform.position = Vector3.MoveTowards(_target.transform.position, anchorPosition1.transform.position, _move);

    }
     

}
