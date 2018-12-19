using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IDamaged
{
    public float life;
    public Vector3 rotation;
    private bool rotated;

    public void RotateWall()
    {
        print("Rotate");
        if(!rotated)transform.eulerAngles = rotation;
        else transform.eulerAngles = Vector3.zero;

        rotated = !rotated;
    }

    public void Feedback(GameObject visualFeed, Vector3 position, Quaternion rotation, int soundClip)
    {

    }

    public void TakeHit(float amount)
    {
        life -= amount;
        if (life < 0)
            Die();
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
