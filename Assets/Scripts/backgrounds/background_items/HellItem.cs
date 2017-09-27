using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellItem : MonoBehaviour, IInputTrigger {

	[SerializeField]
	private GameObject _debris;
	private float _bloatSpeed = 0.05f;
	private float _floatSpeed = 0.05f;

	private bool _isFloating = false;

	private float _timeSinceLastUpdate = 0;

	private Vector3 _scaler;

	public bool isFloating {
		get {
			return _isFloating;
		}
		private set {
			_isFloating = value;
		}
	} 

	private bool _gotBiggerLastUpdate = true;

	private float _maxSize = 0;



	// Use this for initialization
	void Start () {
		MonoBehaviour.FindObjectOfType<InputManager>().add( this );

		Color color = (UnityEngine.Random.Range(0,2) == 0) ? Color.red : new Color(1f, 0.5f, 0);
		color.a = UnityEngine.Random.Range(0.05f, 0.2f);

		GetComponent<SpriteRenderer>().color = color;

		transform.parent = Camera.main.transform;
		
		transform.localPosition = new Vector3(
			UnityEngine.Random.Range(-(CameraScreen.width / 2), (CameraScreen.width / 2)),
			-(CameraScreen.height / 2),
			11
		);

		float scale = Random.Range(0.5f, 1.5f);
		_scaler = new Vector3(scale, scale, 0);

		_bloatSpeed = UnityEngine.Random.Range(0.05f, 0.1f);
		
		_maxSize = UnityEngine.Random.Range(2f, 10f);

		transform.localScale = new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (UnityEngine.Random.Range(0, 1001) == 0) {
			Explode();
		}

		Bloat();
		Float();
		CheckDestroy();
	}

	private void Float() {
		if (isFloating) {
			if (_timeSinceLastUpdate > 0.3f) {
				if ( ! _gotBiggerLastUpdate) {
					transform.localScale += _scaler;
					_gotBiggerLastUpdate = true;
				} else {
					transform.localScale -= _scaler;
					_gotBiggerLastUpdate = false;
				}
				_timeSinceLastUpdate = 0;
			} else {
				_timeSinceLastUpdate += Time.deltaTime;
			}
			transform.Translate(Vector3.up * _floatSpeed);
		}
	}

	public void Explode() {
		for(int i = 0; i < Random.Range(15, 25); i++) {
			GameObject d = Instantiate(_debris);
			d.transform.position = transform.position;
			d.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
		}

		SelfDestruct();
	}

	private void Bloat() {
		if (transform.localScale.x < _maxSize) {
			transform.localScale += new Vector3(_bloatSpeed, _bloatSpeed, 0);
		} else {
			isFloating = true;
		} 
	}

	private void CheckDestroy() {
		if (transform.position.y > Camera.main.transform.position.y + CameraScreen.height) {
			SelfDestruct();
		}
	}

	private void SelfDestruct() {
			MonoBehaviour.FindObjectOfType<InputManager>().remove( this );
			Destroy(gameObject);
	}

  public void Trigger()
  {
		Color color = GetComponent<SpriteRenderer>().color;

    color.g = UnityEngine.Random.Range(0f, 0.5f);

		GetComponent<SpriteRenderer>().color = color;
  }
}
