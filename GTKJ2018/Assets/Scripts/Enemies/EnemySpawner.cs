using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    //Enemy Container
    public List<EnemyFlierController> myFlyingEnemiesList;
    public EnemyFlierController myFlyingEnemy;
    public GroundedEnemyController myWalkingEnemy;
    public GunFire myShooterEnemy;
    public Vector3 initialPos;
    public bool enemyDeployed;
    public bool canDeploy;
    public bool isConstantRegion;
    public int maxSpawnsAtOnce = 4;
    public float spawnDelay = 3.0f;
    float spawnLastTime;

	// Use this for initialization
	void Start ()
    {
        if (myFlyingEnemy)
        {
            myFlyingEnemy.enabled = false;
            myFlyingEnemy.gameObject.SetActive(false);
            myFlyingEnemy.mySpawner = this;
            initialPos = myFlyingEnemy.transform.position;
        }
        if (myWalkingEnemy)
        {
            myWalkingEnemy.gameObject.SetActive(false);
            myWalkingEnemy.enabled = false;
            myWalkingEnemy.mySpawner = this;
            initialPos = myWalkingEnemy.transform.position;
        }
        if (myShooterEnemy)
        {
            myShooterEnemy.gameObject.SetActive(false);
            myShooterEnemy.enabled = false;
            myShooterEnemy.mySpawner = this;
            initialPos = myShooterEnemy.transform.position;
        }
        
	}
	
	// Update is called once per frame
	void Update () {


		if(canDeploy && !enemyDeployed)
        {
            if (!isConstantRegion)
            {
                if (myFlyingEnemy)
                {
                    myFlyingEnemy.transform.position = initialPos;
                    if (!myFlyingEnemy.enabled)
                        myFlyingEnemy.enabled = true;
                    myFlyingEnemy.gameObject.SetActive(true);
                    enemyDeployed = true;
                    myFlyingEnemy.InitMovement();
                }           
            }
            else
            {
                if (spawnLastTime + spawnDelay < Time.time)
                {
                    List<EnemyFlierController> removalList = new List<EnemyFlierController>();
                    foreach(EnemyFlierController fly in myFlyingEnemiesList)
                    {
                        if (fly.gameObject.activeSelf == false)
                        {
                            removalList.Add(fly);
                        }
                    }
                    foreach(EnemyFlierController flyremove in removalList)
                    {
                        myFlyingEnemiesList.Remove(flyremove);
                        Destroy(flyremove.gameObject);
                    }
                    if (myFlyingEnemiesList.Count < maxSpawnsAtOnce)
                    {
                        GameObject newFlyer = Instantiate(myFlyingEnemy.gameObject);
                        newFlyer.gameObject.SetActive(true);
                        newFlyer.GetComponent<EnemyFlierController>().enabled = true;
                        newFlyer.GetComponent<EnemyFlierController>().InitFromRegion();
                        newFlyer.GetComponent<EnemyFlierController>().mySpawner = this;
                        myFlyingEnemiesList.Add(newFlyer.GetComponent<EnemyFlierController>());
                    }
                    spawnLastTime = Time.time;
                }
            }

            if (myWalkingEnemy)
            {
                myWalkingEnemy.transform.position = initialPos;
                if (!myWalkingEnemy.enabled)
                    myWalkingEnemy.enabled = true;
                myWalkingEnemy.gameObject.SetActive(true);
                enemyDeployed = true;
                myWalkingEnemy.InitMovement();
            }
            if (myShooterEnemy)
            {
                myShooterEnemy.transform.position = initialPos;
                if (!myShooterEnemy.enabled)
                    myShooterEnemy.enabled = true;
                myShooterEnemy.gameObject.SetActive(true);
                enemyDeployed = true;
                myShooterEnemy.Init();
            }
        }
	}

    void SpawnEnemy()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //if it hits the player
        if (other.GetComponent<PlayerController>() != null)
        {
            canDeploy = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //if it hits the player
        if (other.GetComponent<PlayerController>() != null)
        {
            canDeploy = false;
        }
    }

   
}
