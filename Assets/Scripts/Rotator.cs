using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	private float _rotationSpeed;

	// Use this for initialization
	void Start () {
		_rotationSpeed = UnityEngine.Random.Range(0.5f, 1f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(0, 0, 1 * _rotationSpeed);
	}
}
