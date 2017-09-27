using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HellPattern : IBackgroundPattern {

	private GameObject _bgItem;

  public HellPattern()
  {
    _bgItem = Resources.Load("HellItem") as GameObject;
  }

  public void Stop()
  {
    foreach(HellItem i in MonoBehaviour.FindObjectsOfType<HellItem>()) {
      i.Explode();
    }
  }

  public void Update()
  {
    HellItem[] items = MonoBehaviour.FindObjectsOfType<HellItem>();

    if (items.Where(i => i.isFloating == false).Count() < 3) {
      if (_bgItem != null) {
        MonoBehaviour.Instantiate( _bgItem );
      }
    }
  }
}