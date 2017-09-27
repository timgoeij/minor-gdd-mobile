using System.Collections.Generic;
using UnityEngine;

public class ByebyePattern : IBackgroundPattern {

	private GameObject _bgItem;
	private int _bgSpawnChance = 50;
	private int _maxBgItems = 300;

  public ByebyePattern()
  {
    _bgItem = Resources.Load("BgItem") as GameObject;
  }

  public void Stop()
  {
    foreach(ByeByeItem item in MonoBehaviour.FindObjectsOfType<ByeByeItem>()) {
      item.FadeOut();
    }
  }

  public void Update()
  {
    CreateBackgroundItems();
  }

  private void CreateBackgroundItems() {
    ByeByeItem[] allItems = MonoBehaviour.FindObjectsOfType<ByeByeItem>();

		if ((allItems.Length < _maxBgItems && UnityEngine.Random.Range(0, _bgSpawnChance) == 0) || allItems.Length < 10) {
			MonoBehaviour.Instantiate(_bgItem);
		}
	}
}