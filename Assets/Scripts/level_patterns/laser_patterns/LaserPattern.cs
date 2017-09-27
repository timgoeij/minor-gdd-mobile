using UnityEngine;

public class LaserPattern : LevelPattern
{
  public LaserPattern() {
    LoadObstacle("Laser");
    _timeBetweenObstacles = 1.2f;
    _obstacleChance = 2;
    _patternDuration = 7f;
    _maxObstaclesSkippedInRow = 1;
    _maxColorRepeat = 2;
    _descendingChance = 3;
  }

  public override void init() {
    if (_timeBetweenObstacles <= 0.6) {
      _timeBetweenObstacles -= 0.05f;
    }
  }
}