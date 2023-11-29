using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scisssorMove1 : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    public bool canMove = true;
    public bool isMoving = false;
    public float timeBetweenMoving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(isMoving);

        rb = GetComponent<Rigidbody2D>();

        if (Input.GetMouseButtonUp(0)) {
           
            canMove = false;
            isMoving = true;

            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

            
            
            
        }

        if(isMoving) {
            StartCoroutine(Wait());
            if (force > 3f) {
                force = force - .025f; 
            } else if (force <= 3f) {
                force = 3.0f;
                rb.velocity = new Vector2(0, 0);
                isMoving = false;
                canMove = true;
            }

            

        }


        

        if(Input.GetMouseButton(0) && canMove ) {



            if (force <= 30f) {
                force = force + .25f; 
            } else if (force >= 25f) {
                force = 25f;
            }


            

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        
       
        //playerSprite.transform.localPosition = Vector3.MoveTowards(playerSprite.transform.localPosition, myPos, 100f * Time.deltaTime);

        
    }
}
