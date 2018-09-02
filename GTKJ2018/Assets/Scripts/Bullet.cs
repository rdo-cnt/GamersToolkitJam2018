using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 6f;
    public Vector3 moveDirection = new Vector3(-1, 0, 0);
    public bool isReflected = false;

    public float hitboxDisabledForSeconds; //disable hitbox for a brief moment so the enemy doesn't shoot himself.
    public Collider2D hitbox;
    Collider2D colli;

	// Use this for initialization
	void Start () {
        hitbox.enabled = false;
        colli = GetComponent<Collider2D>();

        colli.enabled = false;
        StartCoroutine(DisableHitBox());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    IEnumerator DisableHitBox()
    {


        yield return new WaitForSeconds(hitboxDisabledForSeconds);
        hitbox.enabled = true;
        colli.enabled = true;

    }

    private void FixedUpdate()
    {
        this.transform.position += moveDirection * Time.deltaTime * bulletSpeed;
    }

    public void ReflectBullet()
    {
        if (!isReflected)
        {
            isReflected = true;
            moveDirection *= -1;
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1f, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }



}
