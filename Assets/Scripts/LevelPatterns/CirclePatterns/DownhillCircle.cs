using ColourRun.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownhillCircles : LevelPattern {

	public DownhillCircles()
    {
        LoadObstacle("RollingCircle");
        _timeBetweenObstacles = 0.9f;
        _obstacleChance = 0;
        _patternDuration = 21f;
        _maxObstaclesSkippedInRow = 0;
        _maxColorRepeat = 3;
        _quietTime = 2f;
        _descendingChance = 0;
        _allowDescending = true;
        
    }

    public override void init() {
        MonoBehaviour.FindObjectOfType<NotificationManager>().AddNotification(">>> It's all downhill from here <<<", 2.5f);
    }
}
