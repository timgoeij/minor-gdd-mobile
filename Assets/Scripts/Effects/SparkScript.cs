using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkScript : MonoBehaviour {

	private float _timeToFade;
	private float _timeAlive = 0;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().AddForce( (Random.onUnitSphere * Random.Range(-30f, 30f)), ForceMode2D.Impulse );
		_timeToFade = UnityEngine.Random.Range(0.5f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (_timeAlive >= _timeToFade) {
			Color c = GetComponent<SpriteRenderer>().color;
			c.a -= 0.01f;

			if (c.a <= 0) {
				Destroy(gameObject);
				return;
			}

			GetComponent<SpriteRenderer>().color = c;

			return;
		}

		_timeAlive += Time.deltaTime;
	}
}
