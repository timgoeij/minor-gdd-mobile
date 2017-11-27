using ColourRun.Cameras;
using ColourRun.Interfaces;
using ColourRun.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Backgrounds.BackgroundItems
{
    public class ByeByeItem : MonoBehaviour, IInputTrigger
    {

        // Use this for initialization
        private float _speed;

        [SerializeField]
        private List<Sprite> _sprites = new List<Sprite>();

        private GameObject _player;
        private List<Color> _colors = new List<Color> {
        Color.gray,
        Color.cyan,
        Color.magenta,
        Color.blue,
        Color.green,
        Color.white
    };

        private float _timeSinceLastUpdate = 0;
        private bool _gotBiggerLastUpdate = false;
        private Vector3 _scaler;
        private bool _isBigOne;
        private bool _fadeOut = false;
        public void Trigger()
        {
            GetComponent<SpriteRenderer>().color = GetRandomColor();
        }

        private SpriteRenderer _renderer;
        private Transform _transform;

        private InputManager _inputManager;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _transform = transform;

            _inputManager = FindObjectOfType<InputManager>();
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        void OnEnable()
        {
            _fadeOut = false;
            _timeSinceLastUpdate = 0;
            _inputManager.add(this);
            CreateItem();
        }

        private void CreateItem()
        {
            _transform.localScale = new Vector3(1, 1, 1);

            _isBigOne = (UnityEngine.Random.Range(0, 30) == 0);

            float scale = (_isBigOne) ? Random.Range(1.5f, 3f) : Random.Range(0.5f, 1.5f);
            _scaler = new Vector3(scale, scale, 0);

            _speed = (_isBigOne) ? (UnityEngine.Random.Range(0.5f, 0.7f) / 10) : (UnityEngine.Random.Range(0.5f, 2f) / 10);

            _renderer.sprite = _sprites[UnityEngine.Random.Range(0, (_sprites.Count - 1))];
            _renderer.color = GetRandomColor();

            _transform.position = new Vector3(
                _player.transform.position.x + (CameraScreen.width * 1.5f),
                Random.Range(Camera.main.transform.position.y, Camera.main.transform.position.y + (CameraScreen.height / 2)),
                10
            );

            transform.localScale *= (_isBigOne) ? UnityEngine.Random.Range(10, 20) : UnityEngine.Random.Range(1, 3);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Rotate(0, 0, UnityEngine.Random.Range(0, 20));
        }

        void OnDisable()
        {
            _inputManager.remove(this);
        }

        void FixedUpdate()
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            if (_fadeOut)
            {
                Color c = _renderer.color;
                c.a -= 0.01f;

                if (c.a <= 0)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    _renderer.color = c;
                }
            }

            if (_timeSinceLastUpdate > 0.3f)
            {
                if (!_gotBiggerLastUpdate)
                {
                    transform.localScale += _scaler;
                    _gotBiggerLastUpdate = true;
                }
                else
                {
                    transform.localScale -= _scaler;
                    _gotBiggerLastUpdate = false;
                }
                _timeSinceLastUpdate = 0;

            }
            else
            {
                _timeSinceLastUpdate += Time.deltaTime;
            }

            _transform.Translate(Vector3.left * _speed);

            DestoyOnOutOfScreen();
        }

        private Color GetRandomColor()
        {
            Color c = _colors[UnityEngine.Random.Range(0, _colors.Count)];
            c.a = UnityEngine.Random.Range(0.1f, 0.3f);

            return c;
        }

        private void DestoyOnOutOfScreen()
        {
            if (CameraScreen.ObjectIsBehindCamera(_transform) || CameraScreen.ObjectIsUnderCamera(_transform))
            {
                gameObject.SetActive(false);
            }
        }

        public void FadeOut()
        {
            _fadeOut = true;
        }
    }

}

