using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entidad, IObservable
{
    public GameObject model;
    public Vector3 startPos;
    private List<IObserver> _observers;
    private ControllerInput _controllerInput;

    public override void Start()
    {
        base.Start();
        startPos = this.transform.position;
        if (SystemInfo.deviceType == DeviceType.Handheld)
            _controllerInput = new MobileController(speed);
        else
            _controllerInput = new KeyboardController(speed);
        Subscribe(GameCanvasController.instance);
    }

    public override void UpdateMe()
    {
        if (_controllerInput != null)
        {
            transform.position += _controllerInput.CheckMovement();

            if (SystemInfo.deviceType == DeviceType.Handheld)
                transform.eulerAngles = _controllerInput.CheckRotation();
            else
                transform.LookAt(new Vector3(_controllerInput.CheckRotation().x,
                                             transform.position.y,
                                             _controllerInput.CheckRotation().z));
        }
    }

    public override void Die()
    {
    }

    public void Subscribe(IObserver observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        if (!_observers.Contains(observer))
            _observers.Remove(observer);
    }
}
