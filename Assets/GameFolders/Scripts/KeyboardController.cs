using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KeyboardController : ControllerInput
{
    private Ray _cameraRay = new Ray();
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
        _cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 temp = Physics.RaycastAll(_cameraRay)
                          .Where(impact => impact.transform.gameObject.CompareTag("Ground"))
                          .Select(donde => donde.point)
                          .FirstOrDefault();
        if(temp!= null)
            _vectorAim = new Vector3(temp.x, 0, temp.z);

        return _vectorAim;
    }

    public override bool InputShoot()
    {
        return Input.GetMouseButton(0);
    }
}
