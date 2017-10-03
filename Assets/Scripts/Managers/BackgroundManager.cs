using ColourRun.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	private IBackgroundPattern _pattern = null;
	
	void FixedUpdate () {
		if (_pattern != null) {
			_pattern.Update();
		}
	}

	public void SetPattern(IBackgroundPattern pattern) {
		if (_pattern != null) {
			_pattern.Stop();
		}

		_pattern = pattern;
		_pattern.Init();
	}
}
