using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entidad : MonoBehaviour , IUpdateable , IDamaged
{
    public float maxLife;
    protected float currentLife;
    public float speed;

    public virtual void Start ()
    {
        currentLife = maxLife;
	}
	
    public virtual void UpdateMe()
    {
    }

    private void Update()
    {
        UpdateMe();
    }

    public virtual void TakeHit(float amount)
    {
        currentLife -= amount;
        if (currentLife < 1f)
            Die();
    }

    public abstract void Die();

    public virtual void RemoveMe()
    {
//        GameController.instance.RemoveUpdateable(this);
    }

}
