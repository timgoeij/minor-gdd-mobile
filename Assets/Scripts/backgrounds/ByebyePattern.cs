using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ByebyePattern : IBackgroundPattern {
	private int _bgSpawnChance = 50;

  private List<GameObject> _items;

  public void Init()
  {
    _items = new List<GameObject>();
    return;
  }

  public void Stop()
  {
    foreach(GameObject item in _items.Where(i => i.activeInHierarchy == true)) {
      ByeByeItem s = item.GetComponent<ByeByeItem>();

      if (s != null) {
        s.FadeOut();
      }
    }
  }

  public void Update()
  {
    CreateBackgroundItems();
  }

  private void CreateBackgroundItems() {
    List<GameObject> allItems = _items.Where(i => i.activeInHierarchy == true).ToList();

		if ((allItems.Count < PoolManager.GetAll("ByeByeItem", true).Count && UnityEngine.Random.Range(0, _bgSpawnChance) == 0) || allItems.Count < 5) {
      GameObject item = PoolManager.GetItem("ByeByeItem");
      _items.Add( item );
      item.SetActive(true);
		}
	}
}