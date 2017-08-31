using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	private static float totalSeconds = 0;

	[SerializeField]
	private GameObject _timeText;

	private bool _timerStarted = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(_timerStarted) {
			UpdateTime();
		}
	}

	public void StartTimer() {
		_timerStarted = true;
		_timeText.SetActive(true);
	}

	void UpdateTime() {
		ScoreManager.totalSeconds += Time.deltaTime;

		_timeText.GetComponent<UnityEngine.UI.Text>().text = Math.Round(ScoreManager.totalSeconds, 1).ToString();
 	}

	 public static float totalTime() {
		 return ScoreManager.totalSeconds;
	 } 

}
