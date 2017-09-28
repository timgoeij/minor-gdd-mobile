using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTriangles : LevelPattern {

	public FirstTriangles()
    {
        LoadObstacle("TriangleShark");
        _timeBetweenObstacles = 2f;
        _obstacleChance = 0;
        _patternDuration = 3.5f;
        _maxObstaclesSkippedInRow = 0;
        _maxColorRepeat = 1;
        _quietTime = 2f;
        _allowDescending = true;
        _descendingChance = 0;
    }

    public override void init()
    {
        foreach (GameObject circle in _availableObstables)
        {
            circle.GetComponent<TriangleShark>().MaxSpeed = 0.1f;
        }

        base.init();
    }
}
