using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour {
	void Update () {
		if (tag == "Wall" && CameraScreen.ObjectIsBehindCamera(transform)) {
			Destroy(gameObject);
		}
	}
}
