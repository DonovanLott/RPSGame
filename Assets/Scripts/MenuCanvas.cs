using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuCanvas : MonoBehaviour
{
    public GameObject currentCanvas;

    GameObject mainMenuCanvas;
    GameObject levelSelectCanvas;
    GameObject settingsCanvas;
    GameObject creditsCanvas;

    void Start()
    {
        mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        mainMenuCanvas.SetActive(true);
        levelSelectCanvas = GameObject.Find("ModeSelectCanvas");
        levelSelectCanvas.SetActive(false);
        settingsCanvas = GameObject.Find("SettingsCanvas");
        settingsCanvas.SetActive(false);
        creditsCanvas = GameObject.Find("CreditsCanvas");
        creditsCanvas.SetActive(false);
    }

    public void BackToMenu()
    {
        currentCanvas = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        currentCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void LoadLevelSelectCanvas()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);
        currentCanvas = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        currentCanvas.SetActive(false);
        levelSelectCanvas.SetActive(true);
    }

    public void LoadSettingsCanvas()
    {
        currentCanvas = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        currentCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void LoadCreditsCanvas()
    {
        currentCanvas = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        currentCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

}
