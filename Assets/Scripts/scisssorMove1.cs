using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scisssorMove1 : MonoBehaviour
{
    public Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    public bool canMove = true;
    public bool isMoving = false;
    public float timeBetweenMoving;

    //bool isLeft = true;

    public GameObject scottySprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //clicking once should move him a distance of 5 units
    //holding and releasing should allow him to move between 5 and 20 units
    //freeze characters rotation while he's launching forward.

    // Update is called once per frame
    void Update()
    {
        
        

        rb = GetComponent<Rigidbody2D>();

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        

        //if (transform.position.x > mousePos.x && !isLeft) //flips sprite when looking in diferent direction
        //{
        //    flip();
        //}

        //if (transform.position.x < mousePos.x && isLeft)
        //{
        //    flip();
        //}


        if (Input.GetMouseButtonUp(0)) {
           
            canMove = false;
            isMoving = true;

            

            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            
            
        }

        if(isMoving) {
            StartCoroutine(Wait());
            if (force > 0f) {
                force = force - .025f; 
            } else if (force <= 0f) {
                force = 0f;
                rb.velocity = new Vector2(0, 0);
                isMoving = false;
                canMove = true;
            }

            

        }


        

        if(Input.GetMouseButton(0) && canMove ) {



            if (force <= 15f) {
                force = force + .05f; 
            } else if (force >= 15f) {
                force = 15f;
            }


            

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        
       
        //playerSprite.transform.localPosition = Vector3.MoveTowards(playerSprite.transform.localPosition, myPos, 100f * Time.deltaTime);

        
    }

    //void flip()
    //{

    //    isLeft = !isLeft;
    //    scottySprite.transform.localScale = new Vector3(scottySprite.transform.localScale.x,scottySprite.transform.localScale.y * -1,scottySprite.transform.localScale.z); //reverses sprite
    //}
}
