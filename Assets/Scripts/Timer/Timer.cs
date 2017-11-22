using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
	public static bool TimerRunning = false;
	public static float TotalTime = 0;
	
	// Update is called once per frame
	void Update () {
		if (Timer.TimerRunning) {
			Timer.TotalTime += Time.deltaTime;
		}
	}

	public static void StartTimer() {
		Timer.TimerRunning = true;
		Timer.TotalTime = 0;
	}

	public static void StopTimer() {
		Timer.TimerRunning = false;
	}
}
