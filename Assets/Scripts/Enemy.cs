using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/*
 * Brain of enemy object
 * Only for use with enemy prefab
 */
public class Enemy : MonoBehaviour
{
    //BG object
    private Transform areaColl;

    //AStar destination script
    private AIDestinationSetter dest;

    //enemy spawner script
    private EnemyAI enemySpawner;

    //enemy aggro (sister script)
    private EnemyAggro aggroScript;

    //window of random lengths enemy wanders in one direction before switching to another
    public float minWanderTime, maxWanderTime;

    //amount of time before enemy decides to wander off the map
    public float enemyLifespan;

    //public showcase of current enemy behaviour mode
    public string mode;

    private void Awake()
    {
        areaColl = GameObject.FindWithTag("Background").transform;
        enemySpawner = GameObject.FindWithTag("Spawner").GetComponent<EnemyAI>();
        aggroScript = transform.GetChild(1).gameObject.GetComponent<EnemyAggro>();
        dest = GetComponent<AIDestinationSetter>();
    }

    private void Start()
    {
        StartCoroutine(EnemyBehaviour());
    }

    IEnumerator EnemyBehaviour()
    {
        //Set to wander if not in a chase
        if(!aggroScript.chasing)
        {
            mode = "Wander";
            WanderMode();
        }

        //Wait for lifespan
        yield return new WaitForSeconds(enemyLifespan);

        //If enemy is not currently chasing player, exit behaviour functions as normal.
        if(!aggroScript.chasing)
        {
            mode = "Exit";
            ExitMap();
        } // else if the enemy is still chasing the player, enemyLifeSpan = 10;
        else
        {
            enemyLifespan = 10;

            StartCoroutine(EnemyBehaviour());
        }
        
    }

    public void WanderMode()
    {
        Vector3 Destination()
        {
            Vector3 randomPos = Random.insideUnitCircle;

            randomPos *= enemySpawner.spawnCircleRadius;
            randomPos += areaColl.transform.position;

            return randomPos;
        }

        Transform childObject = gameObject.transform.GetChild(0);

        IEnumerator PeriodicDestinationChange()
        {
            while(mode == "Wander")
            {
                childObject.position = Destination();

                yield return new WaitForSeconds(Random.Range(minWanderTime, maxWanderTime));
            }
        }

        dest.target = childObject;

        StartCoroutine(PeriodicDestinationChange());

    }

    private void ExitMap()
    {
        Vector3 FinalDestination()
        {
            Vector3 randomPos = Random.insideUnitCircle.normalized * ( .1f * Random.Range(enemySpawner.spawnCircleRadius, 1f));

            randomPos *= enemySpawner.spawnCircleRadius;
            randomPos += areaColl.transform.position;

            return randomPos;
        }

        Transform childObject = gameObject.transform.GetChild(0);

        childObject.position = FinalDestination();

        dest.target = childObject;



    }

    public void ChaseMode()
    {
        dest.target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Debug.Log("Chase!");
    }


}
