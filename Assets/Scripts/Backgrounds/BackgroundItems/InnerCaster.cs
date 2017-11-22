using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Backgrounds.BackgroundItems
{
    public class InnerCaster : MonoBehaviour
    {

        private Transform _transform;

        private SpriteRenderer _renderer;

        void Awake()
        {
            _transform = transform;
            _renderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {

        }

        public void SetColor(Color color)
        {
            color.a = 0.05f;
            _renderer.color = color;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _transform.Rotate(0, 0, -1f);

            Vector3 scale = _transform.localScale;

            if (_transform.lossyScale.x <= 20)
            {
                scale += new Vector3(1, 1, 0);
            }
            else if (_transform.localScale.x >= 25)
            {
                scale -= new Vector3(1f, 1f, 0);
            }

            _transform.lossyScale.Set(scale.x, scale.y, scale.z);
        }
    }

}

