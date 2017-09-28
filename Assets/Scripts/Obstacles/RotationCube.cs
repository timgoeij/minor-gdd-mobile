using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCube : MonoBehaviour {

    private float speed;

    private Vector3 highPos;
    private Vector3 lowPos;

    private float lastRotation = 0;

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
        Down,
    }

    private laserEnum currentBehaviour = laserEnum.Up;
    private laserEnum lastBehaviour = laserEnum.Idle;

    // Use this for initialization
    void Start () {

        highPos = new Vector3(transform.position.x, transform.position.y + 5.5f);
        lowPos = new Vector3(transform.position.x, transform.position.y - 5.5f);

        speed = Random.Range(0.2f, 0.3f);
        lastRotation = transform.eulerAngles.z;

        IsChangingColor = FindObjectOfType<DifficultyManager>().laserColorChanges;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if ((currentBehaviour == laserEnum.Down && transform.position.y <= lowPos.y) || 
            currentBehaviour == laserEnum.Up && transform.position.y >= highPos.y)
        {

            if(isChangingColor)
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<Laser>().ChangeColor();
                }
            }

            lastBehaviour = currentBehaviour;
            currentBehaviour = laserEnum.Idle;
        }
        else if(currentBehaviour == laserEnum.Down)
        {
            transform.position += Vector3.down * speed;
        }
        else if(currentBehaviour == laserEnum.Up)
        {
            transform.position += Vector3.up * speed;
        }
        else if (currentBehaviour == laserEnum.Idle)
        {

            if ((Mathf.Floor(transform.eulerAngles.z) >= lastRotation + 90) || (Mathf.Floor(transform.eulerAngles.z) == 0 && lastRotation >= 270f))
            {
                ChangeBehaviour();
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x,
                transform.eulerAngles.y, transform.eulerAngles.z + 90), 1f * Time.deltaTime);
                
            }
        }

        if (CameraScreen.ObjectIsBehindCamera(transform))
        {
            Destroy(gameObject);
        }
    }

    private void ChangeBehaviour()
    {
        if (lastBehaviour == laserEnum.Up)
            currentBehaviour = laserEnum.Down;
        else if (lastBehaviour == laserEnum.Down)
            currentBehaviour = laserEnum.Up;

        lastRotation = Mathf.Floor(transform.eulerAngles.z);
            
    }
}
