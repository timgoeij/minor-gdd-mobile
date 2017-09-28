using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownChangeableLaserBox : LevelPattern
{

    public UpAndDownChangeableLaserBox()
    {
        LoadObstacle("UpAndDownLaser");
        LoadObstacle("RotationCube");
        _timeBetweenObstacles = 1.5f;
        _obstacleChance = 1;
        _patternDuration = 10f;
        _maxObstaclesSkippedInRow = 1;
        _maxColorRepeat = 1;
        _quietTime = .5f;
    }

    public override void init()
    {
        foreach (GameObject obstacle in _availableObstables)
        {
            if (obstacle.GetComponent<RotationCube>() != null)
                obstacle.GetComponent<RotationCube>().IsChangingColor = true;
            else
                obstacle.GetComponent<UpAndDownLaser>().IsChangingColor = true;
        }


        base.init();
    }
}