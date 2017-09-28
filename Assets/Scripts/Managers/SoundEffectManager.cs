using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {

	[SerializeField]
	private AudioClip _destroy; 
	private AudioSource _destroySource;
	private float _destroyDefaultPitch = 1f;
	private float _timeSinceDestroyIsPlayed = 0;

	[SerializeField]
	private AudioClip _points; 
	private AudioSource _pointsSource;

	[SerializeField]
	private AudioClip _notification; 
	private AudioSource _notificationSource;

	private float _pointsSourceDefaultPitch = 1f;
	private float _timeSincePointsIsPlayed = 0;

	private bool muted = false;

	[SerializeField]
	private AudioClip _death; 
	private AudioSource _deathSource;

	[SerializeField]
	private AudioClip _landing; 
	private AudioSource _landingSource;

	[SerializeField]
	private AudioClip _jump; 
	private AudioSource _jumpSource;

	private float _timeSinceJumpIsPlayed = 0;

	[SerializeField]
	private AudioClip _alert; 
	private AudioSource _alertSource;

	void Awake() {
		_destroySource = gameObject.AddComponent<AudioSource>();
		_destroySource.clip = _destroy;
		_destroySource.volume = 0.3f;

		_pointsSource = gameObject.AddComponent<AudioSource>();
		_pointsSource.clip = _points;
		_pointsSource.volume = 0.2f;
		
		_pointsSource.pitch = _pointsSourceDefaultPitch;

		_notificationSource = gameObject.AddComponent<AudioSource>();
		_notificationSource.clip = _notification;
		_notificationSource.volume = 0.5f;

		_deathSource = gameObject.AddComponent<AudioSource>();
		_deathSource.clip = _death;
		_deathSource.volume = 0.5f;

		_deathSource = gameObject.AddComponent<AudioSource>();
		_deathSource.clip = _death;
		_deathSource.volume = 0.5f;

		_landingSource = gameObject.AddComponent<AudioSource>();
		_landingSource.clip = _jump;
		_landingSource.volume = 0.3f;
		_landingSource.pitch = 0.5f;

		_jumpSource = gameObject.AddComponent<AudioSource>();
		_jumpSource.clip = _jump;
		_jumpSource.volume = 0.3f;
		_jumpSource.pitch = 0.9f;

		_alertSource = gameObject.AddComponent<AudioSource>();
		_alertSource.clip = _alert;
		_alertSource.volume = 0.5f;
	}

	// Use this for initialization
	void Start () {
		    if (PlayerPrefs.HasKey("Audio") && (PlayerPrefs.GetInt("Audio") == 0))
        {
            ToggleEffects();
        }
	}
	
	// Update is called once per frame
	void Update () {
		_timeSincePointsIsPlayed += Time.unscaledDeltaTime;
		_timeSinceDestroyIsPlayed += Time.unscaledDeltaTime;
		_timeSinceJumpIsPlayed += Time.unscaledDeltaTime;
		
	}

	public void PlayPoints() {
		if (_timeSincePointsIsPlayed > 0.5f | _pointsSource.pitch > 1.1f) {
			_pointsSource.pitch = _pointsSourceDefaultPitch;
		} else {
			_pointsSource.pitch += 0.001f;
		}

		Play(_pointsSource);
		_timeSincePointsIsPlayed = 0;
	}

	public void StopPoints() {
		_pointsSource.Stop();
	}

	public void PlayDestroy() {
		if (_timeSinceDestroyIsPlayed > 1.2f | _destroySource.pitch > 2f) {
			_destroySource.pitch = _destroyDefaultPitch;
		} else {
			_destroySource.pitch += 0.01f;
		}

		Play(_destroySource);
		_timeSinceDestroyIsPlayed = 0;
	}

	public void PlayNotification() {
		_notificationSource.loop = true;
		_notificationSource.volume = 1f;
		Play(_notificationSource);
	}

	public void StopNotification() {
		_notificationSource.Stop();
	}

	private void Play(AudioSource source) {
		if (muted) return;

		source.Play();
	}

	public void ToggleEffects() {
		muted = !muted;

		_destroySource.mute = muted;
		_notificationSource.mute = muted;
		_pointsSource.mute = muted;
		_deathSource.mute = muted; 
	}

	public void PlayDeath(){
		Play(_deathSource);
	}

	public void PlayLanding(){
		Play(_landingSource);
	}

	public void PlayJump() {
		if (_timeSinceJumpIsPlayed < 1.2f) {
			_jumpSource.pitch += 0.1f;
		} else {
			_jumpSource.pitch = 0.9f;
		}

		Play(_jumpSource);
		_timeSinceJumpIsPlayed = 0;
	}

	public void PlayAlert() {
		_alertSource.Play();
	}
}
