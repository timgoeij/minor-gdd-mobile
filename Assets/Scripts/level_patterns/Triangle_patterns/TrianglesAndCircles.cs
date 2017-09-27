using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglesAndCircles : LevelPattern
{

    public TrianglesAndCircles()
    {
        LoadObstacle("TriangleShark");
        LoadObstacle("RollingCircle");
        _timeBetweenObstacles = 1.5f;
        _obstacleChance = 0;
        _patternDuration = 21f;
        _maxObstaclesSkippedInRow = 0;
        _maxColorRepeat = 3;
        _quietTime = 2f;
        _descendingChance = 0;
        _allowDescending = true;

    }
}
