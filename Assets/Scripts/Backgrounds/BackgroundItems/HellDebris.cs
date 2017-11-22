using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Backgrounds.BackgroundItems
{
    public class HellDebris : MonoBehaviour
    {

        private SpriteRenderer _renderer;
        private Rigidbody2D _rigidbody;
        private Transform _transform;
        private GameObject _gameObject;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = transform;
            _gameObject = gameObject;
        }

        // Use this for initialization
        void OnEnable()
        {
            Color c = _renderer.color;
            c.a = Random.Range(0.1f, 0.3f);

            _renderer.color = c;
            _rigidbody.AddForce((Random.onUnitSphere * Random.Range(-30f, 30f)), ForceMode2D.Impulse);

            float scale = Random.Range(1f, 2f);

            _transform.localScale = new Vector3(
                scale,
                scale,
                0
            );
        }

        void Update()
        {
            if (!_gameObject.activeInHierarchy)
            {
                return;
            }

            Color c = _renderer.color;
            c.a -= 0.01f;

            if (c.a <= 0)
            {
                _gameObject.SetActive(false);
            }
            else
            {
                _renderer.color = c;
            }
        }
    }
}


