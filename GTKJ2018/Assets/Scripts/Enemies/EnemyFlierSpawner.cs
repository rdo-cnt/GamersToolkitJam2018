using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlierSpawner : MonoBehaviour {

    //Enemy Container
    public EnemyFlierController myEnemy;
    public Vector3 initialPos;
    public bool enemyDeployed;
    public bool canDeploy;

	// Use this for initialization
	void Start () {
        myEnemy.enabled = false; //Disable enemy at first
        myEnemy.mySpawner = this;
        initialPos = myEnemy.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(canDeploy && !enemyDeployed)
        {
            myEnemy.transform.position = initialPos;
            if(!myEnemy.enabled)
                myEnemy.enabled = true;
            enemyDeployed = true;
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
