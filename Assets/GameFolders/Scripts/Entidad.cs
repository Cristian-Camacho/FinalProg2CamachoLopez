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
        GameController.instance.AddUpdateble(this);
	}

    public virtual void TakeHit(float amount)
    {
        currentLife -= amount;
        if (currentLife < 1f)
            Die();
    }

    public abstract void Die();
    public virtual void UpdateMe()
    { }

    protected virtual void Feedback(GameObject feed, Vector3 pos, Quaternion qua)
    {
        var feedbac = Instantiate(feed, pos, qua);
        Destroy(feedbac, 1.5f);
    }

    public virtual void RemoveMe()
    {
        GameController.instance.RemoveUpdateable(this);
    }

}
