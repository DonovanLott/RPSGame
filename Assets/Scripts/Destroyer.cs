using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  This class is for objects with a 2d collider attached.
 *  
 *  Destroys any object that enters collider with the "enemy" tag
 */
public class Destroyer : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("hey");
            Destroy(other.gameObject);
        }
    }
}
