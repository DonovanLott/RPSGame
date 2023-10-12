using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    private float health;

    void Start()
    {
        health = 10f;
    }

    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    public void ChangeHealth(float amount)
    {
        health = health + amount;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
