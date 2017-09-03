using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Laser : ColorChangeableObject {
    private GameObject _player;
    
    private InputManager _inputManager;

    // Use this for initialization
	public override void Start () {
        base.Start();

        _inputManager = FindObjectOfType<InputManager>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _inputManager.add(this);
    }
	
	// Update is called once per frame
	public void CheckColors (ColorChangeableObject player) {

        if (player.GetCurrentColor() != GetCurrentColor())
        {
            _player.GetComponent<PlayerScript>().Hit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CheckColors(collision.GetComponent<ColorChangeableObject>());
        }   
    }

    void OnDestroy() {
        _inputManager.remove(this);
    }
}
