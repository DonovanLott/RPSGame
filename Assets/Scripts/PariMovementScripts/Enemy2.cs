using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{

    public int health = 1;
    public int currHealth;

    void Start()
    {
        currHealth = health;
    }

    void Update()
    {
        if(currHealth <= 0)
        {
            Die();
        }
    }

    public void ChangeHealth(int amount)
    {
        currHealth = currHealth -= amount;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
