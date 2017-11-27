using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using ColourRun.Leaderboard;

namespace ColourRun.Managers
{
    public class SocialManager : MonoBehaviour
    {

        public bool IsSocialActive
        {
            get { return Social.localUser.authenticated; }
        }

        // Use this for initialization
        void Awake()
        {

#if UNITY_ANDROID
            {
                if (IsSocialActive)
                    return;

                PlayGamesPlatform.Activate();
                PlayGamesPlatform.DebugLogEnabled = true;
            }
#endif
        }

        public void SignIn(long score = 0)
        {
#if UNITY_ANDROID

            if (FindObjectOfType<InternetTest>().InternetAvailable)
            {
                Social.localUser.Authenticate((bool success) =>
                {
                    if (success)
                    {
                        if (score != 0)
                            PostScore(score);

                        Debug.Log("sign in succesfull");
                    }
                    else
                    {
                        FindObjectOfType<ToastMessenger>().ShowToast("There's something wrong with your account details");
                    }
                });
            }
            else
            {
                FindObjectOfType<ToastMessenger>().ShowToast("You need a valid internet connection to sign in");
            }

#endif
            return;
        }

        public long GetPlayerScoreOutOfLeaderboard()
        {
            long score = 0;
#if UNITY_ANDROID
            {
                PlayGamesPlatform.Instance.LoadScores(GPGSLeaderboards.leaderboard_worldwide, LeaderboardStart.PlayerCentered,
                    10,LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime, (data) =>
                    {
                        if(data.Valid)
                        {
                            Debug.Log("get score out of leaderboard");
                            score = data.PlayerScore.value;
                        }
                    });
            }
#endif

            return score;
        }

        public void PostScore(long score)
        {
#if UNITY_ANDROID
            {
                Debug.Log("Gonna Post A Score");

                ((PlayGamesPlatform)Social.Active).ReportScore(score, GPGSLeaderboards.leaderboard_worldwide, (bool success) =>
                {
                    if (success)
                    {
                        Debug.Log("success post a score");
                        //PlayerPrefs.SetInt("PersonalBest", (int)FindObjectOfType<SocialManager>().GetPlayerScoreOutOfLeaderboard());
                    }
                    else
                    {
                        FindObjectOfType<ToastMessenger>().ShowToast("There's something wrong with posting your score in the leaderboard, Try again later");
                    }
                });
            }
#endif
        }


        public void ShowLeaderBoard()
        {
#if UNITY_ANDROID
            {
                if (IsSocialActive)
                    PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSLeaderboards.leaderboard_worldwide);
                else
                    FindObjectOfType<ToastMessenger>().ShowToast("You must signed in to google to see the leaderboard");
            }
#endif

        }
    }

}
