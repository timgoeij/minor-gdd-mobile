using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : ColorChangeableObject {

	private float maxSpeed = 0.5f;
	private float _speed = 0;
	public float speed {
		get {
			return _speed;
		} 
		set {
			if (value <= maxSpeed) {
				_speed = value;
			}
		}
	}

	// Use this for initialization
    public override void Start()
    {
        base.Start();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		 transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(1, 0, 0), speed);
	}

	public void Hit() {
		FindObjectOfType<ScoreManager>().StopTimer();
		speed = 0;
		FindObjectOfType<GameController>().EndGame();
	}

	public void StartRunning() {
		speed = 0.3f;
	}
}
