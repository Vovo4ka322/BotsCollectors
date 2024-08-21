using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private Flag _flag;
    private Camera _camera;
    private bool _isSet = false;

    public event Action<Flag> Put;

    private void Awake()
    {
        Debug.Log(_isSet + "Awake");
        _camera = Camera.main;
        _flag = this;
    }

    private void Update()
    {
        Debug.Log(_isSet + "up1");
        if (_flag != null)
        {
            var ground = new Plane(Vector3.up, Vector3.zero);

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Debug.Log(_isSet + "up1.2");
            if (ground.Raycast(ray, out float position))
            {
                Debug.Log(_isSet + "up1.3");
                Vector3 worldPososition = ray.GetPoint(position);

                _flag.transform.position = worldPososition;

                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log(_isSet + "up2");
                    if (_isSet)
                    {
                        if (_flag.gameObject.TryGetComponent<Platform>(out Platform platform))
                        {
                            Debug.Log(_isSet + "up3");
                            _flag = null;
                            Put?.Invoke(_flag);
                            _isSet = false;
                            Debug.Log(_isSet + "up4");
                        }
                    }
                }
            }
        }
    }

    public void SetFlag()
    {
        if (_isSet != true)
        {
            _flag = Instantiate(this);
            Debug.Log(_isSet + "noup1");
            _isSet = true;
            Debug.Log(_isSet + "noup2");
        }
    }
}
