using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PariMovement : MonoBehaviour
{

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
   
    

    // Update is called once per frame
    void Update()
    {
        
        
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

        

        

        if (move && (Vector2)transform.position != clickPos && speed < 15f && decell == 1) {
            
            
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
           
            
            


        } else if (move && (Vector2)transform.position != clickPos && speed >= 15f && decell == 1) {
            
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

            


            
          
        
        } else if (move && (Vector2)transform.position != clickPos && decell == 2 ) {
            
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

        

        if (isLeft && spinz == true) {

            rotz += Time.deltaTime * RotationSpeed;

        } else if (!isLeft && spinz == true) {
            rotz += -Time.deltaTime * RotationSpeed;
        } else {
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

}
