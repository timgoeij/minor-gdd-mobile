using ColourRun.Cameras;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Floors
{
    public class FloorScript : MonoBehaviour
    {
        void Update()
        {
            if (tag == "Wall" && CameraScreen.ObjectIsBehindCamera(transform))
            {
                Destroy(gameObject);
            }
        }
    }

}
