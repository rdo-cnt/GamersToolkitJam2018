using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    //Enemy Container
    public EnemyFlierController myFlyingEnemy;
    public GroundedEnemyController myWalkingEnemy;
    public Vector3 initialPos;
    public bool enemyDeployed;
    public bool canDeploy;
    public bool isConstantRegion;

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
        
	}
	
	// Update is called once per frame
	void Update () {
		if(canDeploy && !enemyDeployed)
        {
            if (myFlyingEnemy)
            {
                myFlyingEnemy.transform.position = initialPos;
                if (!myFlyingEnemy.enabled)
                    myFlyingEnemy.enabled = true;
                myFlyingEnemy.gameObject.SetActive(true);
                enemyDeployed = true;
                if (isConstantRegion)
                {
                    //Debug.Log("doing const region spawn");
                    myFlyingEnemy.InitFromRegion();
                }
                else
                {
                    myFlyingEnemy.InitMovement();
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
