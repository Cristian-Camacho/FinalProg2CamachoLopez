using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasController : MonoBehaviour , IObserver
{

    public static GameCanvasController instance;
    public GameObject mobileCanvasConteiner;
    public Stick moveStick;
    public Stick aimStick;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    
    void Start ()
    {
       /* if (SystemInfo.deviceType == DeviceType.Handheld)
            mobileCanvasConteiner.SetActive(true);
        else
        {
 //           adButton.enabled = false;
            mobileCanvasConteiner.SetActive(false);
 //           caseNoAds.SetActive(true);
        }*/

    }
	
	void Update () {
		
	}

    public void Notify(string action)
    {
        throw new System.NotImplementedException();
    }
}
