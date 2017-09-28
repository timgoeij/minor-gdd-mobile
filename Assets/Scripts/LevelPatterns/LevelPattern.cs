using System.Collections.Generic;
using UnityEngine;

public abstract class LevelPattern 
{
  protected List<GameObject> _availableObstables = new List<GameObject>();
  protected int _obstacleChance;
  protected float _timeBetweenObstacles;
  protected float _patternDuration;
  protected int _maxObstaclesSkippedInRow;

  protected int _maxColorRepeat = 1;
  protected int _lastColorIndex = 0;
  protected int _lastColorRepeatTimes = 0;

  protected float _quietTime = 0;

  protected bool _allowDescending = true;
  protected int? _descendingChance;

  protected bool _isFallingPattern = false;

  public virtual void init() {
    return;
  }

  public virtual bool IsFallingPattern() {
    return _isFallingPattern;
  }

  public virtual int? DescendingChance() {
    return _descendingChance;
  }

  public virtual bool AllowDescending() {
    return _allowDescending;
  }

  public virtual float QuietTime() {
    return _quietTime;
  }
  public virtual int MaxObstaclesSkippedInRow() {
    return _maxObstaclesSkippedInRow;
  }
  public virtual List<GameObject> availableObstacles()
  {
    return _availableObstables;
  }

  public virtual int ObstacleChance()
  {
    return _obstacleChance;
  }

  public virtual float PatternDuration()
  {
    return _patternDuration;
  }

  public virtual float TimeBetweenObstacles()
  {
    return _timeBetweenObstacles;
  }

  public virtual int MaxColorRepeat() {
    return _maxColorRepeat;
  }

  public virtual int GetColor() {
    int c = ColorManager.GetRandomColorIndex();

    if (c == _lastColorIndex && _lastColorRepeatTimes > _maxColorRepeat) {
      c = ColorManager.FindNextIndex(c);
    }

    _lastColorIndex = c;
    _lastColorRepeatTimes = 1;

    return c;
  }

  protected void LoadObstacle(string name) {
    _availableObstables.Add( Resources.Load(name) as GameObject );
  }
}