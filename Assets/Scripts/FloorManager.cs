using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorManager : MonoBehaviour {

	private List<GameObject> _floors = new List<GameObject>();
	private float _currentYFloor = 0;
	
	[SerializeField]
	private GameObject _floorObject;


	private int _defaultDescentChance = 10;

	[SerializeField]
	private int _descentChance;

	[SerializeField]
	private bool _allowDescending = false;

	private bool _goingDown = false;

	public bool goingDown {
		get {
			return _goingDown;
		}
		set {
			_goingDown = value;
		}
	}

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
		if (_goingDown) {
			StartFalling();
			return;
		}


		MoveFloors();
	}

	private void StartFalling() {

		bool isLeft = false;

		int numberOfWalls = 2 * UnityEngine.Random.Range(1,3);

		for (int i = 0; i < numberOfWalls; i++) {
			GameObject floor = Instantiate(_floorObject);
			floor.tag = "Wall";
			floor.transform.localScale = new Vector3(CameraScreen.width, 0.1f);

			Vector3 newPos = _floors.OrderByDescending(f => f.transform.position.x).First().transform.position;
			

			if (isLeft) {
				newPos.x += (CameraScreen.width / 2);
				newPos.y = _currentYFloor - floor.GetComponent<SpriteRenderer>().bounds.extents.x;
			} else {
				newPos.x += CameraScreen.width;
				newPos.y = _currentYFloor;
			}

			floor.transform.position = newPos;

			floor.transform.Rotate(0, 0, 90);

			_currentYFloor = floor.transform.position.y;
			isLeft = !isLeft;
		}

		_currentYFloor -= (CameraScreen.width / 2);
		_goingDown = false;
	}

	private void Descent() {
		float height;

		Type currentPatternType = FindObjectOfType<LevelManager>().currentPattern.GetType();

		if (currentPatternType == typeof(DownhillCircles) ||  
				currentPatternType == typeof(DownHillTriangles) || 
				currentPatternType == typeof(TrianglesAndCircles) || 
				currentPatternType == typeof(FirstTriangles)) {
			height = 5f;
		} else {
			height = UnityEngine.Random.Range(2.5f, 5f);
		}

		_currentYFloor -= height;
	}

	private bool DescentChance() {
		return (UnityEngine.Random.Range(0, _descentChance) == 0);
	}

	public void DescendingOptions(bool allowDescending, int? descentChance) {
		
		_descentChance   = (descentChance != null ) ? descentChance ?? default(int) : _defaultDescentChance;	
		_allowDescending = allowDescending;
	}

	private void MoveFloors() {
		foreach(GameObject floor in _floors) {
			if (CameraScreen.ObjectIsBehindCamera(floor.transform)) {

				if (_allowDescending && DescentChance()) {
					Descent();
				}
					
				floor.transform.position = new Vector3(
					(_floors.OrderByDescending(f => f.transform.position.x).First().transform.position.x + CameraScreen.width),
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
