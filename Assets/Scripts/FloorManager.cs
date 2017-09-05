using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorManager : MonoBehaviour {

	private List<GameObject> _floors = new List<GameObject>();
	private float _currentYFloor = 0;
	
	[SerializeField]
	private GameObject _floorObject;
	// Use this for initialization
	
	void Start () {
		_currentYFloor = -(CameraScreen.height / 2);
		CreateFloors();
	}
	
	private void CreateFloors() {
		CreateFloor( new Vector3(0, _currentYFloor, 10), new Vector3(CameraScreen.width, 0.1f, 1) );	
		CreateFloor( new Vector3(CameraScreen.width, _currentYFloor, 10), new Vector3(CameraScreen.width, 0.1f, 1) );
		CreateFloor( new Vector3(CameraScreen.width * 2, _currentYFloor, 10), new Vector3(CameraScreen.width, 0.1f, 1) );
	}

	private void CreateFloor(Vector3 position, Vector3 scale, bool addToFloors = true) {
		GameObject floor = Instantiate(_floorObject);

		floor.transform.position = position;
		floor.transform.localScale = scale;
		
		if (addToFloors) {
			_floors.Add(floor);
		}
	}

	// Update is called once per frame
	void Update () {
		MoveFloors();
	}

	private void MoveFloors() {
		foreach(GameObject floor in _floors) {
			if (CameraScreen.ObjectIsBehindCamera(floor.transform)) {
				floor.transform.position = new Vector3(
					(_floors.OrderByDescending(f => f.transform.position.x).First().transform.position.x + (1 * CameraScreen.width)),
					_currentYFloor,
					floor.transform.position.z
				);
			}
		}
	}

	public List<GameObject> GetFloors() {
		return _floors;
	}

	public float GetYPos() {
		return _currentYFloor;
	}
}
