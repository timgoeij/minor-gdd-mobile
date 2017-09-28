using UnityEngine;
public class Windmillpark : LevelPattern
{
  public Windmillpark() {
    LoadObstacle("Rotator");
    _timeBetweenObstacles = 1.2f;
    _obstacleChance = 0;
    _patternDuration = 6.5f;
    _maxObstaclesSkippedInRow = 0;
    _quietTime = 0.6f;
    _allowDescending = false;
  }
}