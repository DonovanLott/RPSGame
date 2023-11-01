using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Functionally independent of gameObject this is placed on
 * 
 * Enemy spawning functionality
 */
public class EnemyAI : MonoBehaviour
{
    //center of enemy spawning
    public GameObject gameArea;

    //enemy prefab that gets instantiated
    public GameObject enemy;

    //amount of time between enemy spawns
    public float spawnDelay;

    //radius of circle enemies spawn along
    public float spawnCircleRadius = 50f;

    //max amount of enemy spawns
    public int limit = 40;

    private void Start()
    {
        StartCoroutine(SpawnTest());
    }
    void OnDrawGizmos()
    {
        Vector2 origin = gameArea.transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, spawnCircleRadius);
    }

    Vector3 GetRandomPosition()
    {


        Vector3 position = Random.insideUnitCircle.normalized;

        position *= spawnCircleRadius;
        position += gameArea.transform.position;

        return position;
    }

    IEnumerator SpawnTest()
    {

        int i = 0;
        while (i < limit)
        {

            yield return new WaitForSeconds(spawnDelay);
            GameObject spawnedGuy = Instantiate(enemy, gameObject.transform);
            enemy.transform.position = GetRandomPosition();

            i++;
        }
    }

}
