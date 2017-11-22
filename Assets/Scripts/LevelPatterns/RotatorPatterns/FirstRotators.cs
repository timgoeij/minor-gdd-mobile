using ColourRun.Managers;
using UnityEngine;
public class FirstRotators : LevelPattern
{
  private int _timesColorRequestes = 0;
  public FirstRotators() {
    LoadObstacle("Rotator");
    _timeBetweenObstacles = 1.5f;
    _obstacleChance = 0;
    _patternDuration = 3.5f;
    _maxObstaclesSkippedInRow = 0;
    _maxColorRepeat = 1;
    _quietTime = 1.5f;
    _allowDescending = false;
  }

  public override int GetColor() {
    if (_timesColorRequestes == 0) {
      _timesColorRequestes++;
      return ColorManager.FindIndex( MonoBehaviour.FindObjectOfType<PlayerScript>().GetCurrentColor() );
    }

    return base.GetColor();
  }
}