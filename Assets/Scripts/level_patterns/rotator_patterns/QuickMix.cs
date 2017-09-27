using UnityEngine;
public class QuickMix : LevelPattern
{
  public QuickMix() {
    LoadObstacle("Rotator");
    LoadObstacle("Laser");
    
    _timeBetweenObstacles = 0.9f;
    _obstacleChance = 2;
    _patternDuration = 6.5f;
    _maxObstaclesSkippedInRow = 2;
    _quietTime = 0f;
    _allowDescending = false;
  }
}