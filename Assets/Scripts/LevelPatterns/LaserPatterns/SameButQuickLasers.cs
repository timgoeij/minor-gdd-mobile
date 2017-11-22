using ColourRun.Managers;
using UnityEngine;

public class SameButQuickLasers : LevelPattern
{
  private int _colorIndex = 0;

  public SameButQuickLasers() {
    LoadObstacle("Laser");
    _timeBetweenObstacles = 0.3f;
    _obstacleChance = 0;
    _patternDuration = 1.2f;
    _maxObstaclesSkippedInRow = 0;
    _quietTime = 1.2f;
    
  }

  public override void init() {
    base.init();
    _colorIndex = ColorManager.GetRandomColorIndex();
  }

  public override int GetColor() {
    return _colorIndex;
  }
}