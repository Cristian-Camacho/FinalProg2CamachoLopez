using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkAttack : MonoBehaviour, IUpdateable
{
    public float cooldown;
    public float currenTime;
    public bool _canAttack;
    public float damage;

    void Start()
    {
        GameController.instance.AddUpdateble(this);
        this.gameObject.SetActive(false);
        currenTime = cooldown;
    }

    public void Attack()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(TimerAttack());
        _canAttack = false;
        currenTime = cooldown;
    }

    public bool Avaliable()
    {
        if (!this.gameObject.activeSelf)
            return _canAttack;
        else return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entidad>() != null)
        {
            other.GetComponent<Entidad>().TakeHit(damage);
        }

    }

    IEnumerator TimerAttack()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    public void UpdateMe()
    {
        if (!_canAttack)
            currenTime -= Time.deltaTime;

        if (currenTime <= 0f)
            _canAttack = true;
    }

	
}
