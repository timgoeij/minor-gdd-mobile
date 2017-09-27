using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject meteorPrefab;

    public enum MeteorSpawnMode
    {
        SingleMeteor,
        DoubleMeteors,
        SmallMeteorShower,
        BigMeteorShower,
        NoMode
    }

    private MeteorSpawnMode spawnMode = MeteorSpawnMode.BigMeteorShower;
    
    public MeteorSpawnMode SpawnMode
    {
        get { return spawnMode; }
        set { spawnMode = value; }
    }

    private Transform player;

    private float maxSpawnTime = 11;

    private float singleAndDoubleSpawnTime = 4;
    private float smallShowerSpawnTime = 2;
    private float bigShowerSpawnTime = 1;

    private float spawnTimer = 0;
    private float spawnModeTimer = 0;


    private float spawnDelay = 0;
    
    // Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player").transform;
		
	}
	
	// Update is called once per frame
	void Update () {

        switch (spawnMode)
        {
            case MeteorSpawnMode.SingleMeteor:
            case MeteorSpawnMode.DoubleMeteors:
                spawnDelay = singleAndDoubleSpawnTime;
                break;
            case MeteorSpawnMode.SmallMeteorShower:
                spawnDelay = smallShowerSpawnTime;
                break;
            case MeteorSpawnMode.BigMeteorShower:
                spawnDelay = bigShowerSpawnTime;
                break;
            case MeteorSpawnMode.NoMode: return;
        }

        if(spawnTimer <= maxSpawnTime)
        {
            if(spawnModeTimer > spawnDelay)
            {
                spawnModeTimer = 0;
                LaunchMeteor();

                if (SpawnMode == MeteorSpawnMode.DoubleMeteors)
                    Invoke("LaunchMeteor", 1);
            }

            spawnModeTimer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

        spawnTimer += Time.deltaTime;
	}

    private void LaunchMeteor()
    {
        Vector3 targetPos = player.position;
        targetPos.x += Random.Range(10, 100);

        Vector3 directon = targetPos - transform.position;
        directon.Normalize();

        Debug.Log("launch");

        GameObject meteor = Instantiate(meteorPrefab, transform.position, Quaternion.identity);
        meteor.GetComponent<Meteor>().SetColor(Random.Range(0, ColorManager.colors().Count));
        meteor.GetComponent<Meteor>().SetTarget(directon);
    }
}
