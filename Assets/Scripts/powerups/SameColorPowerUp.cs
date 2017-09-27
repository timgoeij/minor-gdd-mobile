using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameColorPowerUp : BasePowerUp {

	// Use this for initialization
	public override void Start ()
    {
        base.Start();	
	}

    public override void Activate()
    {
        Laser[] lasers;

        do
        {
            lasers = FindObjectsOfType<Laser>();

            foreach (Laser laser in lasers)
            {
                laser.SameColorAsPlayer = true;
                laser.ChangeColor();
                
            }

            timer += Time.deltaTime;
        }
        while (timer < duration);

        foreach (Laser laser in lasers)
        {
            laser.SameColorAsPlayer = false;
        }

        Destroy();
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
