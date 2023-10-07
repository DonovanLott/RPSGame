using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Collider2D areaColl;
    public AIDestinationSetter dest;
    public EnemyAI enemySpawner;

    private void Awake()
    {
        areaColl = GameObject.Find("Bound").GetComponent<BoxCollider2D>();
        enemySpawner = GameObject.Find("Enemy Spawner").GetComponent<EnemyAI>();
    }

    private void Start()
    {
        SetDestination();
    }

    private void SetDestination()
    {
        Vector3 Destination()
        {
            Vector3 randomPos = Random.insideUnitCircle;

            randomPos *= enemySpawner.spawnCircleRadius;
            randomPos += areaColl.transform.position;

            Debug.Log(randomPos);

            return randomPos;
        }

        Transform childObject = gameObject.transform.GetChild(0);

        childObject.position = Destination();

        dest.target = childObject;
    }


}
