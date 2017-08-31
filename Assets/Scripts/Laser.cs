using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Laser : ColorChangeableObject {

    private Rigidbody2D body;
    private BoxCollider2D col;
    private float width;
    private float height;
    
    // Use this for initialization
	public override void Start () {
        base.Start();

        body = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        body.gravityScale = 0;
        body.isKinematic = true;

        width = GetComponent<SpriteRenderer>().sprite.textureRect.width;
        width /= 25;

        height = GetComponent<SpriteRenderer>().sprite.textureRect.height;
        height /= 100;
        height *= 1.5f;

        FindObjectOfType<InputManager>().add(this);
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2((transform.position.x - width), (transform.position.y - height / 2)), Vector2.up);

        Debug.DrawRay(new Vector2((transform.position.x - width), (transform.position.y - height / 2)), Vector2.up, Color.red);

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

    private void OnTriggerExit2D(Collider2D collision)
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
        }
    }
}
