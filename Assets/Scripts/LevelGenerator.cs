using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	private List<GameObject> _floors = new List<GameObject>();

	[SerializeField]
	private GameObject _floorObject;

	// Use this for initialization
	void Start () {		
		createStartingRoom();
	}

	private void createStartingRoom() {
		CreateFloors();
	}

	// Update is called once per frame
	void FixedUpdate () {
		HandleFloorManagement();
	}

	void HandleFloorManagement() {
		foreach(GameObject floor in _floors) {
			if (floor.transform.position.x + CameraScreen.width < Camera.main.transform.position.x) {

				floor.GetComponent<SpriteRenderer>().material.color = Color.red;

				floor.transform.position = new Vector3(
					(_floors.OrderByDescending(f => f.transform.position.x).First().transform.position.x + (1 * CameraScreen.width)),
					floor.transform.position.y,
					floor.transform.position.z
				);
			}
		}
	}

	void CreateFloors() {
		CreateFloor( new Vector3(0, -(CameraScreen.height / 2), 10), new Vector3(CameraScreen.width, 1, 1) );	
		CreateFloor( new Vector3(CameraScreen.width, -(CameraScreen.height / 2), 10), new Vector3(CameraScreen.width, 1, 1) );
		CreateFloor( new Vector3(CameraScreen.width * 2, -(CameraScreen.height / 2), 10), new Vector3(CameraScreen.width, 1, 1) );
	}

	void CreateFloor(Vector3 position, Vector3 scale) {
		GameObject floor = Instantiate(_floorObject);

		floor.transform.position = position;
		floor.transform.localScale = scale;

		_floors.Add(floor);
	}
}
