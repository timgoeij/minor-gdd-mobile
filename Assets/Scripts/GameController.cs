using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    private GameObject mainMenu;

    private float endScore = 0;

    public void startGame()
    {
        LevelGenerator generator = FindObjectOfType<LevelGenerator>();
        generator.startGame();

        ScoreManager score = FindObjectOfType<ScoreManager>();
        score.StartTimer();
        mainMenu.SetActive(false);

        Camera.main.GetComponent<CameraScript>().SetTarget("Player");
    }

    public void EndGamme(float score)
    {
        endScore = score;
    }
}
