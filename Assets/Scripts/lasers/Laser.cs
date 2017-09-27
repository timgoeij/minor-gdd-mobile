using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : ColorChangeableObject, IObstacle, IHitable {
    private GameObject _player;

    private InputManager _inputManager;

    [SerializeField]
    private GameObject _pointObject;

    [SerializeField]
    private GameObject _explosion;

    protected bool _isHit = false;

    public bool isHit {
        get {
            return _isHit;
        }
    }

    public override void Start () {
        base.Start();

        _inputManager = FindObjectOfType<InputManager>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _inputManager.add(this);
    }
    public virtual void FixedUpdate() {
        if (_isHit) {
            Color c = GetComponent<SpriteRenderer>().color;
            c.a = 0.5f;
            GetComponent<SpriteRenderer>().color = c;
        }

        if(CameraScreen.ObjectIsBehindCamera(transform) && transform.parent == null)
        {
            Destroy(gameObject);
        }
    }

    public void CheckColors (ColorChangeableObject player) {
        if (_player.GetComponent<PlayerScript>().IsDead()) {
            return;
        }

        if (player.GetCurrentColor() != GetCurrentColor()) {
            _player.GetComponent<PlayerScript>().Hit();
            return;
        }


        FindObjectOfType<PlayerScript>().SetMultipliers(gameObject);        
        FindObjectOfType<ScoreManager>().AddPoints( GetPoints() );
        FindObjectOfType<PlayerScript>().OnScore(gameObject);
        
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

        return points;
    }

    public void Hit() {
        if (_isHit) {
            return;
        }

        FindObjectOfType<TimeManager>().FreezeFrame();
        FindObjectOfType<SoundEffectManager>().PlayDestroy();
        FindObjectOfType<SoundEffectManager>().PlayDeath();
        
        Camera.main.GetComponent<CameraScript>().Shake();

        for (int i = 0; i < UnityEngine.Random.Range(100, 150); i++) {
            GameObject point = Instantiate(_pointObject);
            point.GetComponent<SpriteRenderer>().color = GetCurrentColor();
            point.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        GameObject explosion = Instantiate(_explosion);
        
        Vector3 explosionPosition = transform.position;
        explosionPosition.y = FindObjectOfType<PlayerScript>().transform.position.y;
        explosion.transform.position = explosionPosition;

        _isHit = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CheckColors(collision.GetComponent<ColorChangeableObject>());
        }   
    }

    void OnDestroy() {
        _inputManager.remove(this);
    }

    float IObstacle.GetYOffset()
    {
        return GetComponent<SpriteRenderer>().bounds.extents.y; 
    }
}
