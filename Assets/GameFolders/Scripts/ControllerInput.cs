using UnityEngine;

public abstract class ControllerInput
{
    protected float _speed;

    public abstract Vector3 CheckMovement();
    public abstract Vector3 CheckRotation();
    public abstract bool InputShoot();
}
