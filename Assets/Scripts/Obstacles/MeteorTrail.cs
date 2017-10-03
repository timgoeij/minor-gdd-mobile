using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorTrail : MonoBehaviour {

    private TrailRenderer trail;
    private Color trailColor;

    private float timer = 0;
    private float changeColorDelay = 1;

    // Use this for initialization
    void Start () {

        trail = GetComponent<TrailRenderer>();
        trailColor = trail.startColor;
    }
	
	// Update is called once per frame
	void Update () {

        if(trailColor != GetComponentInParent<ColorChangeableObject>().GetCurrentColor())
        {
            trailColor = GetComponentInParent<ColorChangeableObject>().GetCurrentColor();
        }

        Color startColor = trail.startColor;
        startColor.a = PingPong(1, .5f);
        
        if (timer >= changeColorDelay)
        {
            startColor = new Color(trailColor.r * Random.Range(0, trailColor.r), trailColor.g * Random.Range(0, trailColor.g), 
                trailColor.b * Random.Range(0, trailColor.b), startColor.a);

            trail.startColor = startColor;

            timer = 0;
        }

        trail.startColor = startColor;

        timer += Time.deltaTime;

    }

    private float PingPong(float max, float min)
    {
        return Mathf.PingPong(Time.time, max - min) + min;
    }
}
