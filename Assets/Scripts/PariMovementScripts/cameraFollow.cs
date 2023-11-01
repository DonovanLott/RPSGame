using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    public float speed = 2f;
    public Transform target;

    public Vector3 minVal, maxVal;



    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);

        Vector3 boundPos = new Vector3(Mathf.Clamp(newPos.x, minVal.x, maxVal.x), Mathf.Clamp(newPos.y, minVal.y, maxVal.y), Mathf.Clamp(newPos.z, minVal.z, maxVal.z));

        transform.position = Vector3.Slerp(transform.position, boundPos, speed * Time.deltaTime);

      
        


    }

    
}
