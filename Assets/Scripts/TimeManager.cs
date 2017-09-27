using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

	public float _timeSinceSlomoStarted = 0f;
	public float _slomoDuration = 0f;
	public bool _inSlomo = false;

	public bool _freezeFrame = false;
	public float _freezeFrameTime = 0;

	// Update is called once per frame
	void Update () {
		HandleSlomo();
	}

	public void FreezeFrame() {
		SetSlomo(0, 0.00001f);
	}

	public void SetSlomo(float timeScale, float duration) {
		Time.timeScale = timeScale;
		_inSlomo = true;
		_slomoDuration = duration;
		_timeSinceSlomoStarted = 0;
	}

	private void HandleSlomo() {
		if ( ! _inSlomo) {
			return;
		}

		if (_timeSinceSlomoStarted >= _slomoDuration) {
			Time.timeScale = 1;
			_slomoDuration = 0;
			_inSlomo = false;
			_timeSinceSlomoStarted = 0;

			return;
		} 
		
		_timeSinceSlomoStarted += Time.unscaledDeltaTime;
	}

	public void StartSlomo(float timescale) {
		Time.timeScale = timescale;
	}

	public void StopSlomo() {
		Time.timeScale = 1;
	}
}
