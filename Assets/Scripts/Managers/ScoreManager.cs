using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Managers
{
    public class ScoreManager : MonoBehaviour
    {


        [SerializeField]
        private GameObject _scoreText;

        private int _totalScore = 0;
        private int _onScreenScore = 0;
        private float _timeBetweenScoreUpdates = 0;
        private float _timeSinceLastScoreUpdate = 0;

        private int _scoreSizeOffset = 0;

        private float _lastTimeSincePointsAdded = 0;
        private int _multiplier = 1;

        public int multiplier
        {
            get
            {
                return _multiplier;
            }

            set
            {
                _multiplier = value;
            }
        }

        // Use this for initialization
        void Start()
        {
            _timeBetweenScoreUpdates = GetTimeBetweenScoreUpdates();
        }

        private float GetTimeBetweenScoreUpdates()
        {
            return UnityEngine.Random.Range(0f, 0.03f);
        }

        // Update is called once per frame
        void Update()
        {
            if (FindObjectOfType<ColourRun.Controller.GameController>().gameStarted && !_scoreText.activeInHierarchy)
            {
                _scoreText.SetActive(true);
            }

            if (_scoreSizeOffset < 50 && FindObjectOfType<ScoreManager>().GetScore() % 100 == 0)
            {
                _scoreSizeOffset += 5;
            }

            UpdateScore();
        }

        public void AddPoints(int points)
        {
            int totalPoints = points * _multiplier;

            FindObjectOfType<ScoreNotifierManager>().SetText(points, _multiplier, totalPoints);

            AddBonusPoints();

            _totalScore += totalPoints;
            _lastTimeSincePointsAdded = Time.time;


        }

        public void AddBonusPoints()
        {
            AddQuickieBonus();
        }

        public void AddQuickieBonus()
        {
            if ((Time.time - _lastTimeSincePointsAdded) <= 0.39f)
            {
                int pointsToAdd = 15;
                string message = "Too fast for love...";

                _totalScore += pointsToAdd;
                FindObjectOfType<ScoreNotifierManager>().SetBonusText(pointsToAdd, message);
            }
            else if ((Time.time - _lastTimeSincePointsAdded) <= 0.69f)
            {
                int pointsToAdd = 10;
                string message = "Quickie!";

                _totalScore += pointsToAdd;
                FindObjectOfType<ScoreNotifierManager>().SetBonusText(pointsToAdd, message);
            }
            else if (Time.time - _lastTimeSincePointsAdded < 0.99f)
            {
                int pointsToAdd = 5;

                _totalScore += pointsToAdd;
                string message = "In a hurry!";

                FindObjectOfType<ScoreNotifierManager>().SetBonusText(pointsToAdd, message);
            }
        }

        public void AddBonusPoints(int points, string message)
        {

            FindObjectOfType<ScoreNotifierManager>().SetBonusText(points, message);
            _totalScore += points;
        }

        void UpdateScore()
        {
            if (_timeSinceLastScoreUpdate < _timeBetweenScoreUpdates)
            {
                _timeSinceLastScoreUpdate += Time.unscaledDeltaTime;
                return;
            }

            ShowScore();
        }

        private void ShowScore()
        {
            if (_totalScore > _onScreenScore)
            {
                if (((_totalScore - _onScreenScore) / 10) > 2)
                {
                    _onScreenScore += (int)Math.Round((double)((_totalScore - _onScreenScore) / 10));
                }
                else
                {
                    _onScreenScore++;
                }


                FindObjectOfType<SoundEffectManager>().PlayPoints();
                _scoreText.GetComponent<UnityEngine.UI.Text>().fontSize = UnityEngine.Random.Range(75, 125) + _scoreSizeOffset;
                _scoreText.GetComponent<UnityEngine.UI.Text>().text = _onScreenScore.ToString();

                _scoreText.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-10, 10));
            }
            else
            {
                FindObjectOfType<SoundEffectManager>().StopPoints();
            }

            _timeSinceLastScoreUpdate = 0;
            _timeBetweenScoreUpdates = GetTimeBetweenScoreUpdates();
        }

        public int GetScore()
        {
            return _totalScore;
        }
    }

}

