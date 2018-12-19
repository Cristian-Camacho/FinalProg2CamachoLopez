using UnityEngine;



public interface IDamaged
{
	void TakeHit(float amount);
    void Feedback(GameObject visualFeed, Vector3 position, Quaternion rotation, int soundClip);
	void Die();
}
