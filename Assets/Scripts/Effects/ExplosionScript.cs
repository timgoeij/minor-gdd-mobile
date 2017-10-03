using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Effects
{
    public class ExplosionScript : MonoBehaviour
    {
        private float _timeAlive = 0;
        private float _explosionSize = 2f;

        private Transform _transform;
        private SpriteRenderer _renderer;

        private GameObject _gameObject;

        void Awake() {
            _transform = transform;
            _renderer = GetComponent<SpriteRenderer>();
            _gameObject = gameObject;
        }

        // Use this for initialization
        void OnEnable()
        {
            _transform.localScale = new Vector3(7f, 7f, 0f) * _explosionSize;
            _renderer.color = Color.white;
            _timeAlive = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if ( ! _gameObject.activeInHierarchy) {
                return;
            }

            if (_timeAlive > 0.03f)
            {
                _renderer.color = Color.black;
            }

            if (_timeAlive > 0.05f)
            {
                _renderer.color = Color.white;
            }

            if (_timeAlive > 0.08f)
            {
                _renderer.color = Color.black;
            }

            _transform.localScale -= new Vector3(1.5f, 1.5f, 0) * _explosionSize;

            _timeAlive += Time.deltaTime;

            if (_transform.localScale.x < 0.5f) {
                _gameObject.SetActive(false);
            }
        }
    }
}


