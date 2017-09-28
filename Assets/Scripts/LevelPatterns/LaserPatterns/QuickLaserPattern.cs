using UnityEngine;

public class QuickLaserPattern : LevelPattern
{
  public QuickLaserPattern() {
    LoadObstacle("Laser");
    _timeBetweenObstacles = 1.2f;
    _obstacleChance = 0;
    _patternDuration = 6f;
    _maxObstaclesSkippedInRow = 0;
  }
}