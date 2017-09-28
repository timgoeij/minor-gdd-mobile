using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Laser {

    private Vector3 target = Vector3.zero;
    private float speed;
    
    // Use this for initialization
	public override void Start () {

        speed = Random.Range(0.3f, 0.5f);

        base.Start();

        SetColor();
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {

        if(target != Vector3.zero)
        {
            transform.position += target * speed;
        }

        base.FixedUpdate();
	}

    public void SetColor() {
        Color c = GetCurrentColor();

        c.a = 0.5f;

        GetComponentInChildren<TrailRenderer>().startColor = c;
        GetComponentInChildren<TrailRenderer>().endColor = c;
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public override void ChangeColor() {
        base.ChangeColor();
        SetColor();
    }
}
