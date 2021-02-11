using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour
{
    public static event Action startGame = delegate { };
    public static event Action stopGame = delegate { };
   
    PlayerController playerController;

    public Text partyInfo;

    InGameMenuManager inGameMenuManager;

    enum GameState
    {
        stop,
        playing
    }

    GameState gameState;

    public GameFlowManager gameFlowManager;

    void Start()
    {

        if (gameFlowManager == null)
        {
            gameFlowManager = this;
        }

        playerController = FindObjectOfType<PlayerController>();
        gameState = GameState.stop;

        // Controlamos si el FoodGenerator registra que ya se han conseguido todos los puntos
        FoodGenerator.allFoodCollected += Win;

        partyInfo.GetComponent<Text>();

        inGameMenuManager = FindObjectOfType<InGameMenuManager>();
    }

    void Update()
    {
        if(playerController.currentLifes == 0)
        {
            Lose();            
        }

        // Pulsar R para resetear los player prefs con objeto de testeo
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("reset player prefs"); 
            PlayerPrefs.SetInt("totalPoints", 0);
            PlayerPrefs.SetInt("showed_StartMenu", 0);
        }
    }

    public void StartGame()
    {
        gameState = GameState.playing;
        playerController.StartGame();
        startGame();
        Debug.Log("==START GAME==");
    }

    public void StopGame()
    {
        gameState = GameState.stop;
        playerController.StopGame();
        inGameMenuManager.ShowGameOverMenu();
        stopGame();

        FoodGenerator.allFoodCollected -= Win;
    }

    void Win()
    {
        StopGame();
        partyInfo.text = "YOU WIN";
        Debug.Log("==END GAME== (win)");
    }

    void Lose()
    {
        StopGame();
        partyInfo.text = "YOU LOSE";
        Debug.Log("==END GAME== (lose)");
    }
}
