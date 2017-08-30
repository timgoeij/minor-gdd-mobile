using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Laser : ColorChangeableObject {

    private Rigidbody2D body;
    private BoxCollider2D col;
    private float width;
    
    // Use this for initialization
	public override void Start () {
        base.Start();

        body = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        body.gravityScale = 0;
        body.isKinematic = true;

        width = GetComponent<SpriteRenderer>().sprite.textureRect.width;
        width /= 100;

        FindObjectOfType<InputManager>().add(this);
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2((transform.position.x - width / 2), transform.position.y),Vector2.left);

        Debug.DrawRay(new Vector2((transform.position.x - width / 2), transform.position.y), Vector2.left, Color.red);

        if(hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                ColorChangeableObject player = hit.collider.GetComponent<ColorChangeableObject>();
                

                if(player.GetCurrentColor() == colorChanges)
                {
                    col.isTrigger = true;
                }
                else
                {
                    col.isTrigger = false;
                }
            }
        }
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            FindObjectOfType<InputManager>().remove(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("isCollision");
        }
    }
}
