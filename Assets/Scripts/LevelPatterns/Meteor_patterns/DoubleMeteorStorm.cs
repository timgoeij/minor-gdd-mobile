using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleMeteorStorm : LevelPattern
{

    public DoubleMeteorStorm()
    {
        LoadObstacle("MeteorSpawner");
        _timeBetweenObstacles = -1f;
        _obstacleChance = 0;
        _patternDuration = 30f;
        _maxObstaclesSkippedInRow = 0;
        _maxColorRepeat = 1;
        _quietTime = 2f;
        _allowDescending = false;
    }

    public override float TimeBetweenObstacles()
    {
        if (_timeBetweenObstacles == -1)
            _timeBetweenObstacles = 0;
        else if (_timeBetweenObstacles == 0)
            _timeBetweenObstacles = 11;

        return _timeBetweenObstacles;
    }

    public override void init()
    {
        foreach (GameObject meteorSpawner in _availableObstables)
        {
            meteorSpawner.GetComponent<MeteorSpawner>().SpawnMode = MeteorSpawner.MeteorSpawnMode.DoubleMeteors;
        }

        _timeBetweenObstacles = -1;

        base.init();
    }
}