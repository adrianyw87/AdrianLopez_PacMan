using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Text totalPointsText;

    public string MainScene;
   
    void Awake()
    {
        totalPointsText = totalPointsText.GetComponent<Text>();
        totalPointsText.text = PlayerPrefs.GetInt("totalPoints").ToString();

        PlayerPrefs.SetInt("showed_StartMenu", 0);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(MainScene);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
