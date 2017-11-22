using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ColourRun.Managers
{
    public class ScoreNotifierManager : MonoBehaviour
    {

        private int _defaultFontSize;

        void Start()
        {
            _defaultFontSize = GetComponent<Text>().fontSize;
        }

        void FixedUpdate()
        {
            Color c = GetComponent<Text>().color;

            if (c.a > 0)
            {
                c.a -= 0.01f;
            }
            else
            {
                GetComponent<Text>().text = "";
                c.a = 0;
            }

            if (GetComponent<Text>().fontSize > _defaultFontSize)
            {
                GetComponent<Text>().fontSize--;
            }

            GetComponent<Text>().color = c;
        }

        public void SetText(int points, int multiplier, int totalPoints)
        {
            string text = "";

            if (multiplier == 1)
            {
                text = string.Format("+{0}", points);
            }
            else
            {
                text = string.Format("{0} X {1} = {2}", points, multiplier, totalPoints);
            }

            GetComponent<Text>().text = text;

            int fontSize = GetComponent<Text>().fontSize;
            fontSize += 10 * multiplier;

            if (fontSize > 150)
            {
                fontSize = 150;
            }

            ResetAlpha();
            GetComponent<Text>().fontSize = fontSize;
            RotateText();
        }

        public void SetBonusText(int points, string message)
        {
            GetComponent<Text>().text = string.Format("{0} \n\n {1} \n +{2}", GetComponent<Text>().text, message, points);

            GetComponent<Text>().fontSize = 100;
            RotateText();
            ResetAlpha();
        }

        private void RotateText()
        {
            GetComponent<RectTransform>().transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-5, 5));
        }

        private void ResetAlpha()
        {
            Color c = GetComponent<Text>().color;
            c.a = 1;
            GetComponent<Text>().color = c;
        }
    }

}
