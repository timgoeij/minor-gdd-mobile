using ColourRun.Cameras;
using ColourRun.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleShark : Laser, IObstacle
{
    private float _maxSpeed = 0;
    public float MaxSpeed {
        get { return _maxSpeed; }
        set { _maxSpeed = value; }
    }

    public bool Rotated = false;
    public override void Start() {
        MaxSpeed = (MaxSpeed != 0) ? MaxSpeed : Random.Range(0.15f, 0.20f);
        base.Start();
    }
    public override void FixedUpdate () {
        DestroyCheck();
        if (_isHit) {
            Color c = GetComponent<SpriteRenderer>().color;
            c.a = 0.5f;
            GetComponent<SpriteRenderer>().color = c;
        }
        CheckTurn(); 
        Move();
    }
    private void CheckTurn() {
        Vector3 bottomleft = new Vector3( 
            transform.position.x - GetComponent<SpriteRenderer>().bounds.extents.x, 
            transform.position.y - GetComponent<SpriteRenderer>().bounds.extents.y,
            0
        );

        Vector3 topRight = new Vector3( 
            transform.position.x + GetComponent<SpriteRenderer>().bounds.extents.x, 
            transform.position.y + GetComponent<SpriteRenderer>().bounds.extents.y,
            0
        );

        Collider2D leftHit  = Physics2D.Raycast( bottomleft, (Rotated) ? Vector2.right : Vector2.up,   transform.localScale.y ).collider;
        Collider2D rightHit = Physics2D.Raycast( topRight,   (Rotated) ? Vector2.left  : Vector2.down, transform.localScale.y ).collider;
        leftHit = (leftHit != null && (Rotated && leftHit.tag == "Wall" || ! Rotated && leftHit.tag == "Floor" || leftHit.GetComponent<Laser>()  != null )) ? leftHit : null; 
        rightHit = (rightHit != null && (Rotated && rightHit.tag == "Wall" || ! Rotated && rightHit.tag == "Floor" || rightHit.GetComponent<Laser>()  != null )) ? rightHit : null; 

        if ((transform.rotation.eulerAngles.z != 270 && leftHit  == null && MaxSpeed > 0) ||
            (transform.rotation.eulerAngles.z != 270 && rightHit == null && MaxSpeed < 0) ||  
            (transform.rotation.eulerAngles.z == 270 && leftHit  == null && MaxSpeed < 0) ||  
            (transform.rotation.eulerAngles.z == 270 && rightHit == null && MaxSpeed > 0) ||
            (leftHit  != null && leftHit.GetComponent<Laser>()  != null) ||
            (rightHit != null && rightHit.GetComponent<Laser>() != null))
        {
            MaxSpeed *= -1;
        }
    }
    private void Move() {
        transform.Translate( Vector2.left * MaxSpeed );
    }
    private void DestroyCheck() {
        if (CameraScreen.ObjectIsBehindCamera(transform)) {
            Destroy(gameObject);
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            MaxSpeed = 0;
        }
        
        base.OnTriggerEnter2D(collision);
    }
    float IObstacle.GetYOffset()
    {
        return -(GetComponent<SpriteRenderer>().bounds.extents.y / 2);
    }
}
