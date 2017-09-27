using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCircle : MonoBehaviour {

	static List<Color> colors = new List<Color> {
		Color.cyan,
		Color.gray,
		Color.green
	};

	static int colorIndex = 0;

	private Vector3 _shrinkSpeed = new Vector3(5,5,0);

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().color = GetColor();
		transform.localScale = new Vector3(100, 100, 100);
	}

	private Color GetColor() {
		Color c = TouchCircle.colors[ TouchCircle.colorIndex ];
		c.a = 0.1f;

		TouchCircle.colorIndex++;

		if (TouchCircle.colorIndex >= TouchCircle.colors.Count) {
			TouchCircle.colorIndex = 0;
		}

		return c;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
			if (transform.localScale.x <= 0 || transform.localScale.y <= 0) {
				Destroy(gameObject);
			}

			transform.localScale -= _shrinkSpeed;

			transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
	}
}
