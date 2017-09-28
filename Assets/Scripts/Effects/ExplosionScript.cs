using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {

	private float _timeAlive = 0;
	private float _explosionSize = 2f;
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(7f,7f,0f) * _explosionSize;
		GetComponent<SpriteRenderer>().color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {

		if (_timeAlive > 0.03f) {
			GetComponent<SpriteRenderer>().color = Color.black;
		}

		if (_timeAlive > 0.05f) {
			GetComponent<SpriteRenderer>().color = Color.white;
		}

		if (_timeAlive > 0.08f) {
			GetComponent<SpriteRenderer>().color = Color.black;
		}

		transform.localScale -= new Vector3(1.5f,1.5f,0) * _explosionSize;

		_timeAlive += Time.deltaTime;

		if (transform.localScale.x < 0.5f) {
			Destroy(gameObject);
		}
	}
}
