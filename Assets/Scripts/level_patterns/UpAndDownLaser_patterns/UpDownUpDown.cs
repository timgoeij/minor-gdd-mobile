using UnityEngine;

public class UpDownUpDown : LevelPattern
{
  public UpDownUpDown() {
    LoadObstacle("UpAndDownLaser");
    _timeBetweenObstacles = 0.9f;
    _obstacleChance = 0;
    _patternDuration = 9f;
    _maxObstaclesSkippedInRow = 0;
    _maxColorRepeat = 1;
    _descendingChance = 5;
  }

  public override void init() {
    if (_timeBetweenObstacles <= 0.8) {
      _timeBetweenObstacles -= 0.05f;
    }
  }
}