using UnityEngine;

public class OneQuickTab : LevelPattern
{
  private int _colorIndex = 0;
  public OneQuickTab() {
    LoadObstacle("Laser");
    _timeBetweenObstacles = 0.7f;
    _obstacleChance = 0;
    _patternDuration = 3f;
    _maxObstaclesSkippedInRow = 0;
    _quietTime = 0.6f;

    _colorIndex = ColorManager.GetRandomColorIndex();
  }

  public override int GetColor() {
    _colorIndex = ColorManager.FindNextIndex(ColorManager.FindNextIndex(_colorIndex));

    return _colorIndex;
  }
}