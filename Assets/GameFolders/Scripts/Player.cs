using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entidad, IObservable
{
    public Vector3 startPos;
    public float range;
    private List<IObserver> _observers;
    private ControllerInput _controllerInput;
    public SparkAttack attack;

    public override void Start()
    {
        base.Start();
        _observers = new List<IObserver>();
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
            var temp = transform.position;
            transform.position += _controllerInput.CheckMovement();
            if (Vector3.Distance(GameController.instance.GetGameCore().transform.position, this.transform.position) > range)
                transform.position = temp;

            if (SystemInfo.deviceType == DeviceType.Handheld)
                transform.eulerAngles = _controllerInput.CheckRotation();
            else
                transform.LookAt(new Vector3(_controllerInput.CheckRotation().x,
                                             transform.position.y,
                                             _controllerInput.CheckRotation().z));

            if (_controllerInput.InputShoot() && attack.Avaliable())
                attack.Attack();
        }
    }

    public override void Die()
    {
        if (GameController.instance.resources < 150)
            GameController.instance.RemoveUpdateable(this);
        else
        {
            GameController.instance.CostResources(150);
            Reposition();
            GameController.instance.AddUpdateble(this);

        }
    }

    public void Reposition()
    {
        transform.position = startPos;
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
