using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour , IObservable
{
    public static GameController instance;
    public List<IUpdateable> _gameUpdateables;
    public List<IUpdateable> _DontUpdateables;
    private List<IObserver> _observers;
    public List<IUpdateable> _construc;
    public GameObject cameraGame;
    public GameObject cameraConstrucction;

    public float resources;
    private bool endGameFase = false;
    private bool constructionFase = false;
    private bool pauseGame = false;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Core _core;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        _DontUpdateables = new List<IUpdateable>();
        _gameUpdateables = new List<IUpdateable>();
        _observers = new List<IObserver>();
        _construc = new List<IUpdateable>();
        if (_player == null) _player = (Player)FindObjectOfType(typeof(Player));
        if (_core == null) _core = (Core)FindObjectOfType(typeof(Core));


    }
    private void Start()
    {
        EnterGameStage();
    }

    void Update ()
    {
        if (!pauseGame)
        {
		    if (!constructionFase && _gameUpdateables.Count > 0)
            {
                foreach (var upts in _gameUpdateables)
                {
                    if (_DontUpdateables.Contains(upts))
                        continue;

                    if(endGameFase)
                    {
                        upts.UpdateMe();
                        upts.UpdateMe();
                    }
                    else
                        upts.UpdateMe();
                }
            }
            else if (_construc.Count > 0)
            {
                foreach (var item in _construc)
                {
                    item.UpdateMe();
                }
            }


            foreach (var item in _DontUpdateables)
            {
                if (!_gameUpdateables.Contains(item))
                {
                    RemoveDonUpdateThis(item);
                }

            }
        }
    }

    public void EnterGameStage()
    {
        NodeConteiner.instance.gameObject.SetActive(false);
        cameraConstrucction.SetActive(false);
        cameraGame.SetActive(true);
        GameCanvasController.instance.GameHUD();
        constructionFase = false;
        EnemyController.instance.SpawnEnemy();
    }

    public void EnterConstrucctionStage()
    {
        NodeConteiner.instance.gameObject.SetActive(true);
        GameCanvasController.instance.Construcction();
        constructionFase = true;
        cameraConstrucction.SetActive(true);
        cameraGame.SetActive(false);
    }

    public Player GetGamePlayer()
    {
        return _player;
    }

    public Core GetGameCore()
    {
        return _core;
    }

    public void AddResources(float amount)
    {
        resources += amount;
        foreach (var obs in _observers)
        {
            obs.Notify("resources");
        }

    }

    public void CostResources(float amount)
    {
        resources -= amount;
        if (resources < 0)
            resources = 0;
        foreach (var obs in _observers)
        {
            obs.Notify("resources");
        }
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
    public void AddUpdatebleConstruc(IUpdateable me)
    {
        if (!_construc.Contains(me))
            _construc.Add(me);

    }

    public void RemoveUpdateableConstruc(IUpdateable me)
    {
        if (_construc.Contains(me))
            _construc.Remove(me);
    }

    public void DonUpdateThis(IUpdateable me)
    {
        if (_DontUpdateables.Contains(me))
            _gameUpdateables.Add(me);
    }

    public void RemoveDonUpdateThis(IUpdateable me)
    {
        if (_DontUpdateables.Contains(me))
            _gameUpdateables.Remove(me);
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

    public void PauseAll()
    {
        pauseGame = !pauseGame;

        if (!pauseGame)
        {
            foreach (var obs in _observers)
            {
                obs.Notify("resume");
            }
        }
        else
        {
            foreach (var obs in _observers)
            {
                obs.Notify("pause");
            }
        }
    }
}
