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

    public float previousScore = 0;

    private void Start()
    {

        mainMenu = GameObject.Find("MainMenu");
        gameOver = GameObject.Find("GameOver");
        gameOver.SetActive(false);
    }

    public void startGame()
    {
        FindObjectOfType<LevelGenerator>().startGame();
        FindObjectOfType<ScoreManager>().StartTimer();
        mainMenu.SetActive(false);
        Camera.main.GetComponent<CameraScript>().SetTarget("Player");
    }

    public void EndGame()
    {
        gameOver.SetActive(true);
        gameOver.transform.Find("ScoreText").GetComponent<Text>().text = ScoreManager.GetRoundedScoreAsString();
    } 

    public void ReloadGame()
    {
        //transform.name = "OldController";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
