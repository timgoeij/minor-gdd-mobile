using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	private GameObject _target;
	private float _lerp = 1f;
	private bool _shake = false;
	private float _shakeAmount = 0.7f;
	private float _shakeDuration = 0.1f;
	private float _shakeTime = 0;
	// Use this for initialization
	void Start () {
		SetTarget("Player");
	}

	public void SetTarget(string targetTag) {
		_target = GameObject.FindGameObjectWithTag(targetTag);

		Vector3 pos = transform.position;
		pos.z = 0;
		transform.position = pos;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (_target != null) {
			FollowTarget();
		}

		if (_shake) {
			Vector3 shakePos = transform.localPosition + Random.insideUnitSphere * _shakeAmount;
			shakePos.z = 0;

			transform.localPosition = shakePos;

			if (_shakeTime <= _shakeDuration) {
				_shakeTime += Time.deltaTime;
			} else {
				_lerp = 1;
				_shakeTime = 0;
				_shake = false;
			}
		}
	}

	public void Shake() {
		Shake(0.7f);
	}

	public void Shake(float amount) {
		_shake = true;
		_shakeAmount = amount;
	}

	private void FollowTarget() {
		if (FindObjectOfType<LevelManager>().playerInPattern.GetType() == typeof(FallingGatePattern) &&
				_target.tag == "Player" &&
				! _target.GetComponent<PlayerScript>().isGrounded 
		) {
			Vector3 newPos = _target.transform.position;
			
			newPos.y -= (CameraScreen.height / GetDivider());
			newPos.z = 0;

			transform.position = Vector3.Lerp(transform.position, newPos, 0.1f);
		} else {
			Vector3 newPos = _target.transform.position;
			
			newPos.x += CameraScreen.width / GetDivider();
			newPos.y += (CameraScreen.height / (GetDivider() * 3));
			newPos.z = 0;

			transform.position = Vector3.Lerp(transform.position, newPos, _lerp);
		}		
	}

	private int GetDivider() {
		if (Camera.main.aspect >= 1.7)
		{
				return 3;
		}
		else if (Camera.main.aspect >= 1.5)
		{
				return 3;
		}
		else
		{
				return 4;
		}
	} 
}
