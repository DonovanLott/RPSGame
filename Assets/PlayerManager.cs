using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;




public class PlayerManager : MonoBehaviour
{
    public GameObject[] pl;
    public CinemachineVirtualCamera VCam;

    public int selectedCharacter;

    private void Awake() {

        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

    }
    
    // Update is called once per frame
    void Update()
    {
        if (((Input.GetKeyDown("space")))) {
            if ((selectedCharacter == 0) ) {
                if ((pl[selectedCharacter].GetComponent<PariMovement>().speed == 0f)) {
                    selectedCharacter++;
                    if (selectedCharacter == pl.Length) {

                        selectedCharacter = 0;

                    }
                }
            } else if ((selectedCharacter == 1) ) {
                if ((pl[selectedCharacter].GetComponent<RPSGame.RonMovement>()._moving == false)) {
                    selectedCharacter++;
                    if (selectedCharacter == pl.Length) {

                        selectedCharacter = 0;

                    }
                }
            } else if ((selectedCharacter == 2) ) {
                if ((pl[selectedCharacter].GetComponent<scisssorMove1>().isMoving == false)) {
                    selectedCharacter++;
                    if (selectedCharacter == pl.Length) {

                        selectedCharacter = 0;

                    }
                }
                
            }

        }
        VCam.m_Follow = pl[selectedCharacter].transform;
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }
}
