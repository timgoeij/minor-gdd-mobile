using ColourRun.Cameras;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour, ColourRun.Interfaces.IObstacle
{

	private float _rotationSpeed;

	// Use this for initialization
	void Start () {
		_rotationSpeed = UnityEngine.Random.Range(0.5f, 2f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(0, 0, 1 * _rotationSpeed);

        if (CameraScreen.ObjectIsBehindCamera(transform))
        {
            Destroy(gameObject);
        }
    }

  float ColourRun.Interfaces.IObstacle.GetYOffset()
  {
    return (GetComponentInChildren<SpriteRenderer>().bounds.size.y * 2.5f);
  }
}

