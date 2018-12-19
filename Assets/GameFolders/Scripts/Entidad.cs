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
        if (currentLife <= 0f)
            Die();
    }

    public abstract void Die();
    public virtual void UpdateMe()
    { }

    public virtual void RemoveMe()
    {
        GameController.instance.RemoveUpdateable(this);
    }

    public virtual void Feedback(GameObject visualFeed, Vector3 position, Quaternion rotation, int soundID)
    {
        var feedbac = Instantiate(visualFeed, position, rotation);
        SoundManager.instance.PlaySound(soundID);
        Destroy(feedbac, 1.5f);
    }
}
