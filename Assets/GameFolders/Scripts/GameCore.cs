using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : DefaultTrackableEventHandler
{
    public float timerLostIt;
    private bool lostIt;
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        lostIt = false;
       // GameController.instance.CoreVisible(true);
        StopCoroutine("WaitForIt");
        //Agregar un timer para q pasado cierto tiempo recien haga el tracking lost
    }

    protected override void OnTrackingLost()
    {
        StartCoroutine("WaitForIt");
        if (lostIt)
        {
            base.OnTrackingLost();
           // GameController.instance.CoreVisible(false);
        }
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(timerLostIt);
        lostIt = true;
    }
}
