using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggering death on " + collision.gameObject.name);
        if (collision.GetComponent<EnemyBase>() || collision.GetComponent<PlayerController>())
        {
            collision.gameObject.SendMessage("Die");
        }
        
    }
}
