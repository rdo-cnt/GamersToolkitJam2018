using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 6f;
    public Vector3 moveDirection = new Vector3(-1, 0, 0);
    bool isHit = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        this.transform.position += moveDirection * Time.deltaTime * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHit)
        {
            isHit = true;
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("hit player trigger");
                collision.gameObject.SendMessage("BulletHit"); //replace with proper call and response for parry
            }
            else
            {
                //Destroy(this.gameObject);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isHit)
        {
            isHit = true;
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("hit player collider");
                collision.gameObject.SendMessage("BulletHit"); //replace with proper call and response for parry
            }
            else
            {
                //Destroy(this.gameObject);
            }

        }
    }

}
