using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	private List<GameObject> _floors = new List<GameObject>();

	[SerializeField]
	private GameObject _floorObject;

	[SerializeField]
	private GameObject _laserObject;

	private int _bpm = 120;
	private float _timeSinceLastObstacle = 0;
	private int _obstacleChance = 100;

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
		FillFloor();
	}

	void FillFloor() {
		if (_timeSinceLastObstacle > ((_bpm * 60) / 1000)) {
			if (Random.Range(0, _obstacleChance) == 0) {
				SetLaser();
			}
		} else {
			_timeSinceLastObstacle += Time.deltaTime;
		}
	}

	void SetLaser() {
		GameObject lastFloor = _floors.OrderByDescending(f => f.transform.position.x).First();

		GameObject laser = Instantiate(_laserObject);
		laser.transform.position = new Vector3 (
			lastFloor.transform.position.x,
			lastFloor.transform.position.y + (laser.GetComponent<SpriteRenderer>().bounds.extents.y * 1.5f),
			10
		);
		laser.GetComponent<ColorChangeableObject>().ChangeColor( true );
	}

	void HandleFloorManagement() {
		foreach(GameObject floor in _floors) {
			if (CameraScreen.ObjectIsBehindCamera(floor.transform)) {
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
