using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Collider2D areaColl;
    public AIDestinationSetter dest;
    public EnemyAI enemySpawner;

    public float minWanderTime, maxWanderTime;
    public float enemyLifespan;
    public string mode;

    private void Awake()
    {
        areaColl = GameObject.Find("Bound").GetComponent<BoxCollider2D>();
        enemySpawner = GameObject.Find("Enemy Spawner").GetComponent<EnemyAI>();
    }

    private void Start()
    {
        StartCoroutine(EnemyBehaviour());
    }

    IEnumerator EnemyBehaviour()
    {
        mode = "Wander";
        WanderMode();

        yield return new WaitForSeconds(enemyLifespan);

        mode = "Exit";
        ExitMap();
    }

    private void WanderMode()
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


}
