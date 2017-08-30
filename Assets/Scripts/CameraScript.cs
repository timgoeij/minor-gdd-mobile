using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	[SerializeField]
	private GameObject _player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(
			transform.position, 
			new Vector3(
				_player.transform.position.x + CameraScreen.width / 3, 
				_player.transform.position.y + CameraScreen.width / 3, 
				transform.position.z), 
			1f);
	}
}
