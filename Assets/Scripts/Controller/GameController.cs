using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace ColourRun.Controller
{
    public class GameController : MonoBehaviour
    {

        [SerializeField]
        private GameObject mainMenu;

        [SerializeField]
        private GameObject gameOver;

        [SerializeField]
        private bool _gameStarted = false;

        [SerializeField]
        private GameObject pauseMenu;

        [SerializeField]
        private GameObject colorOrder;

        [SerializeField]
        bool menuOpen = true;

        [SerializeField]
        private bool _notActive;

        [SerializeField]
        private GameObject _socialmedia;

        public bool gameStarted
        {
            get
            {
                return _gameStarted;
            }

            private set
            {
                _gameStarted = value;
            }
        }

        public bool MenuOpen
        {
            get { return menuOpen; }
        }

        private void Start()
        {
            gameOver.SetActive(false);
            pauseMenu.SetActive(false);
            colorOrder.SetActive(false);
            menuOpen = true;

            if (CheckReloadedGame())
            {
                startGame();
            }
            else
            {
                if (!FindObjectOfType<SocialManager>().IsSocialActive)
                    FindObjectOfType<SocialManager>().SignIn();
            }

            if (!PlayerPrefs.HasKey("PersonalBest"))
                PlayerPrefs.SetInt("PersonalBest", 0);
        }

        private Boolean CheckReloadedGame()
        {
            GameController[] controllers = FindObjectsOfType<GameController>();

            if (controllers.Length > 1)
            {
                for (int i = 0; i < controllers.Length; i++)
                {
                    if (controllers[i].CompareTag("OldGameController"))
                    {
                        Destroy(controllers[i].gameObject);
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void startGame()
        {
            menuOpen = false;
            gameStarted = true;
            mainMenu.SetActive(false);
            colorOrder.SetActive(true);
            Timer.StartTimer();
            FindObjectOfType<PlayerScript>().StartRunning();
            if(FindObjectOfType<Managers.TutorialManager>().IsTutorialActive)
                FindObjectOfType<NotificationManager>().AddNotification("!@# Incoming Tutorial #@!", 2.5f);
            else
                FindObjectOfType<NotificationManager>().AddNotification("!@# Tap = Change Level Colours #@!", 2.5f);
            //GameObject.FindGameObjectWithTag("ScoreNotifier").SetActive(false);
            _socialmedia.SetActive(false);
        }

        public void EndGame()
        {
            gameStarted = false;
            menuOpen = true;

            gameOver.transform.Find("LeaderBoardButton").GetComponent<Image>().color = FindObjectOfType<SocialManager>().IsSocialActive ?
                gameOver.transform.Find("LeaderBoardButton").GetComponent<Image>().color : Color.gray;

            gameOver.SetActive(true);

            colorOrder.SetActive(false);
            _socialmedia.SetActive(true);

            if (FindObjectOfType<ScoreManager>().GetScore() > PlayerPrefs.GetInt("PersonalBest"))
            {
                PlayerPrefs.SetInt("PersonalBest", FindObjectOfType<ScoreManager>().GetScore());
            }

            if (FindObjectOfType<SocialManager>().IsSocialActive)
            {
                Debug.Log("Active and post a score");
                FindObjectOfType<SocialManager>().PostScore(PlayerPrefs.GetInt("PersonalBest"));
            }
            else
            {
                Debug.Log("Sign in and post a score");
                FindObjectOfType<SocialManager>().SignIn(PlayerPrefs.GetInt("PersonalBest"));
            }


            gameOver.transform.Find("ScoreText").GetComponent<Text>().text = FindObjectOfType<ScoreManager>().GetScore().ToString();
            gameOver.transform.Find("PersonalScoreText").GetComponent<Text>().text = PlayerPrefs.GetInt("PersonalBest").ToString();
            transform.tag = "OldGameController";
        }

        public void ActivatePauseMenu()
        {
            menuOpen = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            FindObjectOfType<BackgroundMusicManager>().PauseMusic();
        }

        public void ResumeGame()
        {
            menuOpen = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            FindObjectOfType<BackgroundMusicManager>().ResumeMusic();

        }

        public void ReturnToMainMenu()
        {
            menuOpen = true;
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void ReloadGame()
        {
            Time.timeScale = 1;
            DontDestroyOnLoad(gameObject);
            ColorManager.resetColors();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ToggleAudio()
        {
            FindObjectOfType<MusicScript>().ToggleMusic();
            FindObjectOfType<SoundEffectManager>().ToggleEffects();
        }

        public void GoToFacebook()
        {
            StartCoroutine(OpenFacebookPage());
        }

        public void GoToTwitter()
        {
            StartCoroutine(OpenTwitterPage());
        }

        public IEnumerator OpenFacebookPage()
        {
            Application.OpenURL("fb://page/116125165730364");
            yield return new WaitForSeconds(0.5f);

            if (_notActive)
            {
                _notActive = false;
            }
            else
            {
                Application.OpenURL("https://www.facebook.com/ColourRunGame");
            }
        }

        public IEnumerator OpenTwitterPage()
        {
            Application.OpenURL("twitter://user?user_id=913009876145770496");
            yield return new WaitForSeconds(0.5f);

            if (_notActive)
            {
                _notActive = false;
            }
            else
            {
                Application.OpenURL("https://twitter.com/WeLikeCows");
            }
        }

        void OnApplicationPause()
        {
            _notActive = true;
            if (!menuOpen)
            {
                ActivatePauseMenu();
            }

        }

        void OnApplicationFocus(bool hasFocus)
        {
            _notActive = !hasFocus;

            if (_notActive && !menuOpen)
            {
                ActivatePauseMenu();
            }
        }
    }

}

