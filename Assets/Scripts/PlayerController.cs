using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 25f;
    private Vector3 target;

    private Vector3 lastCheckedPos;
    private bool isMoving;
    bool up;

    public GameObject playerSprite;
    public GameObject shadowSprite;
    public Collider2D innerC;
    public Collider2D outerC;

    public bool down;

    public List<GameObject> colliderList = new List<GameObject>();

    void Start()
    {
        target = transform.position;
    }


    void Update()
    {
        if (lastCheckedPos != transform.position)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
        }

        if (target != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (!up)
            {
                var myPos = playerSprite.transform.localPosition;
                myPos.y += 1.25f;
                playerSprite.transform.localPosition = myPos;
                //playerSprite.transform.localPosition = Vector3.MoveTowards(playerSprite.transform.localPosition, myPos, 100f * Time.deltaTime);

                var myScale = shadowSprite.transform.localScale;
                myScale.x += 0.1f; 
                myScale.y += 0.1f;
                shadowSprite.transform.localScale = myScale;

                Debug.Log("up");
                up = true;
            }

        }

        if (target == transform.position && up == true)
        {
            //wait a split second
            //groundpound

            StartCoroutine(Wait());
            up = false;
            


        }

        lastCheckedPos = transform.position;

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.25f);
        var myPos = playerSprite.transform.localPosition;
        myPos.y -= 1.25f;
        playerSprite.transform.localPosition = myPos;
        //playerSprite.transform.localPosition = Vector3.MoveTowards(playerSprite.transform.localPosition, myPos, 100f * Time.deltaTime);

        var myScale = shadowSprite.transform.localScale;
        myScale.x -= 0.1f;
        myScale.y -= 0.1f;
        shadowSprite.transform.localScale = myScale;

        //damage
        foreach (var enemy in colliderList)
        {
            enemy.GetComponent<EnemyHealth>().ChangeHealth(-10f);
        }

        Debug.Log("Down");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        /*Debug.Log("triggered: " + collider);
        if(collider.name == "Enemy" && down == true)
        {
            collider.GetComponent<Enemy>().ChangeHealth(-5f);
            Debug.Log("5 damage to enemy");
        }*/

        if (collider.name == "Enemy")
        {
            if (!colliderList.Contains(collider.gameObject))
            {
                colliderList.Add(collider.gameObject);
                Debug.Log("Added " + gameObject.name);
                Debug.Log("GameObjects in list: " + colliderList.Count);
            }
        }
        


    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "Enemy")
        {
            if (colliderList.Contains(collider.gameObject))
            {
                colliderList.Remove(collider.gameObject);
                Debug.Log("Removed " + gameObject.name);
                Debug.Log("GameObjects in list: " + colliderList.Count);
            }
        }
        
    }

    //click on a spot
    //ron hovers in the air
    //ron moves over that spot
    //ron stays in the air for a split second
    //ron slams on the ground
    //not exaclty controlling ron. controlling hi sshadow

    //if holding down mouse button on a spot
    //pari builds acceleration towards that spot
    //letting go causes pari to still move but she will deaccelerate
    //clicking and holding somewhere else will cause pari to start accelerating in that direction

}
