using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownLaserBox : LevelPattern
{

    public UpAndDownLaserBox()
    {
        LoadObstacle("UpAndDownLaser");
        LoadObstacle("RotationCube");
        _timeBetweenObstacles = 1.5f;
        _obstacleChance = 1;
        _patternDuration = 10f;
        _maxObstaclesSkippedInRow = 1;
        _maxColorRepeat = 1;
        _quietTime = .5f;
        _descendingChance = 2;
    }
}