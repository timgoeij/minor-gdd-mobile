using UnityEngine;

public class CubeFallingPattern : LevelPattern
{
  public CubeFallingPattern() {
    LoadObstacle("RotationCube");
    //_timeBetweenObstacles = 1.8f;
    //_obstacleChance = 0;
    _patternDuration = 10f;
    //_maxObstaclesSkippedInRow = 0;
    //_quietTime = 2f;
    _allowDescending = false;
    _isFallingPattern = true;
  }

  public override void init() {
    MonoBehaviour.FindObjectOfType<FloorManager>().fallingPattern = this;
  }
}