using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour , IObservable
{
    public static GameController instance;
    public List<IUpdateable> updateables;
    public AudioSource mainAudio;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}


    public void Subscribe(IObserver observer)
    {
        throw new System.NotImplementedException();
    }

    public void Unsubscribe(IObserver observer)
    {
        throw new System.NotImplementedException();
    }

}
