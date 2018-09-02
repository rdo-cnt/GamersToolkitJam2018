using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlierController : EnemyBase {

    //Might have parent spawner
    public EnemySpawner mySpawner;
    public float speed = 2f;
    public float distanceThreshold = 6f;

    // Use this for initialization
    protected override void Start() {
        base.Start();

        //getBoxCollider
        m_col = GetComponent<BoxCollider2D>();

        //getBoxCollider
        m_rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        //set velocity
        m_rb.velocity = new Vector2(-speed, 0);

        //if I have a parent
        if (mySpawner != null)
        {
            float n = Mathf.Abs(transform.position.x - mySpawner.transform.position.x);
            if(n > distanceThreshold)
                m_rb.velocity = new Vector2(-speed, speed);
            if(n>(distanceThreshold*2))
            {
                mySpawner.enemyDeployed = false;
                this.gameObject.SetActive(false);
            }
        }
        

	}
}
