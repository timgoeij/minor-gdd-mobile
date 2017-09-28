using UnityEngine;
public class SmallPark : LevelPattern
{
  public SmallPark() {
    LoadObstacle("Rotator");
    _timeBetweenObstacles = 0.9f;
    _obstacleChance = 0;
    _patternDuration = 2.1f;
    _maxObstaclesSkippedInRow = 0;
    _quietTime = 0f;
    _allowDescending = false;
  }
}