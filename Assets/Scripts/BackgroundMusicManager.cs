using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour {

	[SerializeField]
	private AudioClip _byeByePlanet;
	[SerializeField]
	private AudioClip _aWelcomeHell;

	private float _timeInPitchDown = 0f;
	private float _pitchDownDuration = 0f;
	private bool _inPitchdown = false;
	private float _pitchChange;
	private int _songIndex = 0;
	private List<BgAudio> _songs;

	void Awake() {
		_songs = new List<BgAudio> {
			new BgAudio {
				clip = _byeByePlanet,
				bgPattern = new ByebyePattern()
			},
			new BgAudio {
				clip = _aWelcomeHell,
				bgPattern = new HellPattern()
			}			
		};
	}

	// Use this for initialization
	void Start () {
		StartPlayingSong(_songs[_songIndex]);
	}

	// Update is called once per frame
	void Update () {
		HandlePitch();

		if ( ! GetComponent<AudioSource>().isPlaying && Time.timeScale > 0) {
			_songIndex++;

			if (_songIndex >= _songs.Count) {
				_songIndex = 0;
			}

			StartPlayingSong( _songs[ _songIndex ] );
		}
	}

	public void PlayFirstSong() {
		_songIndex = 0;
		StartPlayingSong(_songs[_songIndex]);
		
	}

	private void StartPlayingSong(BgAudio bgAudio) {
		GetComponent<AudioSource>().clip = bgAudio.clip;
		GetComponent<AudioSource>().Play();
		FindObjectOfType<BackgroundManager>().SetPattern(bgAudio.bgPattern);
	}

	private void HandlePitch() {
		if (_inPitchdown) {
			if (_timeInPitchDown >= _pitchDownDuration) {
				GetComponent<AudioSource>().pitch = 1f;
				_timeInPitchDown = 0;
				_pitchDownDuration = 0;
				_inPitchdown = false;
			} else {
				GetComponent<AudioSource>().pitch += _pitchChange;
				_timeInPitchDown += Time.deltaTime;
			}
		}
	}

	public void StartPitch(float pitchChange, float duration) {
		_pitchChange = pitchChange;
		_timeInPitchDown = 0;
		_pitchDownDuration = duration;
		_inPitchdown = true;
	}

	public void PauseMusic() {
		GetComponent<AudioSource>().Pause();
	}

	public void ResumeMusic() {
		GetComponent<AudioSource>().Play();
	}
}

public class BgAudio {
	public AudioClip clip;
	public IBackgroundPattern bgPattern;

}
