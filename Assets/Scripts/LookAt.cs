using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class LookAt : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] private float _xAngle = 90;
    [SerializeField] private float _yAngle = 90;
    [SerializeField] private float _zAngle = 0;

    private float _clickDistance = 100;
    private LayerMask _layerMaskClick;

    private void Awake()
    {
        _layerMaskClick = LayerMask.GetMask("UI");
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit RaycastHit;

        if (Physics.Raycast(ray, out RaycastHit, _clickDistance, _layerMaskClick))
        {
            transform.LookAt(RaycastHit.point);
            transform.Rotate(_xAngle, 0, 0);
            transform.Rotate(0, _yAngle, 0);
            transform.Rotate(0, 0, _zAngle);
        }
    }
}
