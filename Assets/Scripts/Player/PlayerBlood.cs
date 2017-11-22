using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlood : MonoBehaviour {

	private SpriteRenderer _renderer;
	private Rigidbody2D _rigidbody;

	private Transform _transform;

	void Awake() {
		_renderer  = GetComponent<SpriteRenderer>();
		_rigidbody = GetComponent<Rigidbody2D>();
		_transform = this.transform;
	}
	// Use this for initialization
	void OnEnable () {
		Color c = _renderer.color;
		c.a = Random.Range(0.4f, 1f);
		
		_renderer.color = c;
		_transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
		_rigidbody.AddForce( (Random.onUnitSphere * Random.Range(-30f, 30f)), ForceMode2D.Impulse );	}
}
