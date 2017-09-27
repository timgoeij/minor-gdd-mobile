using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour {

	private float _timeAlive = 0;

	// Use this for initialization
	void Start () {
		Color c = GetComponent<SpriteRenderer>().color;
		c.a = Random.Range(0.4f, 1f);
		GetComponent<SpriteRenderer>().color = c;
		
		//transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
		GetComponent<Rigidbody2D>().AddForce( Vector2.up * Random.Range(1f, 20f), ForceMode2D.Impulse  );
		GetComponent<Rigidbody2D>().AddForce( Vector2.right * Random.Range(1f, 30f), ForceMode2D.Impulse );
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_timeAlive += Time.deltaTime;

		if (_timeAlive >= 1.5f) {
			Color c = GetComponent<SpriteRenderer>().color;
			c.a -= 0.05f;
			GetComponent<SpriteRenderer>().color = c;
			
			Destroy( GetComponent<Rigidbody2D>() );
			Vector3 startPos = transform.position;
			Vector3 endPos = GameObject.FindGameObjectWithTag("Score").transform.position;
			endPos.x += (GameObject.FindGameObjectWithTag("Score").GetComponent<RectTransform>().rect.width / 2);

			endPos = Camera.main.ScreenToWorldPoint( endPos );

			transform.position = Vector3.Lerp(
				startPos,
				endPos,
				0.1f 
			);
		}

		if (GetComponent<SpriteRenderer>().color.a <= 0) {
			Destroy(gameObject);
		}
	}
}
