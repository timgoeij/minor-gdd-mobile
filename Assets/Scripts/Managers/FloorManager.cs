using ColourRun.Cameras;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColourRun.Managers
{
    public class FloorManager : MonoBehaviour
    {

        [SerializeField]
        private GameObject _gate;

        [SerializeField]
        private GameObject _floorObject;

        private List<GameObject> _floors = new List<GameObject>();
        private float _currentYFloor = 0;

        private int _defaultDescentChance = 10;

        private int _descentChance;
        private bool _allowDescending = false;

        private LevelPattern _fallingPattern = null;

        public LevelPattern fallingPattern
        {
            get
            {
                return _fallingPattern;
            }
            set
            {
                _fallingPattern = value;
            }
        }

        void Start()
        {
            _currentYFloor = -(CameraScreen.height / 2);
            CreateFloors();
        }

        private void CreateFloors()
        {
            CreateFloor(new Vector3(0, _currentYFloor, 10), new Vector3(CameraScreen.width, 0.1f, 1));
            CreateFloor(new Vector3(CameraScreen.width, _currentYFloor, 10), new Vector3(CameraScreen.width, 0.1f, 1));
            CreateFloor(new Vector3(CameraScreen.width * 2, _currentYFloor, 10), new Vector3(CameraScreen.width, 0.1f, 1));
        }

        private void CreateFloor(Vector3 position, Vector3 scale, bool addToFloors = true)
        {
            GameObject floor = Instantiate(_floorObject);

            floor.transform.position = position;
            floor.transform.localScale = scale;

            if (addToFloors)
            {
                _floors.Add(floor);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (fallingPattern != null)
            {
                StartFalling();
                return;
            }

            MoveFloors();
        }

        private void StartFalling()
        {

            bool isLeft = false;

            int numberOfWalls = ((2 * UnityEngine.Random.Range(1, 6)) * UnityEngine.Random.Range(1, 3));

            for (int i = 0; i < numberOfWalls; i++)
            {
                GameObject floor = Instantiate(_floorObject);
                floor.tag = "Wall";
                floor.transform.localScale = new Vector3(CameraScreen.width, 0.1f);

                Vector3 newPos = _floors.OrderByDescending(f => f.transform.position.x).First().transform.position;

                if (isLeft)
                {
                    newPos.x += (CameraScreen.width / 2);
                    newPos.y = _currentYFloor - floor.GetComponent<SpriteRenderer>().bounds.extents.x;

                    if (i == 1)
                    {
                        GameObject gate = Instantiate(_gate);
                        gate.GetComponent<PatternGateScript>().pattern = new FallingGatePattern();
                        gate.transform.position = new Vector2(newPos.x, newPos.y + floor.GetComponent<SpriteRenderer>().bounds.extents.x);
                    }

                }
                else
                {
                    newPos.x += CameraScreen.width;
                    newPos.y = _currentYFloor;
                }

                floor.transform.position = newPos;

                floor.transform.Rotate(0, 0, 90);

                SetFallingObstacle(floor, isLeft, i);

                _currentYFloor = floor.transform.position.y;

                isLeft = !isLeft;
            }

            _currentYFloor -= (CameraScreen.width / 2);
            fallingPattern = null;
        }

        private void SetFallingObstacle(GameObject floor, bool isLeft, int i)
        {
            if (fallingPattern.availableObstacles().Count <= 0)
            {
                return;
            }

            GameObject obstacle = fallingPattern.availableObstacles()[UnityEngine.Random.Range(0, (fallingPattern.availableObstacles().Count))];

            if (obstacle.GetComponent<TriangleShark>() == null && !isLeft)
            {
                return;
            }


            GameObject instance = Instantiate(obstacle);
            instance.transform.Rotate(0, 0, 90);

            if (instance.GetComponent<Laser>() != null)
            {
                instance.GetComponent<Laser>().SetColor(fallingPattern.GetColor());
            }

            if (instance.GetComponent<RotationCube>() != null)
            {
                foreach (Laser laser in instance.GetComponentsInChildren<Laser>())
                {
                    laser.SetColor(fallingPattern.GetColor());
                }
            }

            if (instance.GetComponent<TriangleShark>() != null && isLeft)
            {
                instance.transform.Rotate(0, 0, 180);
            }

            Vector3 pos = floor.transform.position;

            if (instance.GetComponent<TriangleShark>() == null)
            {
                pos.x += (CameraScreen.width / 4);
            }



            instance.transform.position = pos;

        }

        private void Descent()
        {
            float height;

            Type currentPatternType = FindObjectOfType<LevelManager>().currentPattern.GetType();

            if (currentPatternType == typeof(DownhillCircles) ||
                    currentPatternType == typeof(DownHillTriangles) ||
                    currentPatternType == typeof(TrianglesAndCircles) ||
                    currentPatternType == typeof(FirstTriangles))
            {
                height = 5f;
            }
            else
            {
                height = UnityEngine.Random.Range(2.5f, 5f);
            }

            _currentYFloor -= height;
        }

        private bool DescentChance()
        {
            return (UnityEngine.Random.Range(0, _descentChance) == 0);
        }

        public void DescendingOptions(bool allowDescending, int? descentChance)
        {

            _descentChance = (descentChance != null) ? descentChance ?? default(int) : _defaultDescentChance;
            _allowDescending = allowDescending;
        }

        private void MoveFloors()
        {
            foreach (GameObject floor in _floors)
            {
                if (CameraScreen.ObjectIsBehindCamera(floor.transform))
                {

                    if (_allowDescending && DescentChance())
                    {
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

        public List<GameObject> GetFloors()
        {
            return _floors;
        }

        public float GetYPos()
        {
            return _currentYFloor;
        }
    }

}
