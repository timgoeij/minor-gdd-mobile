using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Managers
{
    public class TutorialManager : MonoBehaviour
    {

        [SerializeField]
        private TutorialTouch tutorialTouch;

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
            set { isTryingSelf = value;  }
        }

        public bool IsColorsEqualOfPlayerAndLaser
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
            tutorialTouch.gameObject.SetActive(false);

            if (PlayerPrefs.HasKey("TutorialActive"))
            {
                isTutorialActive = true; //PlayerPrefs.GetInt("TutorialActive") == 1;
            }
            else
            {
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
            if(isTutorialActive)
            {
                if (isTryingSelf)
                {
                    if (FindObjectOfType<LevelManager>().playerInPattern.GetType() != typeof(StartingPattern))
                    {
                        isTryingSelf = false;
                        isTutorialActive = false;
                        notificationManager.AddNotification("You finished the tutorial!!", 1.5f);
                        PlayerPrefs.SetInt("TutorialActive", 0);
                    }
                }

                if((IsFirstColorActive || isFirstMultiplierActive) && Time.timeScale == 0)
                {
                    tutorialTouch.gameObject.SetActive(true);
                    tutorialTouch.ActivateAnimation();
                }
                else
                {
                    
                    tutorialTouch.DeActivateAnimation();
                    tutorialTouch.gameObject.SetActive(false);
                    
                }

                if(isFirstMultiplierActive)
                {
                    if(player.NearDeath != null)
                    {
                        IsColorsEqualOfPlayerAndLaser = player.GetCurrentColor() == player.NearDeath.GetComponent<Laser>().GetCurrentColor();
                    }
                }
            }
        }

        public void OnNotifiedNextTutorialEvent(bool isMultiplierCompleted, InputManager manager)
        {
            if(isMultiplierCompleted)
            { 
                Time.timeScale = 1;
                notificationManager.AddNotification("Great! Multiplier is up!! :D", 1.5f);

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
