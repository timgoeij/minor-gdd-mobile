using System.Collections;
using System.Collections.Generic;
using ColourRun.Interfaces;
using UnityEngine;
using ColourRun.Managers;

namespace ColourRun.Backgrounds.BackgroundItems
{
    public class LightCasterScript : MonoBehaviour, IInputTrigger
    {

        private Transform _cameraTransform;
        private Transform _transform;
        private GameObject _gameObject;
        private SpriteRenderer _renderer;

        private InnerCaster _innerCaster;

        private int _colorIndex = 0;
        private List<Color> _colors;

        private InputManager _inputManager;

        void Awake()
        {
            _transform = transform;
            _gameObject = gameObject;
            _renderer = GetComponent<SpriteRenderer>();

            _innerCaster = GetComponentInChildren<InnerCaster>();
            _inputManager = FindObjectOfType<InputManager>();
            _cameraTransform = Camera.main.transform;

            _colors = new List<Color> {
            Color.blue,
            Color.red,
            Color.magenta
        };
        }

        void Start()
        {
            SetColor();
            _inputManager.add(this);
        }

        void FixedUpdate()
        {
            if (!_gameObject.activeInHierarchy)
            {
                return;
            }

            Vector3 camPos = _cameraTransform.position;
            camPos.z = 21;
            transform.position = camPos;

            _transform.Rotate(0, 0, -1f);
        }

        private void SetColor()
        {
            Color c = _colors[_colorIndex];
            c.a = 0.1f;

            _renderer.color = c;

            int newIndex = _colorIndex + 1;

            if (newIndex >= _colors.Count)
            {
                newIndex = 0;
            }

            _innerCaster.SetColor(_colors[newIndex]);
        }

        public void Trigger()
        {
            _colorIndex++;

            if (_colorIndex >= _colors.Count)
            {
                _colorIndex = 0;
            }

            SetColor();
        }

        void OnDestroy()
        {
            _inputManager.remove(this);
        }
    }

}


