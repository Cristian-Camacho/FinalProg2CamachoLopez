using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pilar") && other != this)
        {
            var temp = other.ClosestPoint(Vector3.up * 5);
            ConstrucctionManager.instance.AddPointTurret(temp);
        }
    }


}
