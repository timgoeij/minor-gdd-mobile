using ColourRun.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColourRun.Backgrounds
{
    public class LightPattern : IBackgroundPattern
    {
        private float _timeBetweenUpdates = 0.213f;
        private float _timeSinceLastUpdate = 0;
        private GameObject _caster;

        public LightPattern()
        {

        }

        public void Init()
        {
            if (_caster == null)
            {
                _caster = Resources.Load("LightCaster") as GameObject;
            }

            _caster = MonoBehaviour.Instantiate(_caster);

            _timeSinceLastUpdate = 0;
        }

        public void Stop()
        {
            MonoBehaviour.Destroy(_caster);
            _caster = null;
        }

        public void Update()
        {
            if (_timeSinceLastUpdate < _timeBetweenUpdates)
            {
                _timeSinceLastUpdate += Time.deltaTime;
                return;
            }


        }
    }
}

