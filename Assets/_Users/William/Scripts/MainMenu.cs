using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject settingsUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit() {
        Application.Quit();
        Debug.Log("Appliaction Quit");
    }

    public void ShowSPMissions() {
        //To Do
    }

    public void GoToMPLobby() {
        //To Do
    }
    public void ShowCredits() {
        //To Do
    }
    public void ShowSettings() {
        mainMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }
    public void ShowMainMenu() {
        mainMenuUI.SetActive(true);
        settingsUI.SetActive(false);
    }
}
