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
				_player.transform.position.x + CameraScreen.width / GetDivider(), 
				_player.transform.position.y + CameraScreen.width / GetDivider(), 
				transform.position.z), 
			1f);
	}

	private int GetDivider() { 
		if (Camera.main.aspect >= 1.7)
		{
				return 16;
		}
		else if (Camera.main.aspect >= 1.5)
		{
				return 3;
		}
		else
		{
				return 4;
		}
	} 
}
