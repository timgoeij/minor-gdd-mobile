using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlood : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Color c = GetComponent<SpriteRenderer>().color;
		c.a = Random.Range(0.4f, 1f);
		GetComponent<SpriteRenderer>().color = c;
		
		transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
		GetComponent<Rigidbody2D>().AddForce( (Random.onUnitSphere * Random.Range(-30f, 30f)), ForceMode2D.Impulse );
		//GetComponent<Rigidbody2D>().AddForce( Vector2.right * Random.Range(-10f, 30f), ForceMode2D.Impulse );
	}
}
