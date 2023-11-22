using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSelect : MonoBehaviour
{
    public GameObject[] playerz;

    public int selectedCharacter;

   



    private void Awake () {

        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        

        foreach(GameObject player in playerz) {
            player.SetActive(false);
        }

        playerz[selectedCharacter].SetActive(true);

    }

    void Update()
    {   
        
        
        if (((Input.GetKeyDown("space")) )   )
        {
           
            if (selectedCharacter == 1) {

                if ((playerz[selectedCharacter].GetComponent<RPSGame.RonMovement>()._moving == false)) {

                    playerz[selectedCharacter].SetActive(false);
                    
     
                    selectedCharacter++;
                    if (selectedCharacter == playerz.Length) {

                        selectedCharacter = 0;

                    }
            

                    playerz[selectedCharacter].SetActive(true);

                    if (selectedCharacter != 0) {
                        playerz[selectedCharacter].transform.position = playerz[selectedCharacter- 1].transform.position;
                        playerz[selectedCharacter].transform.rotation = playerz[selectedCharacter- 1].transform.rotation;
                    } else {

                        playerz[selectedCharacter].transform.position = playerz[playerz.Length - 1].transform.position;
                        playerz[selectedCharacter].transform.rotation = playerz[playerz.Length - 1].transform.rotation;

                    }
                            

                    PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);

                } 

            } else {

                playerz[selectedCharacter].SetActive(false);
                    
     
                selectedCharacter++;
                if (selectedCharacter == playerz.Length) {

                    selectedCharacter = 0;

                }
            

                playerz[selectedCharacter].SetActive(true);

                if (selectedCharacter != 0) {
                    playerz[selectedCharacter].transform.position = playerz[selectedCharacter- 1].transform.position;
                    playerz[selectedCharacter].transform.rotation = playerz[selectedCharacter- 1].transform.rotation;
                } else {

                    playerz[selectedCharacter].transform.position = playerz[playerz.Length - 1].transform.position;
                    playerz[selectedCharacter].transform.rotation = playerz[playerz.Length - 1].transform.rotation;

                }
                            

                PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);

            }
                  
            
        }
             

    }


}
