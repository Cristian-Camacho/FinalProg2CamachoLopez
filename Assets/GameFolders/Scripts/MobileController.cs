using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : ControllerInput
{
    public Stick stick;
    public Stick stickAim;
    private float angle;
    private Vector3 moveVector;
    private Vector3 rotateEulerAngles;


    public MobileController(float speed)
    {
        _speed = speed;
        stick = GameCanvasController.instance.moveStick;
        stickAim = GameCanvasController.instance.aimStick;
    }

    public override Vector3 CheckMovement()
    {
        moveVector = Vector3.zero;
        moveVector += Vector3.right * stick.stickValue.normalized.x * _speed * Time.deltaTime;
        moveVector += Vector3.forward * stick.stickValue.normalized.y * _speed * Time.deltaTime;
        return moveVector;
    }

    public override Vector3 CheckRotation()
    {
        if (!stickAim.rotateModel) return Vector3.zero;
        rotateEulerAngles = Vector3.zero;
        if (stickAim.stickValue != Vector3.zero)
            angle = (Mathf.Atan2(stickAim.stickValue.x, -stickAim.stickValue.y) * 180 / Mathf.PI) * -1f;

        rotateEulerAngles = new Vector3(0, angle, 0);
        return rotateEulerAngles;
    }

    public override bool InputShoot()
    {
        return stickAim.inTouch;
    }
}
