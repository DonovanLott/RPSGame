using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public GameObject playerz;

    

    // Update is called once per frame
    void Update()
    {
        transform.position = playerz.transform.position;
    }
}
