using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCircle : LevelPattern {

	public FirstCircle()
    {
        LoadObstacle("RollingCircle");
        _timeBetweenObstacles = 1.5f;
        _obstacleChance = 0;
        _patternDuration = 3.5f;
        _maxObstaclesSkippedInRow = 0;
        _maxColorRepeat = 1;
        _quietTime = 1.5f;
        _allowDescending = false;
    }

    public override void init()
    {
        foreach(GameObject circle in _availableObstables)
        {
            circle.GetComponent<RollingCircle>().MaxSpeed = 0.1f;
        }

        base.init();
    }
}
