using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour {

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject gameOver;

    [SerializeField]
    private bool _gameStarted = false;
    
    [SerializeField]
    public bool gameStarted {
        get {
            return _gameStarted;
        }

        private set {
            _gameStarted = value;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        mainMenu = GameObject.Find("MainMenu");
        gameOver = GameObject.Find("GameOver");
        gameOver.SetActive(false);

        if (CheckReloadedGame()) {
            startGame();
        }
    }

    private Boolean CheckReloadedGame()
    {
        GameController[] controllers = FindObjectsOfType<GameController>();

        if(controllers.Length > 1)
        {
            for(int i = 0; i < controllers.Length; i++)
            {
                if (controllers[i].CompareTag("OldGameController")) {
                    Destroy(controllers[i].gameObject);
                } else {
                    return true;
                }
            }
        }
        return false;
    }

    public void startGame()
    {
        gameStarted = true;
        mainMenu.SetActive(false);
        FindObjectOfType<ScoreManager>().StartTimer();
        Camera.main.GetComponent<CameraScript>().SetTarget("Player");
        FindObjectOfType<PlayerScript>().StartRunning();
    }

    public void EndGame()
    {
        gameStarted = false;
        gameOver.SetActive(true);
        gameOver.transform.Find("ScoreText").GetComponent<Text>().text = ScoreManager.GetRoundedScoreAsString();
        transform.tag = "OldGameController";
    } 

    public void ReloadGame()
    {
        ColorManager.resetColors();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
