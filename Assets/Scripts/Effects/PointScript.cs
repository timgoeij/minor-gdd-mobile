using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Effects
{
    public class PointScript : MonoBehaviour
    {

        private float _timeAlive = 0;

        private SpriteRenderer _renderer;
        private Rigidbody2D _rigidbody;

        private GameObject _gameObject;
        private Transform _transform;

        private static GameObject _score;
        private static Transform _scoreTransform;
        private static RectTransform _scoreRectTransform;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _gameObject = gameObject;
            _transform = transform;

            if (_score == null)
            {
                _score = GameObject.FindGameObjectWithTag("Score");
                _scoreTransform = _score.transform;
                _scoreRectTransform = _score.GetComponent<RectTransform>();
            }
        }

        // Use this for initialization
        void OnEnable()
        {
            _timeAlive = 0;
            Color c = _renderer.color;
            c.a = Random.Range(0.4f, 1f);

            _renderer.color = c;

            _rigidbody.AddForce(Vector2.up * Random.Range(1f, 20f), ForceMode2D.Impulse);
            _rigidbody.AddForce(Vector2.right * Random.Range(1f, 30f), ForceMode2D.Impulse);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!_gameObject.activeInHierarchy)
            {
                return;
            }

            _timeAlive += Time.deltaTime;

            if (_timeAlive >= 1.5f)
            {
                Color c = _renderer.color;
                c.a -= 0.05f;

                _renderer.color = c;

                Vector3 startPos = _transform.position;
                Vector3 endPos = _scoreTransform.position;
                endPos.x += (_scoreRectTransform.rect.width / 2);

                endPos = Camera.main.ScreenToWorldPoint(endPos);

                _transform.position = Vector3.Lerp(
                    startPos,
                    endPos,
                    0.1f
                );
            }

            if (_renderer.color.a <= 0)
            {
                _gameObject.SetActive(false);
            }
        }
    }
}
