using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightPattern : IBackgroundPattern
{
  private GameObject _sun;

  public LightPattern() {
    _sun = Resources.Load("Sun") as GameObject;
  }

  public void Init() {
    MonoBehaviour.Instantiate(_sun);
  }

  public void Stop() {

  }

  public void Update() {
    
  }
}