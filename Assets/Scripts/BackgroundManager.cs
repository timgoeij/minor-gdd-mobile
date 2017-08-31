using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	[SerializeField]
	private GameObject _bgItem;
	private List<GameObject> _backgroundItems = new List<GameObject>();

	private int _bgSpawnChance = 25;
	private int _maxBgItems = 50;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CreateBackgroundItems();
		CleanBackgroundItems();
	}

	private void CreateBackgroundItems() {
		if (_backgroundItems.Count < _maxBgItems && UnityEngine.Random.Range(0, _bgSpawnChance) == 0) {
			GameObject item = Instantiate(_bgItem);
			_backgroundItems.Add(item);
		}
	}

	private void CleanBackgroundItems() {
		for(int i = 0; i < _backgroundItems.Count; i++) {
			GameObject item = _backgroundItems[i];

			if (CameraScreen.ObjectIsBehindCamera(item.transform)) {
				_backgroundItems.Remove(item);
				Destroy(item);
			}
		}
	}
}
