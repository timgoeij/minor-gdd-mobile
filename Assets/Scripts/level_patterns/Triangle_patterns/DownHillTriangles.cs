using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownHillTriangles : LevelPattern
{

    public DownHillTriangles()
    {
        LoadObstacle("TriangleShark");
        _timeBetweenObstacles = 1.5f;
        _obstacleChance = 0;
        _patternDuration = 21f;
        _maxObstaclesSkippedInRow = 0;
        _maxColorRepeat = 3;
        _quietTime = 2f;
        _descendingChance = 0;
        _allowDescending = true;

    }

    public override void init()
    {
        MonoBehaviour.FindObjectOfType<NotificationManager>().AddNotification(">>> Lets go diving in a pool of !!SHARKS!! <<<", 2.5f);
    }
}
