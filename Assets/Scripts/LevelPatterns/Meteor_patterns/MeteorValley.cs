using ColourRun.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorValley : LevelPattern
{

    public MeteorValley()
    {
        LoadObstacle("MeteorSpawner");
        LoadObstacle("MeteorSpawner");
        LoadObstacle("MeteorSpawner");
        LoadObstacle("MeteorSpawner");
        _timeBetweenObstacles = -1f;
        _obstacleChance = 0;
        _patternDuration = 60f;
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
            _timeBetweenObstacles = 13;

        return _timeBetweenObstacles;
    }

    public override void init()
    {
        MonoBehaviour.FindObjectOfType<NotificationManager>().AddNotification("Welcome to Meteor Valley, its alway hard to survive", 1.5f);
        for (int i = 0; i < _availableObstables.Count; i++)
        {
            GameObject meteorSpawner = _availableObstables[i];
            meteorSpawner.GetComponent<MeteorSpawner>().SpawnMode = (MeteorSpawner.MeteorSpawnMode) i;
            meteorSpawner.GetComponent<MeteorSpawner>().MaxDuration = _timeBetweenObstacles - 4;
        }

        _timeBetweenObstacles = -1;

        base.init();
    }
}
