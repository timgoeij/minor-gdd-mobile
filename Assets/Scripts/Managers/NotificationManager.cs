using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ColourRun.Managers
{
    public class Notification
    {
        public float time = 2;
        public string message = "";
    }

    public class NotificationManager : MonoBehaviour
    {
        private List<Notification> _notications = new List<Notification>();

        private Notification _currentNotification = null;
        private float _notificationTime;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_notications.Count > 0 && _currentNotification == null)
            {
                Notification notification = _notications[0];
                _currentNotification = notification;
                _notications.Remove(notification);
                FindObjectOfType<SoundEffectManager>().PlayNotification();
            }

            if (_currentNotification != null)
            {
                Text t = GetComponent<Text>();

                if (_notificationTime <= _currentNotification.time)
                {

                    if (Random.Range(0, 5) == 0)
                    {
                        t.GetComponent<RectTransform>().transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-3, 3));
                    }

                    if (Random.Range(0, 5) == 0)
                    {
                        t.fontSize = Random.Range(85, 95);
                    }

                    if (_currentNotification.message != t.text)
                    {
                        t.text = _currentNotification.message;
                    }

                    _notificationTime += Time.deltaTime;
                }
                if (_notificationTime > _currentNotification.time)
                {
                    t.text = "";
                    _currentNotification = null;
                    _notificationTime = 0;
                    FindObjectOfType<SoundEffectManager>().StopNotification();
                }
            }
        }

        public void AddNotification(string message, float time)
        {
            _notications.Add(new Notification { message = message, time = time });
        }
    }

}
