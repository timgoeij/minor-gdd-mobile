using ColourRun.Cameras;
using ColourRun.Interfaces;
using ColourRun.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Backgrounds.BackgroundItems
{
    public class HellItem : MonoBehaviour, IInputTrigger
    {
        [SerializeField]
        private GameObject _debris;
        private float _bloatSpeed = 0.05f;
        private float _floatSpeed = 0.05f;

        private bool _isFloating = false;

        private float _timeSinceLastUpdate = 0;

        private Vector3 _scaler;

        public bool isFloating
        {
            get
            {
                return _isFloating;
            }
            private set
            {
                _isFloating = value;
            }
        }

        private bool _gotBiggerLastUpdate = true;

        private float _maxSize = 0;

        private InputManager _inputManager;
        private SpriteRenderer _renderer;
        private Transform _transform;

        public GameObject GetGameObject
        {
            get
            {
                return gameObject;
            }
        }

        void Awake()
        {
            _inputManager = MonoBehaviour.FindObjectOfType<InputManager>();

            _transform = transform;
            _transform.parent = Camera.main.transform;

            _renderer = GetComponent<SpriteRenderer>();
        }

        // Use this for initialization
        void OnEnable()
        {
            _inputManager.add(this);

            Color color = (UnityEngine.Random.Range(0, 2) == 0) ? Color.red : new Color(1f, 0.5f, 0);
            color.a = UnityEngine.Random.Range(0.05f, 0.2f);

            _renderer.color = color;

            _transform.localPosition = new Vector3(
                UnityEngine.Random.Range(-(CameraScreen.width / 2), (CameraScreen.width / 2)),
                -(CameraScreen.height / 2),
                11
            );

            float scale = Random.Range(0.5f, 1.5f);
            _scaler = new Vector3(scale, scale, 0);

            _bloatSpeed = UnityEngine.Random.Range(0.05f, 0.1f);

            _maxSize = UnityEngine.Random.Range(2f, 10f);

            _transform.localScale = new Vector3(0, 0, 0);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            if (UnityEngine.Random.Range(0, 1001) == 0)
            {
                Explode();
            }

            Bloat();
            Float();
            CheckDestroy();
        }

        private void Float()
        {
            if (isFloating)
            {
                if (_timeSinceLastUpdate > 0.3f)
                {
                    if (!_gotBiggerLastUpdate)
                    {
                        _transform.localScale += _scaler;
                        _gotBiggerLastUpdate = true;
                    }
                    else
                    {
                        _transform.localScale -= _scaler;
                        _gotBiggerLastUpdate = false;
                    }
                    _timeSinceLastUpdate = 0;
                }
                else
                {
                    _timeSinceLastUpdate += Time.deltaTime;
                }
                _transform.Translate(Vector3.up * _floatSpeed);
            }
        }

        public void Explode()
        {
            for (int i = 0; i < Random.Range(5, 10); i++)
            {
                GameObject d = PoolManager.GetItem("HellDebris");
                if (d != null)
                {
                    d.transform.position = transform.position;
                    d.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
                    d.SetActive(true);
                }
            }

            SelfDestruct();
        }

        private void Bloat()
        {
            if (_transform.localScale.x < _maxSize)
            {
                _transform.localScale += new Vector3(_bloatSpeed, _bloatSpeed, 0);
            }
            else
            {
                isFloating = true;
            }
        }

        private void CheckDestroy()
        {
            if (CameraScreen.ObjectIsAboveCamera(_transform))
            {
                SelfDestruct();
            }
        }

        private void SelfDestruct()
        {
            _inputManager.remove(this);
            gameObject.SetActive(false);
        }

        public void Trigger()
        {
            Color color = _renderer.color;
            color.g = UnityEngine.Random.Range(0f, 0.5f);

            _renderer.color = color;
        }
    }

}

