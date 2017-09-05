using System.Collections.Generic;
using UnityEngine;

public static class ColorManager {

    private static List<Color> basicColors = new List<Color>();

    public static List<Color> colors()
    {
        if( basicColors.Count == 0)
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
        basicColors.Add(Color.green);
        basicColors.Add(Color.blue);

    }
}