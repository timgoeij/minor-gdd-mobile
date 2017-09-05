using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DifficultyManager : MonoBehaviour {
	
	[SerializeField]
	private GameObject _laserObject;

	[SerializeField]
	private GameObject _rotatorObject;
	
	[SerializeField]
	private GameObject _UpAndDowner;

	private List<ColorsToAdd> _colorsToAdd = new List<ColorsToAdd>();
	private List<GameObject> _obstacles = new List<GameObject>();
	private List<ObstacleToAdd> _obstaclesToAdd = new List<ObstacleToAdd>();

	private int _obstacleChance = 30;
	private float _timeBetweenObstacles = 1f;

	private int _difficultyUpdateAfterSeconds = 10;
	private int _lastDifficultyUpdate = 0;

	public float timeBetweenObstacles {
		get {
			return _timeBetweenObstacles;	
		}

		set {
			if (value > 0.4f) {
				_timeBetweenObstacles = value;
			}
		}
	}
	public int obstacleChance {
		get {
			return _obstacleChance;
		}

		set {
			if (value > 10) {
				_obstacleChance = value;
			}
		}
	}
	void Start () {
		SetStartingObstacles();
		AddObstaclesToAdd();
		AddColorsToAdd();
	}
	private void SetStartingObstacles() {
		_obstacles.Add(_laserObject);
	}

	private void AddObstaclesToAdd() {
		_obstaclesToAdd.Add( new ObstacleToAdd { obstacle = _rotatorObject, addAfterSeconds = 20 });
		_obstaclesToAdd.Add( new ObstacleToAdd { obstacle = _UpAndDowner, addAfterSeconds = 60 });
	}

	private void AddColorsToAdd() {
		_colorsToAdd.Add( new ColorsToAdd { color = Color.magenta, addAfterSeconds = 40 });
	}

	void Update () {
		int totalTime = (int) ScoreManager.GetRoundedScore();

		ChangeObstacleTime(totalTime);
		AddObstacles(totalTime);
		AddColors(totalTime);
	}

	private void ChangeObstacleTime(int totalTime) {
		if (totalTime != _lastDifficultyUpdate && totalTime % _difficultyUpdateAfterSeconds == 0) {
				timeBetweenObstacles -= 0.1f;
				obstacleChance -= 1;
			_lastDifficultyUpdate = totalTime; 
		} 
	}
	private void AddObstacles(int totalTime) {
		ObstacleToAdd obst = _obstaclesToAdd.Where(o => o.addAfterSeconds == totalTime).FirstOrDefault();

		if (obst == null)  {
			return;
		}

		if (_obstacles.Where(o => o == obst.obstacle).Count() != 0) {
			return;
		}

		_obstacles.Add( obst.obstacle );
	}
	private void AddColors(int totalTime) {
		ColorsToAdd colorToAdd = _colorsToAdd.Where(c => c.addAfterSeconds == totalTime).FirstOrDefault();

		if (colorToAdd == null) {
			return;
		}

		if (ColorManager.colors().Where(c => c == colorToAdd.color).Count() != 0) {
			return;
		}

		ColorManager.AddColor( colorToAdd.color );
		FindObjectOfType<LevelManager>().SetForcedColor( (ColorManager.colors().Count - 1), 15 );
	}

  public List<GameObject> GetObstacles() {
		return _obstacles;
	} 
}
