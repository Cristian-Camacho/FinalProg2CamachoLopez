using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour , IObservable
{
    public static GameController instance;
    private List<IUpdateable> _gameUpdateables;
    private List<IUpdateable> _dontUpdateables;
    private List<IObserver> _observers;
    public AudioSource mainAudio;
    public GameObject conteinerObjectsGame;
    private bool gameActive;
    private bool endGameFase;
    private bool constructionFase;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        _gameUpdateables = new List<IUpdateable>();
        _dontUpdateables = new List<IUpdateable>();
        _observers = new List<IObserver>();

    }


    void Start ()
    {
        NodeConteiner.instance.gameObject.SetActive(false);
        conteinerObjectsGame.SetActive(true);


    }
	
	void Update ()
    {
		if (gameActive && !constructionFase && _gameUpdateables.Count > 0)
        {
            foreach (var upts in _gameUpdateables)
            {
                if (_dontUpdateables.Contains(upts))
                    continue;

                if(endGameFase)
                {
                    upts.UpdateMe();
                    upts.UpdateMe();
                }else
                    upts.UpdateMe();
            }
        }
    }

    public void CoreVisible(bool state)
    {
        if(state != gameActive)
            gameActive = state;
    }

    public void AddUpdateble(IUpdateable me)
    {
        if (!_gameUpdateables.Contains(me))
        _gameUpdateables.Add(me);
    }

    public void RemoveUpdateable(IUpdateable me)
    {
        if (_gameUpdateables.Contains(me))
        _gameUpdateables.Remove(me);
    }

    public void DontUpdateMe(IUpdateable me)
    {
        if (!_dontUpdateables.Contains(me))
            _dontUpdateables.Add(me);
    }
    public void RestoreUpdateMe(IUpdateable me)
    {
        if (_dontUpdateables.Contains(me))
            _dontUpdateables.Remove(me);
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
