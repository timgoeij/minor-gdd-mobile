using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Laser {

    private Vector3 target = Vector3.zero;
    private float speed;
    
    // Use this for initialization
	public override void Start () {

        speed = Random.Range(0.25f, 0.75f);

        base.Start();
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {

        if(target != Vector3.zero)
        {
            transform.position += transform.TransformDirection(target) * speed;
        }

        base.FixedUpdate();
	}

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
            Destroy(gameObject);
    }
}
