using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Managers
{
    public static class ColorManager
    {

        private static List<Color> basicColors = new List<Color>();

        public static List<Color> colors()
        {
            if (basicColors.Count == 0)
            {
                resetColors();
            }

            return basicColors;
        }

        public static void AddColor(Color color)
        {
            basicColors.Add(color);
        }

        public static void resetColors()
        {
            basicColors.Clear();
            basicColors.Add(Color.red);
            basicColors.Add(Color.cyan);
            basicColors.Add(Color.yellow);

        }

        public static int FindNextIndexOfColor(Color c)
        {
            return FindNextIndex(FindIndex(c));
        }

        public static int FindNextIndex(int index)
        {
            index++;

            if (index >= colors().Count)
            {
                index = 0;
            }

            return index;
        }

        public static int FindIndex(Color c)
        {
            return ColorManager.colors().IndexOf(c);
        }

        public static int GetRandomColorIndex()
        {
            return Random.Range(0, colors().Count);
        }
    }
}

