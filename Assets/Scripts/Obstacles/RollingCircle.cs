using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCircle : Laser {

    private float speed = 0;
    private float maxSpeed = 0f;

    private enum LaserBehaviour
    {
        Right,
        Left
    }

    public float MaxSpeed
    {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }

    public override void Start()
    {
        if (maxSpeed == 0)
            maxSpeed = Random.Range(0.15f, 0.20f);

        base.Start();
    }
    
    // Update is called once per frame
    public override void FixedUpdate () {

        if (speed <= maxSpeed)
            speed += 0.1f;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left, speed);

        base.FixedUpdate();
	}
}
