using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGateScript : MonoBehaviour {

	[SerializeField]
	public LevelPattern pattern;

	void Update () {
		if (CameraScreen.ObjectIsBehindCamera(transform)) {
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) {
			FindObjectOfType<LevelManager>().playerInPattern = pattern;
		}
	}
}
