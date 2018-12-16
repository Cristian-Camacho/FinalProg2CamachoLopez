using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : ControllerInput
{
    private Ray _cameraRay = new Ray();
    private RaycastHit _hit;
    private Vector3 _vectorMovement;
    private Vector3 _vectorAim;

    public KeyboardController(float speed)
    {
        _speed = speed;
    }

    public override Vector3 CheckMovement()
    {
        _vectorMovement = Vector3.zero;
        _vectorMovement += Vector3.right * Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        _vectorMovement += Vector3.forward * Input.GetAxis("Vertical") * _speed * Time.deltaTime;

        return _vectorMovement;
    }

    public override Vector3 CheckRotation()
    {
        _vectorAim = Vector3.zero;
        if (Input.mousePosition != null)
            _cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_cameraRay, out _hit))
        {
            if (_hit.transform.gameObject.CompareTag("Ground"))
                _vectorAim = new Vector3(_hit.point.x, 0, _hit.point.z);
        }
        return _vectorAim;
    }

    public override bool InputShoot()
    {
        return Input.GetMouseButton(0);
    }
}
