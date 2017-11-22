using UnityEngine;

public class MixedUpDown : LevelPattern
{
  public MixedUpDown() {
    LoadObstacle("Laser");
    LoadObstacle("UpAndDownLaser");
    _timeBetweenObstacles = 1.2f;
    _obstacleChance = 1;
    _patternDuration = 7f;
    _maxObstaclesSkippedInRow = 1;
    _maxColorRepeat = 2;
  }

  public override void init() {
    if (_timeBetweenObstacles <= 0.6) {
      _timeBetweenObstacles -= 0.05f;
    }
  }
}