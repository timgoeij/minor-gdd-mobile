using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicScript : MonoBehaviour {

	[SerializeField]
	Sprite _onSprite;
	
	[SerializeField]
	Sprite _offSprite;

    private bool isMusicOff = false;

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.HasKey("Audio"))
        {
            isMusicOff = PlayerPrefs.GetInt("Audio") == 0;
        }
        else
        {
            PlayerPrefs.SetInt("Audio", isMusicOff ? 0 : 1);
        }

        GetComponentInChildren<AudioSource>().mute = isMusicOff;

        GameObject.FindGameObjectWithTag("AudioToggle").GetComponent<Image>().sprite = isMusicOff ? _offSprite : _onSprite;
    }
	
	// Update is called once per frame
	void Update () {

	}

	public void ToggleMusic() {
       
        GetComponentInChildren<AudioSource>().mute = !GetComponentInChildren<AudioSource>().mute;
        isMusicOff = !isMusicOff;

        PlayerPrefs.SetInt("Audio", isMusicOff ? 0 : 1);

		GameObject.FindGameObjectWithTag("AudioToggle").GetComponent<Image>().sprite = (GetComponentInChildren<AudioSource>().mute) ? _offSprite : _onSprite;
	}
}
