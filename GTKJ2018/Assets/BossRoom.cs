using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour {

    public Transform mobSpawn1;
    public Transform mobSpawn2;
    public Transform topSpawnArea;

    public GameObject mobSpawnRunner;
    public GameObject mobSpawnGunner;


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

}
