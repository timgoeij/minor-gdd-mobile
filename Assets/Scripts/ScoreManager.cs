using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public static float totalSeconds = 0;

	[SerializeField]
	private GameObject _timeText;
	private bool _timerStarted = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(_timerStarted) {
			if ( ! _timeText.activeInHierarchy) {
				_timeText.SetActive(true);
			}
			UpdateTime();
		}
	}

	public void StartTimer() {
		_timerStarted = true;
		totalSeconds = 0;
		_timeText.SetActive(true);
	}

	public void StopTimer() {
		_timerStarted = false;	
	}

	public static double GetRoundedScore() {
		return Math.Round(ScoreManager.totalSeconds, 1);
	}

	public static string GetRoundedScoreAsString() {
		return ScoreManager.GetRoundedScore().ToString();
	}

	void UpdateTime() {
		ScoreManager.totalSeconds += Time.deltaTime;

		_timeText.GetComponent<UnityEngine.UI.Text>().text = Math.Round(ScoreManager.totalSeconds, 1).ToString();
 	}

	 public static float TotalTime() {
		 return ScoreManager.totalSeconds;
	 } 
}
