using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PariMovement : MonoBehaviour
{
    public int health = 1;
    public float speed = 0f;

    public Transform target;

    Vector2 clickPos;
    Vector2 lastClick;

    bool move;

    int decell = 0;

    int Dir = 0;
    int Dir2 = 0;

    bool isLeft = true;

    float rotz;
    public float RotationSpeed;

    int way;
    int flipz = 0;
    bool spinz = false;

    public Vector2 minVal, maxVal;

    bool atWall = false;

    float cPos;
    public int currHealth;
   
    Vector2 movementM = new Vector2(-.25f, 0);
    Vector2 movementM2 = new Vector2(.25f, 0);
    Vector2 movementM3 = new Vector2(0, -.25f);
    
    void Start()
    {
        currHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
      

        if(currHealth <= 0)
        {
            Die();
        }    
      
        
        if (Input.GetMouseButtonUp(0)) {
            
            decell = 2;
            spinz = false;

        }

        

        if (Input.GetMouseButton(0)) {
                lastClick = clickPos;
                clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                
                
                move = true;
               

                
                decell = 1;

                spinz = true;

                

        } 

        if (transform.position.x <= minVal.x && atWall == false ) {

            atWall = true;
         
            if (clickPos.x > minVal.x) {

                transform.transform.Translate(movementM); 
                atWall = false;

            }
           

        } else if (transform.position.y <= minVal.y && atWall == false) {

            atWall = true;
          
            if (clickPos.y > minVal.y) {
                
               
                atWall = false;

            }
            

        } else if (transform.position.x >= maxVal.x && atWall == false) {

            atWall = true;
        
            if (clickPos.x <= maxVal.x) {

                transform.transform.Translate(movementM2); 
                atWall = false;

            }
           

        } else if (transform.position.y >= maxVal.y && atWall == false) {

            atWall = true;
    
            if (clickPos.y <= maxVal.y) {

                transform.transform.Translate(movementM3); 
                atWall = false;

            }
            

        } else {

            atWall = false;
        }

        

        if (move && (Vector2)transform.position != clickPos && speed < 15f && decell == 1 && atWall == false) {
            
            
            float far = speed * Time.deltaTime;
            
            transform.position = Vector2.MoveTowards(transform.position, clickPos, far);

            if (transform.position.x > lastClick.x && !isLeft) {
                flip();
          

            } 

            if (transform.position.x < lastClick.x && isLeft) {
                flip();
                

            } 

           

            Dir2 = checkDirections();
            
            if (Dir == Dir2) {
                speed = speed + .05f;
                 
            } else {
                if (speed > 6f) {
                    speed = speed * .99f;
                } else {
                    speed = 6f;
                }
                Dir = Dir2;
                
            }
           
            
            


        } else if (move && (Vector2)transform.position != clickPos && speed >= 15f && decell == 1 && atWall == false) {
            
            float far = speed * Time.deltaTime; 
            transform.position = Vector2.MoveTowards(transform.position, clickPos, far);

            if (transform.position.x > lastClick.x && !isLeft) {
                flip();
          
            } 

            if (transform.position.x < lastClick.x && isLeft) {
                flip();
             
            } 

            

            Dir2 = checkDirections();
            if (Dir == Dir2) {
                speed = 6f;
                
            } else {
                
                if (speed > 6f) {
                    speed = speed * .99f;
                
                } else {
                    speed = 6f;
                }
                
                Dir = Dir2;
                
            }

            


            
          
        
        } else if (move && (Vector2)transform.position != clickPos && decell == 2 && atWall == false) {
            
            float far = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, clickPos, far);

            

            if (transform.position.x > lastClick.x && !isLeft) {
                flip();
               

            } 

            if (transform.position.x < lastClick.x && isLeft) {
                flip();
             

            } 

            if (speed > 0) {
                speed = speed - .01f;
                
            } else {
                speed = 0f;
                
            }
           
        
        } else {

            move = false;
    

            
            speed = 0f;
            decell = 0;
            

        }

        

        if (spinz == true && atWall == false) {

            rotz += Time.deltaTime * RotationSpeed;

        }  else {
            rotz = 0;
        }
       
        target.rotation = Quaternion.Euler(0, flipz, rotz);
    
        
        
        
   
        
    }

    public int checkDirections() {

        if (clickPos.x > lastClick.x && clickPos.y > lastClick.y ) {

            return 1;

        } else if (clickPos.x > lastClick.x && clickPos.y < lastClick.y ) {

            return -1;

        } else if (clickPos.x < lastClick.x && clickPos.y > lastClick.y ) {
            return 2;

        } else if (clickPos.x < lastClick.x && clickPos.y < lastClick.y ) {

            return -2;

        } else if (clickPos.y > lastClick.y && clickPos.x > lastClick.x ) {
 
            return 3;

        } else if (clickPos.y > lastClick.y && clickPos.x < lastClick.x ) {

            return -3;

        } else if (clickPos.y < lastClick.y && clickPos.x > lastClick.x ) {
   
            return 4;

        } else if (clickPos.y < lastClick.y && clickPos.x < lastClick.x ) {
            return -4;

        } else {

            return 0;

        } 


    }

    void flip() {

        isLeft = !isLeft;
        if (flipz == 180) {
            flipz = 0;
        } else {
            flipz = 180;
        }
        
        

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if(collision.collider.gameObject.tag == "Ron") {

            collision.gameObject.GetComponent<Enemy2>().ChangeHealth(1);

        } else if(collision.collider.gameObject.tag == "Scotty") {

            ChangeHealth(1);

        }


    }

    public void ChangeHealth(int amount)
    {
        currHealth = currHealth -= amount;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }


    

}
