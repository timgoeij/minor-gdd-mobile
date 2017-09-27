using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleShark : Laser, IObstacle {

    private float speed = 0;
    private float maxSpeed = 0f;

    private enum LaserBehaviour
    {
        Right,
        Left,
        Death,
    }

    private LaserBehaviour currentBehaviour = LaserBehaviour.Left;

    private Vector3 direction = Vector3.zero;

    public float MaxSpeed
    {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }

    // Use this for initialization
    public override void Start () {

        if (maxSpeed == 0)
            maxSpeed = Random.Range(0.15f, 0.20f);

        currentBehaviour = (LaserBehaviour)Random.Range(0, 2);


        base.Start();
		
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {

        if (_isHit)
        {
            Color c = GetComponent<SpriteRenderer>().color;
            c.a = 0.5f;
            GetComponent<SpriteRenderer>().color = c;
        }

        if (speed <= maxSpeed)
            speed += 0.1f;

        RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + GetComponent<SpriteRenderer>().bounds.extents.x,
            transform.position.y - (GetComponent<SpriteRenderer>().bounds.extents.y * 0.5f)), Vector2.up, 3f);
        RaycastHit2D lefttHit = Physics2D.Raycast(new Vector2(transform.position.x - GetComponent<SpriteRenderer>().bounds.extents.x,
            transform.position.y - (GetComponent<SpriteRenderer>().bounds.extents.y * 0.5f)), Vector2.up, 3f);


        if (lefttHit.collider == null && rightHit.collider == null && CameraScreen.ObjectIsBehindCamera(transform))
        {
            Destroy(gameObject);
        }
        else if (((lefttHit.collider == null || rightHit.collider == null) && currentBehaviour != LaserBehaviour.Death))
        {
            if (currentBehaviour == LaserBehaviour.Left)
                currentBehaviour = LaserBehaviour.Right;
            else
                currentBehaviour = LaserBehaviour.Left;
        }

        if (currentBehaviour == LaserBehaviour.Left)
            direction = Vector3.left;
        else if (currentBehaviour == LaserBehaviour.Right)
            direction = Vector3.right;
        else
            direction = Vector3.zero;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Color c = GetComponent<SpriteRenderer>().color;
            c.a = 0.5f;
            GetComponent<SpriteRenderer>().color = c;

            currentBehaviour = LaserBehaviour.Death;
        }

        base.OnTriggerEnter2D(collision);
    }

    float IObstacle.GetYOffset()
    {
        return 0;
    }
}
