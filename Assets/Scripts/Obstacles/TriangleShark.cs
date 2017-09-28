using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleShark : Laser, IObstacle {

    private float speed = 0;
    private float maxSpeed = 0f;

    [SerializeField]
    private bool alreadyChanged = false;

    [SerializeField]
    private bool alreadyCollide = false;

    [SerializeField]
    private bool rotated;

    public bool Rotated
    {
        get { return rotated; }
        set { rotated = value; }
    }

    private enum LaserBehaviour
    {
        Right,
        Left,
        Idle,
        Death,
    }

    [SerializeField]
    private LaserBehaviour currentBehaviour = LaserBehaviour.Idle;

    private Vector3 direction = Vector3.zero;

    private float rayXRightPos;
    private float rayYRightPos;

    private float rayXLeftPos;
    private float rayYLeftPos;


    private Vector3 rayDirection;


    public float MaxSpeed
    {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }

    // Use this for initialization
    public override void Start () {

        if (maxSpeed == 0)
            maxSpeed = Random.Range(0.15f, 0.20f);

        base.Start();
		
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {

        rayXRightPos = rotated ? -GetComponent<SpriteRenderer>().bounds.extents.x * 0.5f : GetComponent<SpriteRenderer>().bounds.extents.x;
        rayYRightPos = rotated ? -GetComponent<SpriteRenderer>().bounds.extents.y : GetComponent<SpriteRenderer>().bounds.extents.y * 0.5f;

        rayXLeftPos = rotated ? GetComponent<SpriteRenderer>().bounds.extents.x * 0.5f : GetComponent<SpriteRenderer>().bounds.extents.x;
        rayYLeftPos = rotated ? GetComponent<SpriteRenderer>().bounds.extents.y : GetComponent<SpriteRenderer>().bounds.extents.y * 0.5f;

        rayDirection =  rotated && transform.eulerAngles.y == 270 ? transform.TransformDirection(Vector3.down) : transform.TransformDirection(Vector3.up);

        if (_isHit)
        {
            Color c = GetComponent<SpriteRenderer>().color;
            c.a = 0.5f;
            GetComponent<SpriteRenderer>().color = c;
        }

        if (speed <= maxSpeed)
            speed += 0.1f;

        RaycastHit2D rightDownHit = Physics2D.Raycast(new Vector2(transform.position.x + rayXRightPos, 
            transform.position.y - rayYRightPos), rayDirection, 0.96f);

        RaycastHit2D leftDownHit = Physics2D.Raycast(new Vector2(transform.position.x - rayXLeftPos,
            transform.position.y - rayYLeftPos), rayDirection, 0.96f);

        RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + GetComponent<SpriteRenderer>().bounds.extents.x,
            transform.position.y - GetComponent<SpriteRenderer>().bounds.extents.y * 0.5f), transform.TransformDirection(Vector2.right), 1f);
        RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x - GetComponent<SpriteRenderer>().bounds.extents.x,
            transform.position.y - GetComponent<SpriteRenderer>().bounds.extents.y * 0.5f), transform.TransformDirection(Vector2.left), 1f);

        /*Debug.DrawRay(new Vector2(transform.position.x + rayXRightPos,
            transform.position.y - rayYRightPos), rayDirection, Color.blue);*/

        Debug.DrawRay(new Vector2(transform.position.x - rayXLeftPos,
            transform.position.y - rayYLeftPos), rayDirection, Color.red);


        if (currentBehaviour != LaserBehaviour.Idle)
        {
            if (rightDownHit.collider != null && leftDownHit.collider != null)
                alreadyChanged = false;

            if (rightHit.collider == null && leftHit.collider == null)
                alreadyCollide = false;

            if (rightHit.collider != null && !alreadyCollide && currentBehaviour != LaserBehaviour.Death)
            {
                ChangeBehaviour();

                if (rightHit.collider.GetComponent<TriangleShark>() != null)
                    rightHit.collider.GetComponent<TriangleShark>().ChangeBehaviour();

                alreadyCollide = true;
            }
            else if (leftHit.collider != null && !alreadyCollide && currentBehaviour != LaserBehaviour.Death)
            {
                ChangeBehaviour();

                if (leftHit.collider.GetComponent<TriangleShark>() != null)
                    leftHit.collider.GetComponent<TriangleShark>().ChangeBehaviour();

                alreadyCollide = true;
            }

            if (leftDownHit.collider == null && rightDownHit.collider == null && CameraScreen.ObjectIsBehindCamera(transform))
            {
                Destroy(gameObject);
            }
            else if(leftDownHit.collider == null && rightDownHit.collider == null && currentBehaviour != LaserBehaviour.Death)
            {
                ChangeBehaviour();
            }
            else if (leftDownHit.collider == null && !alreadyChanged && currentBehaviour != LaserBehaviour.Death)
            {
                ChangeBehaviour();
                alreadyChanged = true;
            }
            else if(rightDownHit.collider == null && !alreadyChanged && currentBehaviour != LaserBehaviour.Death)
            {
                ChangeBehaviour();
                alreadyChanged = true;
            }

            if (currentBehaviour == LaserBehaviour.Left)
                direction = Vector3.left;
            else if (currentBehaviour == LaserBehaviour.Right)
                direction = Vector3.right;
            else
                direction = Vector3.zero;

            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.TransformDirection(direction), speed);
        }
        else
        {
            if (leftDownHit.collider != null || rightDownHit.collider != null)
            {
                currentBehaviour = (LaserBehaviour)Random.Range(0, 2);
            }
        }
    }

    public void ChangeBehaviour()
    {
        if (currentBehaviour == LaserBehaviour.Left)
            currentBehaviour = LaserBehaviour.Right;
        else
            currentBehaviour = LaserBehaviour.Left;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Player"))
        {
            currentBehaviour = LaserBehaviour.Death;
        }
    }

    float IObstacle.GetYOffset()
    {
        return 0;
    }
}
