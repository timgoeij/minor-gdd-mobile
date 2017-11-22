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
    
    AudioSource _source;
    Image _audioSprite;

    void Awake() {
        _source = GetComponentInChildren<AudioSource>();
        _audioSprite = GameObject.FindGameObjectWithTag("AudioToggle").GetComponent<Image>();
    }

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

        _source.mute = isMusicOff;

        _audioSprite.sprite = isMusicOff ? _offSprite : _onSprite;
    }

	public void ToggleMusic() {
       
        _source.mute = !_source.mute;
        isMusicOff = !isMusicOff;

        PlayerPrefs.SetInt("Audio", isMusicOff ? 0 : 1);
		
        _audioSprite.sprite = (_source.mute) ? _offSprite : _onSprite;
	}
}
