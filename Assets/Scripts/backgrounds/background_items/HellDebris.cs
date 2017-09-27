using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellDebris : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Color c = GetComponent<SpriteRenderer>().color;
		c.a = Random.Range(0.1f, 0.3f);
		GetComponent<SpriteRenderer>().color = c;
		GetComponent<Rigidbody2D>().AddForce( (Random.onUnitSphere * Random.Range(-30f, 30f)), ForceMode2D.Impulse );

		float scale = Random.Range(1f, 2f);

		transform.localScale = new Vector3(
			scale,
			scale,
			0  
		);
	}

	void Update() {
		Color c = GetComponent<SpriteRenderer>().color;
		c.a -= 0.01f;

		if (c.a <= 0) {
			Destroy(gameObject);
		} else {
			GetComponent<SpriteRenderer>().color = c;
		}
	}
}
