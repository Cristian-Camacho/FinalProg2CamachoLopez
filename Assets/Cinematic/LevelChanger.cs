using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public Animator animator;

    public int levelToLoad;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Reemplazar Input por el deseado en Celular, para skipear cinematica
        if(Input.GetKey(KeyCode.Escape))
        {
            FadeToLevel(levelToLoad);
        }
		
	}

    public void FadeToLevel (int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);
    }
     
}
