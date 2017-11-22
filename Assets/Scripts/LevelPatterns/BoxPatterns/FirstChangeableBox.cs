using ColourRun.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstChangeableBox : LevelPattern {

    public FirstChangeableBox()
    {
        LoadObstacle("RotationCube");
        _timeBetweenObstacles = 1.5f;
        _obstacleChance = 0;
        _patternDuration = 3.5f;
        _maxObstaclesSkippedInRow = 0;
        _maxColorRepeat = 1;
        _quietTime = 1.5f;
    }

    public override void init()
    {
        MonoBehaviour.FindObjectOfType<DifficultyManager>().AllowBoxColorChanges();
    }
}
