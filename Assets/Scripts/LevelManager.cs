using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	
	private int? _forcedColor = null;
	private float _forcedColorTime = 0;
	private float _forcedColorTimer = 0;

	public int? forcedColor {
		get {
			return _forcedColor;
		}

		private set {
			if (value <= ColorManager.colors().Count) {
				_forcedColor = value;
			}
		}
	}  
	// Use this for initialization
	
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (_forcedColor != null && _forcedColorTime > 0) {
			UpdateForcedColor();
		}
	}

	private void UpdateForcedColor() {
		
		if (_forcedColorTimer > _forcedColorTime) {
			_forcedColor = null;
			_forcedColorTimer = 0;
			_forcedColorTime = 0;

			return;
		}
		 
		_forcedColorTimer += Time.deltaTime;
	}

	public void SetForcedColor(int colorIndex, int time) {
		forcedColor = colorIndex;
		_forcedColorTimer = 0;
	}
}
