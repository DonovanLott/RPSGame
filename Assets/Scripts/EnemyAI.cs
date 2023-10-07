using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{

    /*This script is a ROUGH and basic AI that just spawns enemies randomly outside
    bounds and has them move in a random direction through the play area

    Made this as a good place to start
    */

    public float lowestSpeed, highestSpeed;

    public GameObject gameArea;
    public GameObject enemy;

    public int enemyLimit;
    public float spawnDelay;

    public float spawnCircleRadius = 50f;
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
            GameObject spawnedGuy = Instantiate(enemy);
            enemy.transform.position = GetRandomPosition();

            i++;
        }
    }
    
    

    

    

    

    
}
