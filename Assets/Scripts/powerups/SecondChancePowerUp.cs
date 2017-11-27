using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondChancePowerUp : BasePowerUp {

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    public override void Activate()
    {
        FindObjectOfType<PlayerScript>().SecondChance = true;

        Destroy();
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
