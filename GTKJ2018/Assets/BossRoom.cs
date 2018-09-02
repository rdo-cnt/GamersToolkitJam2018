using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour {

    public Transform mobSpawn1;
    public Transform mobSpawn2;
    public Transform batRegionSpawn;
    public Transform topSpawnArea;

    //public GameObject mobSpawnRunner;
    //public GameObject mobSpawnGunner;

    public GameObject batSpawnRegionPrefab;
    public GameObject mobSpawnPrefab;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("got trigger enter from " + collision.gameObject.name);
        if (collision.GetComponent<GroundedEnemyController>() || collision.GetComponent<PlayerController>())
        {
            MoveObjectToTop(collision.gameObject);
        }
            
    }

    void MoveObjectToTop(GameObject objToMove)
    {
        objToMove.transform.position = new Vector3(objToMove.transform.position.x, topSpawnArea.transform.position.y, objToMove.transform.position.z);
    }

    public void TriggerNext(int healthTrigger)
    {
        if (healthTrigger == 6)
        {
            GameObject newSpawn = Instantiate(mobSpawnPrefab);
            newSpawn.transform.position = mobSpawn1.transform.position;
        }
        if (healthTrigger == 5)
        {
            GameObject newSpawn = Instantiate(mobSpawnPrefab);
            newSpawn.transform.position = mobSpawn2.transform.position;
        }
        if (healthTrigger == 4)
        {
            GameObject newSpawn = Instantiate(batSpawnRegionPrefab);
            newSpawn.transform.position = batRegionSpawn.transform.position;
            newSpawn.GetComponent<EnemySpawner>().maxSpawnsAtOnce = 2;
        }
        if (healthTrigger == 3)
        {
            GameObject newSpawn = Instantiate(mobSpawnPrefab);
            newSpawn.transform.position = mobSpawn1.transform.position;
        }
        if (healthTrigger == 2)
        {
            GameObject newSpawn = Instantiate(mobSpawnPrefab);
            newSpawn.transform.position = mobSpawn2.transform.position;
        }
        if (healthTrigger == 1)
        {
            GameObject newSpawn = Instantiate(batSpawnRegionPrefab);
            newSpawn.transform.position = batRegionSpawn.transform.position;
            newSpawn.GetComponent<EnemySpawner>().maxSpawnsAtOnce = 3;
        }
        if (healthTrigger == 0) //finish 
        {

        }
    }

}
