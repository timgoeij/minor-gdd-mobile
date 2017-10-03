using ColourRun.Interfaces;
using ColourRun.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColourRun.Cameras;

public class Laser : ColorChangeableObject, IObstacle, IHitable {
    private GameObject _player;

    private InputManager _inputManager;

    [SerializeField]
    private GameObject _pointObject;

    [SerializeField]
    private GameObject _explosion;

    protected bool _isHit = false;

    private TutorialManager tm;

    protected SoundEffectManager soundManager;

    protected CameraScript cameraScript;

    protected TimeManager timeManager;
    protected ScoreManager scoreManager;
    public bool isHit {
        get {
            return _isHit;
        }
    }

    public override void Awake() {
        base.Awake();
        soundManager = FindObjectOfType<SoundEffectManager>();
        cameraScript = Camera.main.GetComponent<CameraScript>();
        timeManager = FindObjectOfType<TimeManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public override void Start () {
        base.Start();

        _inputManager = FindObjectOfType<InputManager>();
        tm = FindObjectOfType<TutorialManager>();
        _inputManager.add(this);
    }
    public virtual void FixedUpdate() {
        if (_isHit) {
            Color c = spriteRenderer.color;
            c.a = 0.5f;
            
            spriteRenderer.color = c;
        }

        if(CameraScreen.ObjectIsBehindCamera(transform) && transform.parent == null)
        {
            Destroy(gameObject);
        }
    }

    public void CheckColors (ColorChangeableObject player) {
        if (playerScript.IsDead()) {
            return;
        }

        if (player.GetCurrentColor() != GetCurrentColor()) {
            playerScript.Hit();
            return;
        }


        playerScript.SetMultipliers(gameObject);        
        scoreManager.AddPoints( GetPoints() );
        playerScript.OnScore(gameObject);
        
        Hit();
    }
    
    private int GetPoints() {
        int points = 10;

        System.Type t = this.GetType();

        if (t == typeof(UpAndDownLaser)) {
            points += 5;
        }

        if (GetComponentInParent<Rotator>() != null) {
            points += 10;
        }

        if (GetComponentInParent<RotationCube>() != null) {
            points += 15;
        }

        if (t == typeof(RollingCircle)) {
            points += 40;
        }

        if (t == typeof(TriangleShark)) {
            points += 65;
        }

        if (t == typeof(Meteor)) {
            points += 90;
        }

        return points;
    }

    public void Hit() {
        if (_isHit) {
            return;
        }

        timeManager.FreezeFrame();
        soundManager.PlayDestroy();
        soundManager.PlayDeath();
        
        cameraScript.Shake();

        for (int i = 0; i < 50; i++) {
            GameObject point = PoolManager.GetItem("Point");
            
            if (point != null) {
                point.GetComponent<SpriteRenderer>().color = GetCurrentColor();
                point.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
                point.SetActive(true);
            }
        }

        GameObject explosion = PoolManager.GetItem("Explosion");
        
        if (explosion) {
            Vector3 explosionPosition = transform.position;
            explosionPosition.y = FindObjectOfType<PlayerScript>().transform.position.y;
            explosion.transform.position = explosionPosition;
            explosion.SetActive(true);
        }


        _isHit = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit) {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            if (tm.IsTutorialActive && tm.IsFirstMultiplierActive)
            {
                tm.IsFirstMultiplierActive = false;
                tm.IsTryingSelf = true;
            }
            else if (tm.IsTutorialActive && tm.IsFirstColorActive)
            {
                Debug.Log("????");
                tm.IsFirstColorActive = false;
                tm.IsFirstMultiplierActive = true;
            }   

            CheckColors(collision.GetComponent<ColorChangeableObject>());
        }   
    }

    void OnDestroy() {
        _inputManager.remove(this);
    }

    float IObstacle.GetYOffset()
    {
        return spriteRenderer.bounds.extents.y; 
    }
}
