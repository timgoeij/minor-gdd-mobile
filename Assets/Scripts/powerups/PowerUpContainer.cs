using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpContainer : MonoBehaviour {

    [SerializeField]
    private List<BasePowerUp> powerUpList;

    private BasePowerUp powerUp;

    public List<BasePowerUp> PowerUpList
    {
        get { return powerUpList; }
    }

    public BasePowerUp PowerUp
    {
        get { return powerUp; }
        set { powerUp = value; }
    }

    private int randomPowerUpIndex = 0;
    // Use this for initialization
	void Start () {

        randomPowerUpIndex = Random.Range(0, powerUpList.Count);
        powerUp = Instantiate(powerUpList[randomPowerUpIndex], transform.position, Quaternion.identity,transform) as BasePowerUp;
        powerUp.initialize(this);
	}
}
