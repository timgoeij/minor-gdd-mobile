using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickUpAndDownBox : LevelPattern
{

    public QuickUpAndDownBox()
    {
        LoadObstacle("RotationCube");
        _timeBetweenObstacles = 0.6f;
        _obstacleChance = 0;
        _patternDuration = 6.5f;
        _maxObstaclesSkippedInRow = 1;
        _maxColorRepeat = 1;
        _quietTime = 0;
    }
}