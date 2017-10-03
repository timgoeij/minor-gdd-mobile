using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkScript : MonoBehaviour {

	private float _timeToFade;
	private float _timeAlive = 0;

	private SpriteRenderer _renderer;
	private Rigidbody2D _rigidBody;
	// Use this for initialization
	void Awake() {
		_renderer = GetComponent<SpriteRenderer>();
		_rigidBody = GetComponent<Rigidbody2D>();
	}

	void OnEnable () {
		_rigidBody.AddForce( (Random.onUnitSphere * Random.Range(-30f, 30f)), ForceMode2D.Impulse );
		
		Color c = _renderer.color;
		c.a = 1;

		_renderer.color = c;
		
		_timeAlive = 0;
		_timeToFade = UnityEngine.Random.Range(0.2f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if ( ! gameObject.activeInHierarchy) {
			return;
		}

		if (_timeAlive >= _timeToFade) {
			Color c = _renderer.color;
			c.a -= 0.01f;

			if (c.a <= 0) {
				gameObject.SetActive(false);
				return;
			}

			_renderer.color = c;

			return;
		}

		_timeAlive += Time.deltaTime;
	}
}
