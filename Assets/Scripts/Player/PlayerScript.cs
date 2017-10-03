using ColourRun.Cameras;
using ColourRun.Controller;
using ColourRun.Interfaces;
using ColourRun.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScript : ColorChangeableObject, IHitable {

	private float maxSpeed = 0.7f;
	private float _speed = 0;
    private bool secondChance = false;
    private bool tutorialEventAlreadyActivated = false;
    private TutorialManager tm;
    private bool _isDead = false;
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

    public GameObject NearDeath
    {
        get { return _nearDeath; }
    }

	private GameObject _deathAlerted = null;

	private TrailRenderer _trailRenderer;
	private SoundEffectManager _soundManager;
	private Rigidbody2D _rigidbody;

	private CameraScript _cameraScript;

	private Transform _transform;
	
	private TimeManager _timeManager;

	private SpriteRenderer _renderer;

	private ScoreManager _scoreManager;

	void Awake() {
		_trailRenderer = GetComponentInChildren<TrailRenderer>();
		_soundManager = FindObjectOfType<SoundEffectManager>();
		_rigidbody = GetComponent<Rigidbody2D>();
		_cameraScript = Camera.main.GetComponent<CameraScript>();
		tm = FindObjectOfType<TutorialManager>();
		_transform = transform;
		_timeManager = FindObjectOfType<TimeManager>();
		_renderer = GetComponent<SpriteRenderer>();
		_scoreManager = FindObjectOfType<ScoreManager>();
	}

	public override void Start()
	{
		base.Start();
		SetColor( UnityEngine.Random.Range(0, (ColorManager.colors().Count)) );
		SetTrailColor(); 
	}
	
	private void SetTrailColor() {
		if (_trailRenderer != null) {
			Color c = GetCurrentColor();
			c.a = 0.5f;

			_trailRenderer.startColor = c;
			_trailRenderer.endColor = c;
		}
	}

	void FixedUpdate () {
		if (_isDead) {
			return;
		}

		CheckTutorialDistance();
		CheckGrounded();
		CheckNearDeath();

		if (_nearDeath != null) {
			if (_deathAlerted == null || _nearDeath != _deathAlerted) {
				_soundManager.PlayAlert();
				_deathAlerted = _nearDeath;
				Vector3 sparkPos = transform.position;
				Spark(sparkPos, 30);
			}

			if (tm.IsTutorialActive
					&& tm.IsFirstMultiplierActive && tutorialEventAlreadyActivated)
			{
						Time.timeScale = 0;
						tutorialEventAlreadyActivated = false;
			}
			else
			{
					Time.timeScale = 0.75f;
			}
		}
		else
		{
				if (!tm.IsTutorialActive)
						Time.timeScale = 1;
		}

    CheckWallJump();
		Move();
	}

    private void CheckTutorialDistance()
    {
        if (!tm.IsTutorialActive)
            return;

        if (!tm.IsFirstColorActive)
            return;

        RaycastHit2D tutorialHit = Physics2D.Raycast(transform.position, Vector2.right, 10);

        if (tutorialHit.collider != null && tutorialHit.collider.GetComponent<Laser>() != null && !tutorialEventAlreadyActivated)
        {
            tutorialEventAlreadyActivated = true;
            Time.timeScale = 0;
        }

    }

    private void Spark(Vector3 pos, int amount) {
		for(int i = 0; i < amount; i++) {
			GameObject spark = PoolManager.GetItem("Spark");
			if (spark) {
				pos.z = 21;
				spark.transform.position = pos;
				spark.SetActive(true);
			}
		}
	}

	private void Move() {
		_transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(1, 0, 0), speed);
	}

	private void CheckWallJump() {
		RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, 1.2f);
		RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, 1.2f);

		if (rightHit.collider != null && rightHit.collider.tag == "Wall" || leftHit.collider != null && leftHit.collider.tag == "Wall" ) {
			
			if (rightHit.collider != null && _speed > 0 || leftHit.collider != null && _speed < 0) {
				_speed *= -1;
			}

			_touchedWall = true;
			
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
			_cameraScript.Shake(0.5f);
			
			_timeManager.FreezeFrame();


			Vector3 sparkPos = (rightHit.collider != null ) ? rightHit.point : leftHit.point;
			Spark(sparkPos, 30);

			_soundManager.PlayJump();
		}
	}

	private void CheckGrounded() {

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);

		bool prevState = _grounded;

		if (hit.collider != null && hit.collider.tag == "Floor") {
			if ( ! _grounded) {
				_cameraScript.Shake(0.3f);
			}

			_grounded = true;
			_touchedWall = false;

			if (! prevState) {
				_soundManager.PlayLanding();

				Vector3 sparkPos = hit.point;
				Spark(sparkPos, 15);

			}

		} else {
			_grounded = false;
		}
	}

	private void CheckNearDeath() {

		RaycastHit2D[] hit = Physics2D.CircleCastAll( transform.position, 1f, (speed > 0) ? Vector2.right : Vector2.left, 1.5f);

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
		_rigidbody.gravityScale = 0;
		_rigidbody.velocity = new Vector2(0,0);
		
		FindObjectOfType<BackgroundMusicManager>().StartPitch(-0.5f, 0.5f);
		_timeManager.SetSlomo(0.5f, 1.5f);
		
		_renderer.sprite = null;
		
		_trailRenderer.gameObject.SetActive(false);
    	

		_cameraScript.Shake();
		_soundManager.PlayDeath();

		GameObject explosion = PoolManager.GetItem("Explosion");

		if (explosion != null) {
			explosion.transform.position = transform.position;
			explosion.SetActive(true);
		}

		FindObjectOfType<GameController>().EndGame();

		List<GameObject> bloodSpatters = PoolManager.GetAll("Blood");
		
		foreach(GameObject o in bloodSpatters) {
			o.GetComponent<SpriteRenderer>().color = ColorManager.colors()[ UnityEngine.Random.Range(0, ColorManager.colors().Count) ];
			o.SetActive(true);
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
			_scoreManager.AddBonusPoints(25, "Superman!");
		}
	}

	public void SetMultipliers(GameObject obstacle) {
		if (_nearDeath != null && _nearDeath == obstacle) {
			_scoreManager.multiplier++;
		} else {
			_scoreManager.multiplier = 1;
		}
	}

	public override void SetColor(int color) {
		base.SetColor(color);

		if (_trailRenderer != null) {

				Color trailColor = GetCurrentColor();
				trailColor.a = 0.5f;

				_trailRenderer.startColor = trailColor;
				_trailRenderer.endColor = trailColor;
		}
	}
}
