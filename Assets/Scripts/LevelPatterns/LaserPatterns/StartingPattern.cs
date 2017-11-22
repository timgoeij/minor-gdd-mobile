using ColourRun.Managers;
using UnityEngine;

public class StartingPattern : LevelPattern
{
  private int _timesColorReturned = 0;

  public StartingPattern() {
    LoadObstacle("Laser");
    _timeBetweenObstacles = 1.8f;
    _obstacleChance = 0;
    _patternDuration = 10f;
    _maxObstaclesSkippedInRow = 0;
    _allowDescending = false;
  }

  public override int GetColor() {
    if (_timesColorReturned == 0) {
      _timesColorReturned++;
      return ColorManager.FindNextIndex( ColorManager.FindNextIndexOfColor(MonoBehaviour.FindObjectOfType<PlayerScript>().GetCurrentColor()) );
    } 
    else if (_timesColorReturned == 1) {
      _timesColorReturned++;
      return ColorManager.FindNextIndexOfColor(MonoBehaviour.FindObjectOfType<PlayerScript>().GetCurrentColor());
      
    } 
    else {
      return base.GetColor();
    }
  }
}