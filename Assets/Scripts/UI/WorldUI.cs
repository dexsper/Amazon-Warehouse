using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class WorldUI : MonoBehaviour
{ 
    private Vector3 _offset; 

    private RectTransform _rectTransform;
    private Transform _target;
    private Camera _camera;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>(); 
    }

    public void SetData(Transform target, Camera camera, Vector3 offset)
    {
        _target = target;
        _camera = camera;
        _offset = offset;
    }

    private void Update()
    {
        if(_target != null && _camera != null)
        {
            _rectTransform.position = _camera.WorldToScreenPoint(_target.position + _offset);

            float distance = 1 / Vector3.Distance(_camera.transform.position, _target.transform.position) * 2f;

            distance = Mathf.Clamp(distance, .5f, 1.0f);
            _rectTransform.transform.localScale = new Vector3(distance, distance, 0);
        }
    }
}
