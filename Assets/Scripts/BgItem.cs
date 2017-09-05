using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgItem : MonoBehaviour {

	// Use this for initialization
	private float _speed;

	[SerializeField]
	private List<Sprite> _sprites = new List<Sprite>();

	private GameObject _player;

	private List<Color> _colors = new List<Color> {
		Color.gray,
		Color.cyan,
		Color.yellow
	};

	void Awake () {
		_player = GameObject.FindGameObjectWithTag("Player");

		bool bigOne = (UnityEngine.Random.Range(0, 75) == 0);
		
		_speed = (bigOne) ? (UnityEngine.Random.Range(0.5f, 0.7f) / 10)  : (UnityEngine.Random.Range(0.5f, 2f) / 10);
		
		GetComponent<SpriteRenderer>().sprite = _sprites[ UnityEngine.Random.Range(0, (_sprites.Count - 1)) ];
		GetComponent<SpriteRenderer>().color = GetRandomColor();

		transform.position = new Vector3 (
			_player.transform.position.x + (CameraScreen.width * 1.5f),
			-UnityEngine.Random.Range(Camera.main.transform.position.y, (CameraScreen.height / 3)),
			10
		);

		transform.localScale *= (bigOne) ? UnityEngine.Random.Range(10, 20) : UnityEngine.Random.Range(1, 3);
		transform.Rotate(0, 0, UnityEngine.Random.Range(0, 20));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate( Vector3.left * _speed );
	}

	private Color GetRandomColor() {
		Color c = _colors[ UnityEngine.Random.Range(0, _colors.Count) ];
		c.a = UnityEngine.Random.Range(0.1f, 0.5f);

		return c;
	}
}
