using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScript : ColorChangeableObject, IHitable {

	private float maxSpeed = 0.7f;
	private float _speed = 0;
    private bool secondChance = false;

	private bool _isDead = false;

	[SerializeField]
	private GameObject _explosion;

	[SerializeField]
	private GameObject _blood;

	[SerializeField]
	private GameObject _spark;

	[SerializeField]
	private bool _grounded = true;

	public bool isGrounded {
		get {
			return _grounded;
		}
	}

	public float speed {
		get {
			return _speed;
		} 
		set {
			if (value <= maxSpeed) {
				_speed = value;
			}
		}
	}

    public bool SecondChance
    {
        get { return secondChance; }
        set { secondChance = value; }
    }

	[SerializeField]
	private bool _touchedWall = false;

	public bool touchedWall {
		get {
			return _touchedWall;
		}

		private set {
			_touchedWall = value;
		}
	}

	private GameObject _nearDeath = null;

	private GameObject _deathAlerted = null;

	public override void Start()
	{
		base.Start();
		SetColor( UnityEngine.Random.Range(0, (ColorManager.colors().Count)) );
		SetTrailColor();
	}
	
	private void SetTrailColor() {
		if (GetComponentInChildren<TrailRenderer>() != null) {
			Color c = GetCurrentColor();
			c.a = 0.5f;

			GetComponentInChildren<TrailRenderer>().startColor = c;
			GetComponentInChildren<TrailRenderer>().endColor = c;
		}
	}

	void FixedUpdate () {
		if (_isDead) {
			return;
		}

		CheckGrounded();
		CheckNearDeath();

		if (_nearDeath != null) {

			if (_deathAlerted == null || _nearDeath != _deathAlerted) {
				FindObjectOfType<SoundEffectManager>().PlayAlert();
				_deathAlerted = _nearDeath;
			}
			
			
			Time.timeScale = 0.75f;

			for(int i = 0; i < 15; i++) {
				GameObject spark = Instantiate(_spark);
				Vector3 sparkPos = transform.position;
				sparkPos.z = UnityEngine.Random.Range(3, 5);
				spark.transform.position = sparkPos;
			}
	
			
			
		} else {
			Time.timeScale = 1;
		}

		CheckWallJump();
		
		Move();
	}

	private void Move() {
		transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(1, 0, 0), speed);
	}

	private void CheckWallJump() {
		RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, 1.2f);
		RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, 1.2f);

		if (rightHit.collider != null && rightHit.collider.tag == "Wall" || leftHit.collider != null && leftHit.collider.tag == "Wall" ) {
			_speed *= -1;
			_touchedWall = true;
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
			Camera.main.GetComponent<CameraScript>().Shake(0.5f);
			FindObjectOfType<TimeManager>().FreezeFrame();

			for(int i = 0; i < 30; i++) {
					GameObject spark = Instantiate(_spark);
					Vector3 sparkPos = (rightHit.collider != null ) ? rightHit.point : leftHit.point;
					sparkPos.z = UnityEngine.Random.Range(3, 5);
					spark.transform.position = sparkPos;
			}

			FindObjectOfType<SoundEffectManager>().PlayJump();
		}
	}

	private void CheckGrounded() {

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);

		bool prevState = _grounded;

		if (hit.collider != null && hit.collider.tag == "Floor") {
			if ( ! _grounded) {
				Camera.main.GetComponent<CameraScript>().Shake(0.3f);
			}

			_grounded = true;
			_touchedWall = false;

			if (! prevState) {
				FindObjectOfType<SoundEffectManager>().PlayLanding();

				for(int i = 0; i < 15; i++) {
					GameObject spark = Instantiate(_spark);
					Vector3 sparkPos = hit.point;
					sparkPos.z = UnityEngine.Random.Range(3, 5);
					spark.transform.position = sparkPos;
				}				
			}

		} else {
			_grounded = false;
		}
	}

	private void CheckNearDeath() {

		RaycastHit2D[] hit = Physics2D.CircleCastAll( transform.position, 1f, Vector2.right, 1.5f);

		IEnumerable<RaycastHit2D> hits = hit.Where(
		c => c.collider.GetComponent<Laser>() != null &&
				c.collider.GetComponent<Laser>().GetCurrentColor() != GetCurrentColor() &&
				! c.collider.GetComponent<Laser>().isHit
		);

		if ( hits.Count() != 0) {		
			_nearDeath = hits.OrderBy(c => c.collider.transform.position.x).First().collider.gameObject;
		} else if (_nearDeath != null && _nearDeath.transform.position.x < transform.position.x) {
			_nearDeath = null;
		}

	}

	public void Hit() {
		if (_isDead) {
			return;
		}

    if( ! secondChance) {
			speed = 0;
			Die();
		} else {
    	secondChance = false;
    } 
	}
	
	private void Die() {
		if (_isDead) {
			return;
		}

		FindObjectOfType<BackgroundMusicManager>().StartPitch(-0.5f, 0.5f);
		FindObjectOfType<TimeManager>().SetSlomo(0.5f, 1.5f);
		GetComponent<SpriteRenderer>().sprite = null;
		gameObject.GetComponentInChildren<TrailRenderer>().gameObject.SetActive(false);
    FindObjectOfType<GameController>().EndGame();

		Camera.main.GetComponent<CameraScript>().Shake();
		FindObjectOfType<SoundEffectManager>().PlayDeath();

		GameObject explosion = Instantiate(_explosion);
		explosion.transform.position = transform.position;

		FindObjectOfType<GameController>().EndGame();

		for(int i = 0; i < 500; i++) {
			GameObject point = Instantiate(_blood);
			point.GetComponent<SpriteRenderer>().color = ColorManager.colors()[ UnityEngine.Random.Range(0, ColorManager.colors().Count) ];
		}

		_isDead = true;;
	}

	public void StartRunning() {
		speed = 0.3f;
	}

	public bool IsDead() {
		return _isDead;
	}

	public void OnScore(GameObject obstacle) {
		if (! _grounded) {
			FindObjectOfType<ScoreManager>().AddBonusPoints(25, "Superman!");
		}
	}

	public void SetMultipliers(GameObject obstacle) {
		if (_nearDeath != null && _nearDeath == obstacle) {
			FindObjectOfType<ScoreManager>().multiplier++;
		} else {
			FindObjectOfType<ScoreManager>().multiplier = 1;
		}
	}

	public override void SetColor(int color) {
		base.SetColor(color);

		if (gameObject.GetComponentInChildren<TrailRenderer>() != null) {

				Color trailColor = GetCurrentColor();
				trailColor.a = 0.5f;

				gameObject.GetComponentInChildren<TrailRenderer>().startColor = trailColor;
				gameObject.GetComponentInChildren<TrailRenderer>().endColor = trailColor;
		}
	}
}
