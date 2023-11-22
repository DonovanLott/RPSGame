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

            selectedCharacter++;
            if (selectedCharacter == pl.Length) {

                selectedCharacter = 0;

            }

        }
        VCam.m_Follow = pl[selectedCharacter].transform;
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }
}
