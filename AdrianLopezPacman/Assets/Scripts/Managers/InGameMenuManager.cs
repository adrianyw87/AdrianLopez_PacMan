using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour
{
    public GameObject StartGamePanel;

    public GameObject GameOverPanel;    

    public string MainmenuScene;

    public InGameMenuManager inGameMenuManager;

    bool showedMainMenu;

    GameFlowManager gameFlowManager;
    
    void Start()
    {
        gameFlowManager = FindObjectOfType<GameFlowManager>();

        if(inGameMenuManager == null)
        {
            inGameMenuManager = this;
        }

        GameOverPanel.SetActive(false);

        // Sólo encender StartGamePanel la primera vez que se entra en escena
        StartGamePanel.SetActive(false);
        int showedMenu = PlayerPrefs.GetInt("showed_StartMenu");
        if(showedMenu == 0)
        {            
            PlayerPrefs.SetInt("showed_StartMenu", 1);
            StartGamePanel.SetActive(true);            
        }
        else
        {
            gameFlowManager.StartGame();
        }
    }

    public void ShowGameOverMenu()
    {
        GameOverPanel.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu ()
    {
        SceneManager.LoadScene(MainmenuScene);
    }
}
