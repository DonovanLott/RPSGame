using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Gives enemy aggro functionality
 * 
 * Will chase player in certain radius, will lose aggression if player leaves larger radius
 * 
 * 
 */
public class EnemyAggro : MonoBehaviour
{
    
    public Enemy thisEnemy;
    private Collider2D thisCollider;
    private Collider2D outerCollider;

    private GameObject player;

    public float cooldownLength;
    public bool chasing;

    private void Awake()
    {
        thisCollider = GetComponent<CircleCollider2D>();
        outerCollider = transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

            chasing = true;
            thisEnemy.mode = "Chase";
            thisEnemy.ChaseMode();

            StartCoroutine(CooldownAggro(cooldownLength)); // Start cooldown of inner aggro range
        }
    }

    private void Update()
    {

        if(chasing && !outerCollider.bounds.Contains(player.transform.position))
        {

            thisEnemy.mode = "Wander";
            thisEnemy.WanderMode();
            chasing = false;

        }


    }

    IEnumerator CooldownAggro(float length)
    {
        thisCollider.enabled = false;

        yield return new WaitForSeconds(length);

        thisCollider.enabled = true;
    }


}
