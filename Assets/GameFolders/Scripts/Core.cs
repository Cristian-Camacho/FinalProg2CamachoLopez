using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour, IDamaged, IObservable
{
    public float maxLife;
    public float currentLife;
    public GameObject feed;
    private List<IObserver> _observers;

    // Use this for initialization
    private void Awake()
    {
        _observers = new List<IObserver>();
    }

    void Start ()
    {
        currentLife = maxLife;
	}
	
    public void Die()
    {
        GameController.instance.PauseAll();

    }

    public void TakeHit(float amount)
    {
        currentLife -= amount;
        foreach (var item in _observers)
        {
            item.Notify("lifeCore");
        }
        if (currentLife < 1f)
            Die();
    }
    /*
    public void TakeHit(float amount, Vector3 pointDamage)
    {
        TakeHit(amount);
        Feedback(feed, pointDamage, Quaternion.identity, 0);
    }*/

    public void Feedback(GameObject visualFeed, Vector3 position, Quaternion rotation, int soundID)
    {
        var feedbac = Instantiate(visualFeed, position, rotation);
        SoundManager.instance.PlaySound(soundID);
        Destroy(feedbac, 1.5f);
    }


    public void Subscribe(IObserver observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        if (_observers.Contains(observer))
            _observers.Remove(observer);
    }
}
