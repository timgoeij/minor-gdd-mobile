using UnityEngine;

public class QuickButRandomPattern : LevelPattern
{
  public QuickButRandomPattern() {
    LoadObstacle("Laser");
    _timeBetweenObstacles = 0.9f;
    _obstacleChance = 10;
    _patternDuration = 5f;
    _maxObstaclesSkippedInRow = 2;
  }
}