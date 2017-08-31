using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : ColorChangeableObject {

	private float maxSpeed = 0.1f;
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
		transform.Translate( Vector3.right * speed );
	}
}
