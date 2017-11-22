using ColourRun.Cameras;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Backgrounds.BackgroundItems
{
    public class RayItem : MonoBehaviour
    {
        void Start()
        {

        }

        void FixedUpdate()
        {

        }

        public void Move()
        {
            transform.Rotate(0, 0, 1);
        }

        public void Change()
        {

        }

        private void DestroyCheck()
        {
            if (CameraScreen.ObjectIsBehindCamera(transform) || CameraScreen.ObjectIsUnderCamera(transform))
            {
                Destroy(gameObject);
            }
        }
    }

}

