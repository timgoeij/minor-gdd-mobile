using UnityEngine;

public class UpAndDownLaserPattern : LevelPattern
{
  public UpAndDownLaserPattern() {
    LoadObstacle("UpAndDownLaser");
    _timeBetweenObstacles = 1.2f;
    _obstacleChance = 2;
    _patternDuration = 7f;
    _maxObstaclesSkippedInRow = 1;
    _maxColorRepeat = 2;
  }

  public override void init() {
    if (_timeBetweenObstacles <= 0.8) {
      _timeBetweenObstacles -= 0.05f;
    }
  }
}