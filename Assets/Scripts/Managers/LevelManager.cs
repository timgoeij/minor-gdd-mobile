using ColourRun.Cameras;
using ColourRun.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ColourRun.Controller;

namespace ColourRun.Managers
{
    public class LevelManager : MonoBehaviour
    {

        [SerializeField]
        private GameObject _gate;

        private GameObject _player;

        private List<LevelPattern> _levelPatterns = new List<LevelPattern>();
        private LevelPattern _currentPattern = null;

        public LevelPattern currentPattern
        {
            get
            {
                return _currentPattern;
            }
        }

        private float _timeSinceLastObstacle = 0;
        private float _currentPatternTime = 0;
        private int _timessObstacleSkipped = 0;
        private float _quietTime = 0;

        public delegate void NotifyAddingNewColor(LevelManager levelManager);
        public NotifyAddingNewColor notitfyAddingNewColor;

        public List<LevelPattern> levelPatterns
        {
            get
            {
                return _levelPatterns;
            }
            private set
            {
                _levelPatterns = value;
            }
        }

        public GameObject _lastAddedObstacle;
        public GameObject _lastObstacleOfPreviousPattern;

        public List<Color> _colorsToAdd;
        public LevelPattern _playerInPattern;
        public LevelPattern playerInPattern
        {
            get
            {
                return _playerInPattern;
            }

            set
            {
                _playerInPattern = value;
            }
        }

        private PlayerScript _playerScript;
        private DifficultyManager _difficultyManager;
        private FloorManager _floorManager;
        private GameController _controller;

        void Awake()
        {
            _colorsToAdd = new List<Color>();

            _levelPatterns.Add(new LaserPattern());
            _levelPatterns.Add(new QuickLaserPattern());
            _levelPatterns.Add(new OneQuickTab());
            _levelPatterns.Add(new SameButQuickLasers());
            _levelPatterns.Add(new QuickButRandomPattern());

            _player = GameObject.FindGameObjectWithTag("Player");
            _playerScript = _player.GetComponent<PlayerScript>();
            _difficultyManager = FindObjectOfType<DifficultyManager>();
            _floorManager = FindObjectOfType<FloorManager>();
            _controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        void Start()
        {
            _currentPattern = new StartingPattern();
            _playerInPattern = _currentPattern;
            InitPattern();
        }

        void Update()
        {
            if (!_controller.gameStarted)
            {
                return;
            }

            if (_quietTime < _currentPattern.QuietTime())
            {
                _quietTime += Time.deltaTime;
                return;
            }

            if (!_playerScript.isGrounded && _playerScript.touchedWall)
            {
                return;
            }

            if (
                _lastObstacleOfPreviousPattern != null &&
                _lastObstacleOfPreviousPattern.transform.position.x < _playerScript.transform.position.x &&
                _colorsToAdd.Count != 0
            )
            {
                AddColors();
            }

            if (_currentPatternTime >= _currentPattern.PatternDuration())
            {
                SetNewPattern();
                _lastObstacleOfPreviousPattern = _lastAddedObstacle;
            }
            else
            {
                _currentPatternTime += Time.deltaTime;
            }

            UpdateLevel();
        }

        private void AddColors()
        {
            ColorOrder order = FindObjectOfType<ColorOrder>();

            foreach (Color c in _colorsToAdd)
            {
                ColorManager.AddColor(c);
                notitfyAddingNewColor -= order.OnNotifiedColorAdded;
                notitfyAddingNewColor += order.OnNotifiedColorAdded;

                if (notitfyAddingNewColor != null)
                {
                    notitfyAddingNewColor(this);
                }
            }

            _colorsToAdd = new List<Color>();
        }

        private void SetNewPattern()
        {
            if (_difficultyManager.HasPatternToAdd())
            {
                SetRequiredPattern();
            }
            else
            {
                SetRandomPattern();
            }

            InitPattern();
            SetGate();

            _quietTime = 0;
            _currentPatternTime = 0;
        }

        private void InitPattern()
        {
            _currentPattern.init();
            _floorManager.DescendingOptions(_currentPattern.AllowDescending(), _currentPattern.DescendingChance());
        }
        private void SetGate()
        {
            GameObject instance = Instantiate(_gate);
            instance.GetComponent<PatternGateScript>().pattern = _currentPattern;

            float x = (_player.transform.position.x + CameraScreen.width);

            List<GameObject> floors = FindObjectOfType<FloorManager>().GetFloors();

            GameObject floorPart = floors.Where(
                f => f.transform.position.x < x &&
                         f.transform.position.x + CameraScreen.width > x)
                .FirstOrDefault();

            if (floorPart == null)
            {
                return;
            }

            instance.transform.position = floorPart.transform.position;
        }

        private void SetRequiredPattern()
        {
            PatternToAdd p = _difficultyManager.GetPatternToAdd();
            _currentPattern = p.Pattern;
            p.Added = true;

            if (p.ExtraPatterns.Count != 0)
            {
                AddPatterns(p.ExtraPatterns);
            }

            if (p.Color != null)
            {
                _colorsToAdd.Add(p.Color ?? default(Color));
            }

            if (p.Notification != "")
            {
                FindObjectOfType<NotificationManager>().AddNotification(p.Notification, 2.5f);
            }
        }

        private void AddPatterns(List<LevelPattern> newPatterns)
        {
            _levelPatterns.AddRange(newPatterns);
        }

        private void SetRandomPattern()
        {
            IEnumerable<LevelPattern> availablePatterns = _levelPatterns.Where(p => !p.GetType().Equals(_currentPattern.GetType()));

            _currentPattern = availablePatterns.ToArray()[UnityEngine.Random.Range(0, availablePatterns.ToArray().Count())];
            _currentPattern.init();
        }

        private void UpdateLevel()
        {
            if (_timeSinceLastObstacle < _currentPattern.TimeBetweenObstacles())
            {
                _timeSinceLastObstacle += Time.deltaTime;
                return;
            }

            if (_currentPattern.IsFallingPattern())
            {
                return;
            }

            if (_currentPattern.availableObstacles().Count <= 0)
            {
                return;
            }

            if (_timessObstacleSkipped >= _currentPattern.MaxObstaclesSkippedInRow() || UnityEngine.Random.Range(0, _currentPattern.ObstacleChance()) == 0)
            {
                PlaceObstacle();
            }
            else
            {
                _timessObstacleSkipped++;
            }

            _timeSinceLastObstacle = 0;
        }

        private void PlaceObstacle()
        {
            GameObject obstacle = _currentPattern.availableObstacles()[UnityEngine.Random.Range(0, (_currentPattern.availableObstacles().Count))];
            GameObject instance = Instantiate(obstacle);

            SetObstaclePosition(instance);
            SetObstacleColor(instance);

            _lastAddedObstacle = instance;
            _timessObstacleSkipped = 0;
        }

        private void SetObstaclePosition(GameObject instance)
        {
            float x = (_player.transform.position.x + CameraScreen.width);

            List<GameObject> floors = FindObjectOfType<FloorManager>().GetFloors();
            GameObject floorPart = floors.Where(
                f => f.transform.position.x - f.GetComponent<SpriteRenderer>().bounds.extents.x < x &&
                         f.transform.position.x + f.GetComponent<SpriteRenderer>().bounds.extents.x > x)
                .FirstOrDefault();

            if (floorPart == null)
            {
                return;
            }

            float y = floorPart.transform.position.y + (instance.GetComponent<IObstacle>() != null ? instance.GetComponent<IObstacle>().GetYOffset() : 0);

            instance.transform.position = new Vector3(x, y, 10);
        }

        private void SetObstacleColor(GameObject instance)
        {
            if (instance.GetComponent<Laser>() != null)
            {
                instance.GetComponent<Laser>().SetColor(_currentPattern.GetColor());
            }

            if (instance.GetComponent<Rotator>() != null)
            {
                foreach (Laser laser in instance.GetComponentsInChildren<Laser>())
                {
                    laser.SetColor(_currentPattern.GetColor());
                }
            }

            if (instance.GetComponent<RotationCube>() != null)
            {
                foreach (Laser laser in instance.GetComponentsInChildren<Laser>())
                {
                    laser.SetColor(_currentPattern.GetColor());
                }
            }
        }
    }
}


