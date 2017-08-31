using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	private List<GameObject> _floors = new List<GameObject>();
	private List<GameObject> _obstacles = new List<GameObject>();

	private GameObject _player;

	[SerializeField]
	private GameObject _floorObject;

	[SerializeField]
	private GameObject _laserObject;

	private float _timeSinceLastObstacle = 0;
	private int _obstacleChance = 50;
	private float _timeBetweenObstacles = 1f;

	private int _difficultyUpdateAfterSeconds = 10;
	private int _lastDifficultyUpdate = 0;
	public float timeBetweenObstacles {
		get {
			return _timeBetweenObstacles;	
		}

		set {
			if (value > 0.3f) {
				_timeBetweenObstacles = value;
			}
		}
	}

	private bool _gameHasStarted = false;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");		
		createStartingRoom();
	}

	private void createStartingRoom() {
		CreateFloors();
	}

	public void startGame() {
		_gameHasStarted = true;
		_player.GetComponent<PlayerScript>().speed = 0.1f;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if ( ! _gameHasStarted) {
			return;
		}
		
		HandleFloorManagement();
		HandleDifficultyManagement();
		HandleObstacleManagement();
	}

	private void HandleDifficultyManagement() {
		int totalTime = (int) Math.Round(ScoreManager.totalTime());

		if (totalTime != _lastDifficultyUpdate && totalTime % _difficultyUpdateAfterSeconds == 0) {
			if (UnityEngine.Random.Range(0, 5) == 0) {
				_player.GetComponent<PlayerScript>().speed += 0.05f;
			} else {
				timeBetweenObstacles -= 0.1f;
			}

			_lastDifficultyUpdate = totalTime;
		}
	}

	private void HandleObstacleManagement() {
		SetObstacles();
		CleanObstacles();
	}

	private void CleanObstacles() {
		for(int i = 0; i < _obstacles.Count; i++) {
			GameObject obs = _obstacles[i];
			if (CameraScreen.ObjectIsBehindCamera(obs.transform)) {
				_obstacles.Remove(obs);
				Destroy(obs);
			}
		}
	}

	private void SetObstacles() {
			if (_timeSinceLastObstacle > _timeBetweenObstacles) {
				if (UnityEngine.Random.Range(0, _obstacleChance) == 0) {
					SetLaser();
					_timeSinceLastObstacle = 0;
				}
			} else {
				_timeSinceLastObstacle += Time.deltaTime;
			}
		}

	private void SetLaser() {
			GameObject lastFloor = _floors.OrderByDescending(f => f.transform.position.x).First();

			GameObject laser = Instantiate(_laserObject);
			laser.transform.position = new Vector3 (
				_player.transform.position.x + CameraScreen.width,
				lastFloor.transform.position.y + (laser.GetComponent<SpriteRenderer>().bounds.extents.y * 1.5f),
				10
			);

			_obstacles.Add(laser);
		}

	private void HandleFloorManagement() {
		MoveFloors();
	}

	private void MoveFloors() {
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

	private void CreateFloors() {
		CreateFloor( new Vector3(0, -(CameraScreen.height / 2), 10), new Vector3(CameraScreen.width, 1, 1) );	
		CreateFloor( new Vector3(CameraScreen.width, -(CameraScreen.height / 2), 10), new Vector3(CameraScreen.width, 1, 1) );
		CreateFloor( new Vector3(CameraScreen.width * 2, -(CameraScreen.height / 2), 10), new Vector3(CameraScreen.width, 1, 1) );
	}

	private void CreateFloor(Vector3 position, Vector3 scale, bool addToFloors = true) {
		GameObject floor = Instantiate(_floorObject);

		floor.transform.position = position;
		floor.transform.localScale = scale;
		
		if (addToFloors) {
			_floors.Add(floor);
		}
	}
}
