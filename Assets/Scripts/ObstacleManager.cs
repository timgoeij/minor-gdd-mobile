using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

	private DifficultyManager _difficultyManager;
	private FloorManager _floorManager;

	private GameObject _player;

	private float _timeSinceLastObstacle = 0;
	private List<GameObject> _obstacles = new List<GameObject>();
	// Use this for initialization
	void Start () {
		_difficultyManager = FindObjectOfType<DifficultyManager>();
		_floorManager = FindObjectOfType<FloorManager>();
		_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if ( ! FindObjectOfType<GameController>().gameStarted) {
			return;
		}

		CleanObstacles();
		if (_timeSinceLastObstacle < _difficultyManager.timeBetweenObstacles) {
			_timeSinceLastObstacle += Time.deltaTime;
			return;
		}

		if (Random.Range(0, _difficultyManager.obstacleChance) == 0) {
			SetObstacle();
			_timeSinceLastObstacle = 0;
		}
	}

	private void SetObstacle() {
		List<GameObject> availableObstacles = _difficultyManager.GetObstacles();
		GameObject obstacle = availableObstacles[ Random.Range(0, availableObstacles.Count) ];
		GameObject instance = Instantiate(obstacle);
		
		instance.transform.position = new Vector3 (
			(_player.transform.position.x + CameraScreen.width),
			_floorManager.GetYPos() + (instance.GetComponent<IObstacle>() != null ? instance.GetComponent<IObstacle>().GetYOffset() : 0),
			10
		);
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
}
