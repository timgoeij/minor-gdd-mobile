using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Effects
{
    public class TouchCircle : MonoBehaviour
    {

        static List<Color> colors = new List<Color> {
        Color.cyan,
        Color.gray,
        Color.green
    };

        static int colorIndex = 0;

        private Vector3 _shrinkSpeed = new Vector3(5, 5, 0);

        private SpriteRenderer _renderer;
        private Transform _transform;

        private GameObject _player;

        private GameObject _gameObject;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _transform = transform;
            _gameObject = gameObject;
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        // Use this for initialization
        void OnEnable()
        {
            _renderer.color = GetColor();
            _transform.localScale = new Vector3(100, 100, 100);
        }

        private Color GetColor()
        {
            Color c = TouchCircle.colors[TouchCircle.colorIndex];
            c.a = 0.1f;

            TouchCircle.colorIndex++;

            if (TouchCircle.colorIndex >= TouchCircle.colors.Count)
            {
                TouchCircle.colorIndex = 0;
            }

            return c;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!_gameObject.activeInHierarchy)
            {
                return;
            }

            if (_transform.localScale.x <= 0 || _transform.localScale.y <= 0)
            {
                _gameObject.SetActive(false);
            }

            _transform.localScale -= _shrinkSpeed;

            _transform.position = _player.transform.position;
        }
    }
}
