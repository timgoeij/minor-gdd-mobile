using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HellPattern : IBackgroundPattern {

  private List<HellItem> _items;

  public void Init() {
    _items = new List<HellItem>();
    return;
  }

  public void Stop() {
    foreach(HellItem i in _items.Where(i => i.GetGameObject.activeInHierarchy == true)) {
      i.Explode();
    }
  }

  public void Update() {

    if (_items.Where(i => i.GetGameObject.activeInHierarchy == true && i.isFloating == false).Count() < 3) {
      GameObject item = PoolManager.GetItem("HellItem");
      if (item != null) {
        item.SetActive(true);
        _items.Add(item.GetComponent<HellItem>());
      }
    }
  }
}