using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasController : MonoBehaviour , IObserver
{
    public static GameCanvasController instance;
    private Dictionary<string, Action> _actions;
    private bool hasConstruction;
    public Image coreLifeBar;
    public Text resources;
    public GameObject pauseMenu;
    public GameObject defeat;
    public GameObject construcctionOptions;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    
    void Start ()
    {
        defeat.SetActive(false);
        GameController.instance.Subscribe(this);
        GameController.instance.GetGameCore().Subscribe(this);
        CreateDictionary();
    }
	
	void Update () {
		
	}

    private void CreateDictionary()
    {
        _actions = new Dictionary<string, Action>();

        _actions.Add("pause", PauseGame);
        _actions.Add("resume", ResumeGame);
        _actions.Add("resources", UpdateResources);
        _actions.Add("lifeCore", UpdateLifeCore);
        _actions.Add("construcct", Construcction);
        _actions.Add("game", GameHUD);

    }

    public void GameHUD()
    {
        construcctionOptions.SetActive(false);
    }

    public void Construcction()
    {
        construcctionOptions.SetActive(true);

    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        SoundManager.instance.PlaySound(SoundsIDs.ID_PAUSE_ON);
    }

    public void Defeat()
    {
        defeat.SetActive(true);
    }

    public void ResumeGame()
    {
        SoundManager.instance.PlaySound(SoundsIDs.ID_PAUSE_OFF);
        pauseMenu.SetActive(false);
    }

    void UpdateResources()
    {
        resources.text = GameController.instance.resources.ToString();
    }

    void UpdateLifeCore()
    {
        var core = GameController.instance.GetGameCore();
        coreLifeBar.fillAmount = core.currentLife / core.maxLife;
    }

    public void Notify(string action)
    {
        if (_actions.ContainsKey(action))
            _actions[action]();
    }

    public void CreateWall()
    {
        if (!hasConstruction && GameController.instance.resources >= 100f)
        {
            ConstrucctionManager.instance.Wall();
            GameController.instance.CostResources(100f);
            hasConstruction = true;
        }
    }
    public void CreateFrostTurret()
    {
        if (ConstrucctionManager.instance.nodesTurrets.Count < 0)
            return;


        if (!hasConstruction && GameController.instance.resources >= 300f)
        {
            ConstrucctionManager.instance.Frost();

            GameController.instance.CostResources(300f);
            hasConstruction = true;
        }
    }

    public void CreateMinigun()
    {
        if (ConstrucctionManager.instance.nodesTurrets.Count < 0)
            return;

        if (!hasConstruction && GameController.instance.resources >= 150f)
        {
            ConstrucctionManager.instance.Minigun();
            GameController.instance.CostResources(150f);
            hasConstruction = true;
        }
    }

    public void CreateHeavyCannon()
    {
        if (ConstrucctionManager.instance.nodesTurrets.Count < 0)
            return;

        if (!hasConstruction && GameController.instance.resources >= 300f)
        {
            GameController.instance.CostResources(300f);
            ConstrucctionManager.instance.Heavy();
            hasConstruction = true;
        }
    }

    public void ReleaseConstrucction()
    {

        hasConstruction = false;
    }

}
