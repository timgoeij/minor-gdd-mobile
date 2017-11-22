﻿using ColourRun.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownLaser : Laser {

    private float speed;
    private Vector3 startPos;

    private float timer = 0;
    private float waitTime = 0.5f;

    private bool isChangingColor = false;

    public bool IsChangingColor
    {
        get { return isChangingColor; }
        set { isChangingColor = value; }
    }

    private enum laserEnum
    {
        Up,
        Idle,
        Down
    }

    private laserEnum currentBehaviour = laserEnum.Down;
    private laserEnum lastBehaviour = laserEnum.Idle;

    
    // Use this for initialization
	public override void Start () {

        base.Start();
        speed = Random.Range(0.01f, 0.25f);

        startPos = transform.position;

        IsChangingColor = FindObjectOfType<DifficultyManager>().laserColorChanges;
	}
	
	// Update is called once per frame
	public override void  FixedUpdate () {

        if (transform.position.y > startPos.y && currentBehaviour == laserEnum.Up)
        {
            lastBehaviour = currentBehaviour;
            currentBehaviour = laserEnum.Idle;
        }

        if (currentBehaviour == laserEnum.Down)
        {
            transform.position += Vector3.down * speed;
        }
        else if(currentBehaviour == laserEnum.Up)
        {
            transform.position += Vector3.up * speed;
        }
        else if(currentBehaviour == laserEnum.Idle)
        {
            timer += Time.deltaTime;

            if(timer > waitTime)
            {
                ChangeBehaviour();
                timer = 0;
            }
        }

        base.FixedUpdate();
	}

    private void ChangeBehaviour()
    {
        if (lastBehaviour == laserEnum.Up)
            currentBehaviour = laserEnum.Down;
        else if (lastBehaviour == laserEnum.Down)
            currentBehaviour = laserEnum.Up;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && transform.position.y < collision.transform.position.y)
        {
            if(isChangingColor)
                ChangeColor();

            lastBehaviour = currentBehaviour;
            currentBehaviour = laserEnum.Idle;
        }
    }
}
