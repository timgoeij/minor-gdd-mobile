﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	[SerializeField]

	private GameObject _target;
	// Use this for initialization
	void Start () {
		
	}

	public void SetTarget(string targetTag) {
		_target = GameObject.FindGameObjectWithTag(targetTag);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(
			transform.position, 
			new Vector3(
				_target.transform.position.x + CameraScreen.width / GetDivider(), 
				_target.transform.position.y + CameraScreen.height / (GetDivider() * 3) , 
				transform.position.z
			), 
			1f);
	}

	private int GetDivider() {
		if (Camera.main.aspect >= 1.7)
		{
				return 3;
		}
		else if (Camera.main.aspect >= 1.5)
		{
				return 3;
		}
		else
		{
				return 4;
		}
	} 
}
