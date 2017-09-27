using UnityEngine;

public class UpDownUpDownFast : LevelPattern
{
  public UpDownUpDownFast() {
    LoadObstacle("UpAndDownLaser");
    _timeBetweenObstacles = 0.6f;
    _obstacleChance = 0;
    _patternDuration = 4f;
    _maxObstaclesSkippedInRow = 0;
    _maxColorRepeat = 1;
    _quietTime = 1.2f;
  }

  public override void init() {
    if (_timeBetweenObstacles <= 0.8) {
      _timeBetweenObstacles -= 0.05f;
    }
  }
}