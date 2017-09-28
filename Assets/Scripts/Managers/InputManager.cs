using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

    [SerializeField]
    GameObject _touchCircle;

    private List<IInputTrigger> _listeners = new List<IInputTrigger>();

    // Use this for initialization
    void awake() {
        _listeners = new List<IInputTrigger>();
    }
	
	// Update is called once per frame
	void LateUpdate () {

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            if(!FindObjectOfType<GameController>().MenuOpen)
            {
                if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    VisualizeInput(Input.GetTouch(0).position);

                    foreach (IInputTrigger listener in _listeners)
                    {
                        listener.Trigger();
                    }
                }
            } 
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            GameController controller = FindObjectOfType<GameController>();

            if(controller.gameStarted)
            {
                FindObjectOfType<GameController>().ActivatePauseMenu();
            }
            else
            {
                FindObjectOfType<GameController>().ExitGame();
            }
        }
#else
        if (Input.GetMouseButtonDown(0))
        {
            if (!FindObjectOfType<GameController>().MenuOpen)
            {
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    VisualizeInput(Input.mousePosition);

                    foreach (IInputTrigger listener in _listeners)
                    {
                        listener.Trigger();
                    }
                }
            }
        }
#endif
    }
    
    private void VisualizeInput(Vector3 position) {
        position.z = 20;
        GameObject touchCircle = Instantiate(_touchCircle);

        Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;

        touchCircle.transform.position = pos;
    }

    public void add(IInputTrigger listener)
    {
        _listeners.Add(listener);
    }

    public void remove(IInputTrigger listener)
    {
        _listeners.Remove(listener);
    }
}
