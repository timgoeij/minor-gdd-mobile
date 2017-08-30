using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	private float _totalSeconds = 0;

	[SerializeField]
	private GameObject _timeText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTime();
	}

	void UpdateTime() {
		_totalSeconds += Time.deltaTime;

		_timeText.GetComponent<UnityEngine.UI.Text>().text = Math.Round(_totalSeconds, 1).ToString();
 	}

}
