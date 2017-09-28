using UnityEngine;
public class NiceMix : LevelPattern
{
  public NiceMix() {
    LoadObstacle("Rotator");
    LoadObstacle("Laser");
    
    _timeBetweenObstacles = 1.2f;
    _obstacleChance = 1;
    _patternDuration = 13f;
    _maxObstaclesSkippedInRow = 1;
    _quietTime = 0f;
    _allowDescending = false;
  }

  public override void init() {
    _timeBetweenObstacles = Random.Range(0.9f, 1.2f);
  }
}