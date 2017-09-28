using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circles : LevelPattern {

	public Circles()
    {
        LoadObstacle("RollingCircle");
        _timeBetweenObstacles = 2f;
        _obstacleChance = 1;
        _patternDuration = 16f;
        _maxObstaclesSkippedInRow = 1;
        _maxColorRepeat = 1;
        _quietTime = 2f;
        _descendingChance = 100;
    }

    public override void init() {
        MonoBehaviour.FindObjectOfType<NotificationManager>().AddNotification("!M0rE !1NC0M1nG", 2.5f);
    }
}
