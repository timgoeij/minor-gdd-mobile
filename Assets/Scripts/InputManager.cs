using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IColorChangeObserver {

    private List<ColorChangeableObject> changeableObjects = new List<ColorChangeableObject>();

    // Use this for initialization
    void awake() {
        changeableObjects = new List<ColorChangeableObject>();
    }
	
	// Update is called once per frame
	void Update () {

        #if UNITY_ANDROID              
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                foreach(ColorChangeableObject changeableColorObject in changeableObjects)
                {
                    changeableColorObject.ChangeColor(false);
                }
            }
        #else

            if (Input.anyKeyDown)
            {
                foreach (ColorChangeableObject changeableColorObject in changeableObjects)
                {
                    changeableColorObject.ChangeColor();
                }
            }
        #endif
    }

    public void add(ColorChangeableObject changeableObject)
    {
        changeableObjects.Add(changeableObject);
    }

    public void remove(ColorChangeableObject changeableObject)
    {
        changeableObjects.Remove(changeableObject);
    }
}
