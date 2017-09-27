using UnityEngine;

public class NewColorPattern : LevelPattern
{
  private int _timesColorReturned = 0;

  public NewColorPattern() {
    LoadObstacle("Laser");
    _timeBetweenObstacles = 1.8f;
    _obstacleChance = 0;
    _patternDuration = 10f;
    _maxObstaclesSkippedInRow = 0;
    _quietTime = 2f;
  }

  public override int GetColor() {
    if (_timesColorReturned == 0 ) {
      _timesColorReturned++;
      return (ColorManager.colors().Count - 1);
    }

    return base.GetColor();
  }
}