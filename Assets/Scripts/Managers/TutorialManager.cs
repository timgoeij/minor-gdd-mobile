using ColourRun.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ColourRun.Managers
{
    public class TutorialManager : MonoBehaviour
    {

        [SerializeField]
        private GameObject tutorialMessage;

        private bool IsFinalNotificationFired = false;

        private bool isTutorialActive = false;

        public bool IsTutorialActive
        {
            get { return isTutorialActive; }
            set { isTutorialActive = value; }
        }

        private bool isFirstColorActive = false;

        public bool IsFirstColorActive
        {
            get { return isFirstColorActive; }
            set { isFirstColorActive = value; }
        }

        private bool isFirstMultiplierActive = false;

        public bool IsFirstMultiplierActive
        {
            get { return isFirstMultiplierActive; }
            set { isFirstMultiplierActive = value; }
        }

        private bool isTryingSelf = false;

        public bool IsTryingSelf
        {
            get { return isTryingSelf; }
            set { isTryingSelf = value; }
        }

        public bool IsColorsEqualOfPlayerAndLaser
        {
            get;
            set;
        }

        public bool IsAtPosition
        {
            get;
            set;
        }

        private PlayerScript player;

        private NotificationManager notificationManager;

        // Use this for initialization
        void Awake()
        {
            player = FindObjectOfType<PlayerScript>();
            notificationManager = FindObjectOfType<NotificationManager>();
            tutorialMessage.SetActive(false);

            if (PlayerPrefs.HasKey("TutorialActive"))
            {
                isTutorialActive = PlayerPrefs.GetInt("TutorialActive") == 1;
            }
            else
            {
                PlayerPrefs.SetInt("TutorialActive", 1);
                isTutorialActive = true;
            }

            if (isTutorialActive)
            {
                isFirstColorActive = true;
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (isTutorialActive)
            {
                if(FindObjectOfType<ColourRun.Controller.GameController>().MenuOpen)
                    tutorialMessage.SetActive(false);
                else
                    tutorialMessage.SetActive(true);

                if (isTryingSelf)
                {
                    if(!IsFinalNotificationFired)
                    {
                        notificationManager.AddNotification("#@# GOOD LUCK!!! #@#", 2f);
                        PlayerPrefs.SetInt("TutorialActive", 0);
                        tutorialMessage.SetActive(false);
                        isTutorialActive = false;
                    }
                }

                if(isFirstColorActive)
                {
                    checkEqualColors(player.firstColorTarget);
                }
                else if(isFirstMultiplierActive)
                {
                    checkEqualColors(player.NearDeath);
                }

                if (isFirstColorActive && !FindObjectOfType<ColourRun.Controller.GameController>().MenuOpen && FindObjectOfType<GameController>().gameStarted)
                {
                    tutorialMessage.transform.Find("MessageText").GetComponent<Text>().text = "You will encounter different obstacles during your run. " +
                        "You can only touch obstacles the same colour as you. Tapping the screen will change the colour of these obstacles. Give it a try!";
                }
                else if (isFirstMultiplierActive && !FindObjectOfType<ColourRun.Controller.GameController>().MenuOpen && FindObjectOfType<GameController>().gameStarted)
                {

                    tutorialMessage.transform.Find("MessageText").GetComponent<Text>().text = "Good Job!!! " +
                        "Changing the colour of an obstacle at the last moment will result in a score multiplier! Sparks around your player will notify you when it's time to tap.";
                }
            }
            else
            {
                tutorialMessage.SetActive(false);
            }
        }

        private void checkEqualColors(GameObject target)
        {
            if (target == null)
                return;

            IsColorsEqualOfPlayerAndLaser = player.GetCurrentColor() == target.GetComponent<Laser>().GetCurrentColor();
        }

        public void PlayerIsAtPostion()
        { 
            IsAtPosition = true;
            Time.timeScale = 0;
        }

        public void PlayerIsNotInPosition()
        {
            IsAtPosition = false;
        }

        public void SwitchTutorialState()
        {
            if (isFirstMultiplierActive)
            {
                isFirstMultiplierActive = false;
                isTryingSelf = true;
            }
            else if (isFirstColorActive)
            {
                isFirstColorActive = false;
                isFirstMultiplierActive = true;
            }
        }

        public void OnNotifiedNextTutorialEvent(bool isMultiplierCompleted, InputManager manager)
        {
            if(isMultiplierCompleted)
            { 
                Time.timeScale = 1;
                //notificationManager.AddNotification("Out on your own!", 1.5f);

                manager.notifyNextTutorialEvent -= OnNotifiedNextTutorialEvent;
            }
            else
            {
                Time.timeScale = 1;
                notificationManager.AddNotification("Good job!", 1.5f);
            }
        }
    }
}
