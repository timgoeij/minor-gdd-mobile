using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : ColorChangeableObject {
    
    private Rigidbody2D body;
    private BoxCollider2D col;

    private Vector3 startPos;
    
    // Use this for initialization
    public override void Start()
    {
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        startPos = transform.position;

        body.gravityScale = 0;
        body.velocity = new Vector2(1, 0);

        base.Start();
    }

    // Update is called once per frame
    void Update () {


		
	}

    private void ResetPos()
    {
        transform.position = startPos;
    }
}
