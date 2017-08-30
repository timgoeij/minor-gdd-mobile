using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : ColorChangeableObject {

	private float _speed = 0.1f;

	// Use this for initialization
    public override void Start()
    {
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
		transform.Translate( Vector3.right * _speed );
	}
}
