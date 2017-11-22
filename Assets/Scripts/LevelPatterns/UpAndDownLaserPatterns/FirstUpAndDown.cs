﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstUpAndDown : LevelPattern
{
    public FirstUpAndDown()
    {
        LoadObstacle("UpAndDownLaser");
        _timeBetweenObstacles = 1.5f;
        _obstacleChance = 0;
        _patternDuration = 3.5f;
        _maxObstaclesSkippedInRow = 0;
        _maxColorRepeat = 1;
        _quietTime = 1.5f;
    }
}
    