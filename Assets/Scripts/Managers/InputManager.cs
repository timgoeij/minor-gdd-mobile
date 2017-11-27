using ColourRun.Controller;
using ColourRun.Interfaces;
using ColourRun.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ColourRun.Managers
{
    public class InputManager : MonoBehaviour
    {
        private List<IInputTrigger> _listeners = new List<IInputTrigger>();

        public delegate void NotifyNextTutorialEvent(bool isMultiplierCompleted, InputManager input);
        public NotifyNextTutorialEvent notifyNextTutorialEvent;

        private TutorialManager tm;
        private bool isTouched = false;

        // Use this for initialization
        void Awake()
        {
            _listeners = new List<IInputTrigger>();
            tm = FindObjectOfType<TutorialManager>();

            notifyNextTutorialEvent -= tm.OnNotifiedNextTutorialEvent;
            notifyNextTutorialEvent += tm.OnNotifiedNextTutorialEvent;
        }

        // Update is called once per frame
        void LateUpdate()
        {

            GameController controller = FindObjectOfType<GameController>();

#if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {

                if (!controller.MenuOpen)
                {
                    if (tm.IsTutorialActive)
                    {
                        if (FindObjectOfType<GameController>().gameStarted)
                        {
                            if (tm.IsFirstColorActive && tm.IsAtPosition)
                            {
                                isTouched = true;
                            }
                            else if (tm.IsFirstMultiplierActive && tm.IsAtPosition)
                            {
                                isTouched = true;
                            }
                            else if (tm.IsTryingSelf || tm.IsFirstColorActive || tm.IsFirstMultiplierActive)
                            {
                                TriggerObjects(Input.GetTouch(0).position, Input.GetTouch(0).fingerId);
                            }
                        }
                        else
                        {
                            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                            {
                                FindObjectOfType<GameController>().gameStarted = true;
                                Timer.StartTimer();
                                FindObjectOfType<PlayerScript>().StartRunning();
                            }
                        }
                    }
                    else
                    {
                        TriggerObjects(Input.GetTouch(0).position, Input.GetTouch(0).fingerId);
                    }
                }
            }

            if (isTouched)
            {
                StartCoroutine(CheckColors(Input.GetTouch(0).position, Input.GetTouch(0).fingerId));
                isTouched = false;
            }

            if (Input.GetKey(KeyCode.Escape))
            {


                if (controller.gameStarted)
                {
                    controller.ActivatePauseMenu();
                }
                else
                {
                    controller.ExitGame();
                }
            }
#else
        if (Input.GetMouseButtonDown(0))
        {
            if( ! controller.MenuOpen)
            {
                if(tm.IsTutorialActive)
                {
                    if(FindObjectOfType<GameController>().gameStarted)
                    {
                        if (tm.IsFirstColorActive && tm.IsAtPosition)
                        {
                            isTouched = true;
                        }
                        else if (tm.IsFirstMultiplierActive && tm.IsAtPosition)
                        {
                            isTouched = true;
                        }
                        else if (tm.IsTryingSelf || tm.IsFirstColorActive || tm.IsFirstMultiplierActive)
                        {
                            TriggerObjects(Input.mousePosition);
                        }
                    }
                    else
                    {
                        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                        {
                            FindObjectOfType<GameController>().gameStarted = true;
                            Timer.StartTimer();
                            FindObjectOfType<PlayerScript>().StartRunning();
                        }
                    }
                    
                }
                else
                {
                    TriggerObjects(Input.mousePosition);
                }
            }
            
            if (isTouched)
            {
                StartCoroutine(CheckColors(Input.mousePosition));
                isTouched = false;
            }

        }
#endif
        }

        private void TriggerObjects(Vector3 inputPosition, int inputId = -1)
        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(inputId))
            {
                VisualizeInput(inputPosition);

                foreach (IInputTrigger listener in _listeners)
                {
                    listener.Trigger();
                }
            }
        }

        private void VisualizeInput(Vector3 position)
        {
            position.z = 20;
            GameObject touchCircle = PoolManager.GetItem("TouchCircle");

            if (touchCircle != null)
            {
                Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;

                touchCircle.transform.position = pos;
                touchCircle.SetActive(true);
            }
        }

        IEnumerator CheckColors(Vector3 position, int id = -1)
        {

            if (!tm.IsColorsEqualOfPlayerAndLaser)
                TriggerObjects(position, id);

            yield return new WaitForSecondsRealtime(.01f);

            if (tm.IsColorsEqualOfPlayerAndLaser)
            {
                if (notifyNextTutorialEvent != null)
                {
                    notifyNextTutorialEvent(tm.IsFirstMultiplierActive, this);
                }
            }
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

}

